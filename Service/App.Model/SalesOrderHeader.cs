//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SalesOrderHeader
    {
        public SalesOrderHeader()
        {
            this.SalesOrderDetails = new HashSet<SalesOrderDetail>();
            this.SalesOrderHeaderSalesReasons = new HashSet<SalesOrderHeaderSalesReason>();
        }
    
        public int SalesOrderID { get; set; }
        public byte RevisionNumber { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public byte Status { get; set; }
        public bool OnlineOrderFlag { get; set; }
        public string SalesOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string AccountNumber { get; set; }
        public int CustomerID { get; set; }
        public Nullable<int> SalesPersonID { get; set; }
        public Nullable<int> TerritoryID { get; set; }
        public int BillToAddressID { get; set; }
        public int ShipToAddressID { get; set; }
        public int ShipMethodID { get; set; }
        public Nullable<int> CreditCardID { get; set; }
        public string CreditCardApprovalCode { get; set; }
        public Nullable<int> CurrencyRateID { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal Freight { get; set; }
        public decimal TotalDue { get; set; }
        public string Comment { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual CreditCard CreditCard { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
        public virtual SalesPerson SalesPerson { get; set; }
        public virtual ICollection<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReasons { get; set; }
    }
}
