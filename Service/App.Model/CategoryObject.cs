using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class CategoryObject
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<SubcategoryObject> Subcategories { get; set; } 
    }
}
