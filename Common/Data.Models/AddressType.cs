using System.Collections.Generic;

namespace Data.Models
{
    public static class AddressType
    {
        private static readonly string[] AddressTypeStrings = new string[]
        {
            "Main Office",
            "Post Office",
            "Shipping",
            "Home",
            "Office",
            "Additional"
        };

        public static IEnumerable<string> Types
        {
            get { return AddressTypeStrings; }
        }
    }
}
