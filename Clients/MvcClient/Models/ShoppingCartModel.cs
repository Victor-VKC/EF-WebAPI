using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcClient.Models
{
    public class ShoppingCartModel
    {
        public int Count { get; set; }

        private static ShoppingCartModel _cart;

        public static ShoppingCartModel GetCart()
        {
            return _cart ?? (_cart = new ShoppingCartModel());
        }
    }
}