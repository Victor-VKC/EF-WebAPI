using System.Collections.Generic;
using System.Linq;
using Data.Models;
using Server.Entity;
using Server.Services.DAL;

namespace Server.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories(int? parentId);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<ProductCategory> _categoryRepository;


        public CategoryService()
            : this(new Repository<ProductCategory>())
        { }

        public CategoryService(IRepository<ProductCategory> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        protected Category ConvertCategory(ProductCategory category)
        {
            if (category == null) return null;
            return new Category
            {
                Name = category.Name,
                Id = category.ProductCategoryID,
                ParentId = category.ParentProductCategoryID
            };
        }

        private IList<Category> GetCategoriesById(int? parentId)
        {
            return _categoryRepository.GetAll().Where(item => item.ParentProductCategoryID == parentId).Select(ConvertCategory).ToList();
        }

        public IEnumerable<Category> GetCategories(int? parentId)
        {
            var productRepository = new Repository<Server.Entity.Product>();
            var list = GetCategoriesById(parentId);
            foreach (var productCategory in list)
            {
                var total = productRepository.GetAll().Count(item => item.ProductCategoryID == productCategory.Id);
                productCategory.Subcategories = GetCategoriesById(productCategory.Id);
                foreach (var subcategory in productCategory.Subcategories)
                {
                    subcategory.TotalNumberOfItems = productRepository.GetAll().Count(item => item.ProductCategoryID == subcategory.Id);
                    total += subcategory.TotalNumberOfItems;
                }
                productCategory.TotalNumberOfItems = total;
            }
            return list;
        }
    }
}
