using System.Collections.Generic;
using System.Web.Mvc;
using Data.Models;
using Data.Transport;

namespace MvcClient.Controllers
{
    public class CategoryController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            var result = ApiAdapter<IList<Category>>.Instance.Get("Category?parentCategoryId=null");
            return PartialView(result);
        }
    }
}
