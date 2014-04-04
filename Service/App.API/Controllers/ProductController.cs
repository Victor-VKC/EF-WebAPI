using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using App.Model;
using App.Service;

namespace App.API.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        
        //public IEnumerable<ProductObject> Get()
        //{
        //    return
        //        _productService.GetAllProduct();
        //}

        public IEnumerable<ProductObject> GetListPage(int? subcategoryId, int pageNumber, int pageLength, 
            string orderByField, bool forward = true)
        {
            return
                _productService.GetProductList(subcategoryId, pageNumber, pageLength, orderByField, forward);
        }

        //public ProductObject Get(int id)
        //{
        //    return _productService.GetProductById(id);
        //}

        public byte[] GetLargePhoto(int id)
        {
            return 
                _productService.GetProductPicture(id);
        }

    }
}
