using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Services.DAL;
using Server.Entity;
using Data.Models;
using DataProduct = Data.Models.Product;
using EntityProduct = Server.Entity.Product;

namespace Server.Services
{
    public interface IProductService
    {
        IEnumerable<DataProduct> GetProducts();
        DataProduct GetProduct(int id);
        IEnumerable<DataProduct> GetProductsByCategory(int? categoryId);
        IEnumerable<DataProduct> GetProductsPage(int? categoryId, int pageNumber, int pageLength, string orderData, bool forward = true);
        SearchResult GetSearchResults(string queryString, int pageNumber, int maxResults);
        void UpdateProduct(DataProduct product);
    }

    public class ProductService : IProductService
    {
        private readonly IRepository<EntityProduct> _productRepository;

        public ProductService()
            : this(new Repository<EntityProduct>())
        {}

        public ProductService(IRepository<EntityProduct> productRepository)
        {
            _productRepository = productRepository;
        }

        private static DataProduct ConvertProduct(EntityProduct product)
        {
            if (product == null) return null;
            return
                new DataProduct
                {
                    Color = product.Color,
                    ListPrice = product.ListPrice,
                    Name = product.Name,
                    ProductCategoryID = product.ProductCategoryID,
                    ProductID = product.ProductID,
                    ProductModelID = product.ProductModelID,
                    ProductNumber = product.ProductNumber,
                    Size = product.Size,
                    StandardCost = product.StandardCost,
                    ThumbNailPhoto = product.ThumbNailPhoto,
                    ThumbnailPhotoFileName = product.ThumbnailPhotoFileName
                };
        }

        public IEnumerable<DataProduct> GetProducts()
        {
            return _productRepository.GetAll().Select(ConvertProduct).ToList();
        }

        public DataProduct GetProduct(int id)
        {
            return
                ConvertProduct(_productRepository.Get(item => item.ProductID == id));
        }

        public IEnumerable<DataProduct> GetProductsByCategory(int? categoryId)
        {
            return
                _productRepository.GetMany(item => item.ProductCategoryID == categoryId).Select(ConvertProduct).ToList();
        }

        public IEnumerable<DataProduct> GetProductsPage(int? categoryId, int pageNumber, int pageLength, string orderData,
            bool forward = true)
        {
            var list = _productRepository.GetMany(item => item.ProductCategoryID == categoryId);
            switch (orderData.ToLowerInvariant())
            {
                case "name":
                    list = forward ? list.OrderBy(item => item.Name) : list.OrderByDescending(item => item.Name);
                    break;
                case "productid":
                    list = forward ? list.OrderBy(item => item.ProductID) : list.OrderByDescending(item => item.ProductID);
                    break;
                case "listprice":
                    list = forward ? list.OrderBy(item => item.ListPrice) : list.OrderByDescending(item => item.ListPrice);
                    break;
            }
            var lC = list.Count();
            if (lC == 0)
                return new List<DataProduct>();
            var pN = pageNumber < 1 ? 1 : pageNumber;
            var pL = pageLength < 1 ? 1 : pageLength;
            if (pN > lC/pL) pN = lC/pL;
            if(lC > pL)
                list = list.Skip((pN - 1)*pL).Take(pL);
            return 
                list.Select(ConvertProduct).ToList();
        }


        public SearchResult GetSearchResults(string queryString, int pageNumber, int maxResults)
        {
            IEnumerable<EntityProduct> list =
                _productRepository.GetMany(item => item.Name.ToLower().Contains(queryString.ToLower())).OrderBy(item => item.ProductCategoryID).ToList();
            var lC = list.Count();
            if (lC == 0)
                return new SearchResult {TotalCount = 0, Products = null};
            var pN = pageNumber < 1 ? 1 : pageNumber;
            var pL = maxResults < 1 ? 1 : maxResults;
            if (pN > lC / pL) pN = lC / pL;
            if (lC > pL)
                list = list.Skip((pN - 1)*pL).Take(pL);
            return 
                new SearchResult
                {
                    TotalCount = lC,
                    Products = list.Select(ConvertProduct)
                };
        }

        public void UpdateProduct(DataProduct product)
        {
            var target = _productRepository.Get(item => item.ProductID == product.ProductID);
            if(target == null) return;
            target.Color = product.Color;
            target.ListPrice = product.ListPrice;
            target.Name = product.Name;
            target.ProductNumber = product.ProductNumber;
            target.Size = product.Size;
            target.StandardCost = product.StandardCost;
            target.ModifiedDate = DateTime.Now;
            _productRepository.Update(target);
            _productRepository.Commit();
        }

    }
}
