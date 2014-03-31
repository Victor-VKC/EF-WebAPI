using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model;
using App.Repository;

namespace App.Service
{
    public interface IProductService
    {
        IEnumerable<ProductCategory> GetProductCategories();
        IEnumerable<Product> GetProductByProductCategoryId(int id);
    }

    public class ProductService:IProductService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository)
        {
            this._productRepository = productRepository;
            this._productCategoryRepository = productCategoryRepository;
        }

        IEnumerable<ProductCategory> IProductService.GetProductCategories()
        {
            var result = _productCategoryRepository.GetAll().ToList();
            return result;
        }

        IEnumerable<Product> IProductService.GetProductByProductCategoryId(int id)
        {
            var result = id > 4 ? _productRepository.GetMany(item => item.ProductSubcategoryID == id).ToList() : 
                _productRepository.GetMany(item => item.ProductSubcategory.ProductCategoryID == id).ToList();
            return result;
        }
    }
}
