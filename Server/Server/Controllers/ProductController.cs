using System;
using System.Collections.Generic;
using System.Web.Http;
using Server.Services;
using Data.Models;

namespace Server.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetProducts();
        }

        public Product GetProduct(int id)
        {
            return _productService.GetProduct(id);
        }

        public IEnumerable<Product> GetProducts(int? categoryId)
        {
            return _productService.GetProductsByCategory(categoryId);
        }

        public IEnumerable<Product> GetProducts(int? categoryId, string orderData, bool forward)
        {
            return _productService.GetProductsPage(categoryId, 1, Int32.MaxValue, orderData, forward);
        }

        public SearchResult GetSearchResults(string queryString, int page, int pageLength)
        {
            return _productService.GetSearchResults(queryString, page, pageLength);
        }

        [HttpPut]
        public void Update(Product product)
        {
            _productService.UpdateProduct(product);
        }
    }
}
