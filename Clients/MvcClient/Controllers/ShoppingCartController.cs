using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcClient.Models;

namespace MvcClient.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShoppingCart/

        public ActionResult Index()
        {
            var cart = ShoppingCartModel.GetCart();
            return PartialView(cart);
        }

        public RedirectResult Add()
        {
            var cart = ShoppingCartModel.GetCart();
            cart.Count++;
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

    }
}
