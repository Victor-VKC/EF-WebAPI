using System.Collections.Generic;

namespace Data.Models
{
    public class SearchResult
    {
        public int TotalCount { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
