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
    
    public partial class ProductPhoto
    {
        public ProductPhoto()
        {
            this.ProductProductPhotoes = new HashSet<ProductProductPhoto>();
        }
    
        public int ProductPhotoID { get; set; }
        public byte[] ThumbNailPhoto { get; set; }
        public string ThumbnailPhotoFileName { get; set; }
        public byte[] LargePhoto { get; set; }
        public string LargePhotoFileName { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual ICollection<ProductProductPhoto> ProductProductPhotoes { get; set; }
    }
}
