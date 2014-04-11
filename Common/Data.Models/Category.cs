using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class Category
    {
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Category> Subcategories { get; set; }
        public int TotalNumberOfItems { get; set; }
    }
}
