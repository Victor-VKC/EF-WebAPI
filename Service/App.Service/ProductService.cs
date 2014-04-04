using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using App.Entity;
using App.Repository;
using App.Model;
using App.DAL;
using App.Service.Converters;

namespace App.Service
{
    public interface IProductService
    {
        IEnumerable<CategoryObject> GetProductCategories();
        IEnumerable<ProductObject> GetProductsBySubcatgegory(int id);
        IEnumerable<SubcategoryObject> GetSubcategoriesByCategory(int id);
    }

    public class ProductService:IProductService
    {
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductSubcategory> _subcategoryRepository;

        public ProductService()
        {
            _productRepository = new Repository<Product>(DatabaseFactory.Instance);
            _productCategoryRepository = new Repository<ProductCategory>(DatabaseFactory.Instance);
            _subcategoryRepository = new Repository<ProductSubcategory>(DatabaseFactory.Instance);
        }

        public ProductObject GetProductById(int id)
        {
            return
                _productRepository.GetById(id).ToProductObject();
        }

        public byte[] GetProductPicture(int id)
        {
            var result = new Repository<ProductProductPhoto>(DatabaseFactory.Instance).Get(item => item.ProductID == id);
            return 
                new Repository<ProductPhoto>(DatabaseFactory.Instance).Get(item => item.ProductPhotoID == result.ProductPhotoID).LargePhoto;
        }

        public IEnumerable<ProductObject> GetProductList(int? subcategoryId = null, 
            int pageNumber = 1, int pageLength = 20, 
            string orderByField = "Name", bool forward = true)
        {
            var products = _productRepository.GetAll().Where(item => item.ProductSubcategoryID == subcategoryId);
            switch (orderByField.ToLowerInvariant())
            {
                case "name":
                    products = forward ? products.OrderBy(item => item.Name) : products.OrderByDescending(item => item.Name);
                    break;
                case "productid":
                    products = forward ? products.OrderBy(item => item.ProductID) : products.OrderByDescending(item => item.ProductID);
                    break;
                case "listprice":
                    products = forward ? products.OrderBy(item => item.ListPrice) : products.OrderByDescending(item => item.ListPrice);
                    break;
            }
            products = products.Skip((pageNumber - 1)*pageLength).Take(pageLength);
            var picturesMaps = new Repository<ProductProductPhoto>(DatabaseFactory.Instance);
            var picturesData = new Repository<ProductPhoto>(DatabaseFactory.Instance);
            return
                (from item in products let map = picturesMaps.Get(m => m.ProductID == item.ProductID)
                 select item.ToProductObject(picturesData.Get(d => d.ProductPhotoID == map.ProductPhotoID).ThumbNailPhoto));
        }


        public IEnumerable<ProductObject> GetAllProduct()
        {
            return from item in _productRepository.GetAll() select item.ToProductObject();
        }

        public void UpdateProduct(ProductObject item)
        {
            var target = _productRepository.GetById(item.ProductId);
            target.GetDataFromProductObject(item);
            new UnitOfWork (DatabaseFactory.Instance).Commit();
        }

        public IEnumerable<ProductObject> GetProductsBySubcatgegory(int id)
        {
            return
                (from item in _productRepository.GetAll().Where(item => item.ProductSubcategoryID == id)
                    select new ProductObject() {ListPrice = item.ListPrice, ProductId = item.ProductID, Name = item.Name}).ToList();
        }

        public IEnumerable<SubcategoryObject> GetSubcategoriesByCategory(int id)
        {
            return
                (from item in _subcategoryRepository.GetAll().Where(item => item.ProductCategoryID == id)
                    select new SubcategoryObject() {Name = item.Name, SubcategoryId = item.ProductSubcategoryID}).ToList();
        }

        public IEnumerable<CategoryObject> GetProductCategories()
        {
            var items = (from item in _productCategoryRepository.GetAll()
                select new CategoryObject() {CategoryId = item.ProductCategoryID, Name = item.Name}).ToList();
            foreach (var item in items)
                item.Subcategories = GetSubcategoriesByCategory(item.CategoryId).ToList();
            return items;
        }

    }
}
