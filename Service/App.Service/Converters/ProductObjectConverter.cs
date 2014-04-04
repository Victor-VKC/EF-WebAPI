using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Entity;
using App.Model;

namespace App.Service.Converters
{
    public static class ProductObjectConverter
    {
        public static ProductObject ToProductObject(this Product item, byte[] thumb = null)
        {
            if(item == null)
                return null;
            return new ProductObject
            {
                Name = item.Name,
                Class = item.Class,
                Color = item.Color,
                ListPrice = item.ListPrice,
                ProductId = item.ProductID,
                ProductModelId = item.ProductModelID,
                ProductNumber = item.ProductNumber,
                ProductSubcategoryId = item.ProductSubcategoryID,
                StandardCost = item.StandardCost,
                Style = item.Style,
                ThumbNail = thumb
            };
        }

        public static void GetDataFromProductObject(this Product target, ProductObject item)
        {
            if(target == null || item == null) return;
            target.Name = item.Name;
            target.Class = item.Class;
            target.Color = item.Color;
            target.ListPrice = item.ListPrice;
            target.ProductID = item.ProductId;
            target.ProductModelID = item.ProductModelId;
            target.ProductNumber = item.ProductNumber;
            target.ProductSubcategoryID = item.ProductSubcategoryId;
            target.StandardCost = item.StandardCost;
            target.Style = item.Style;
        }
    }
}
