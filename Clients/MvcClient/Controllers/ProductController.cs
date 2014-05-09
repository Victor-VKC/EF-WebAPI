using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Data.Models;
using Data.Transport;

namespace MvcClient.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Browse(int id)
        {
            var result = ApiAdapter<IList<Product>>.Instance.Get(String.Format("Product?categoryId={0}", id));
            return View(result);
        }

        public ActionResult Index(int id)
        {
            var result = ApiAdapter<Product>.Instance.Get(String.Format("Product/{0}", id));
            return View(result);
        }
        //Product/{id}
    }

}
