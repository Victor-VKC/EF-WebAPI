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
    
    public partial class SalesOrderHeaderSalesReason
    {
        public int SalesOrderID { get; set; }
        public int SalesReasonID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual SalesOrderHeader SalesOrderHeader { get; set; }
        public virtual SalesReason SalesReason { get; set; }
    }
}
