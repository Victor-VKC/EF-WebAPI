using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.DataObjects
{
    public class InvoiceObject
    {
        public int SalesOrderId { get; set; }
        public string SalesOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public System.DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public int CustomerId { get; set; }
        public List<ProductObject> Products { get; set; }
    }
}
