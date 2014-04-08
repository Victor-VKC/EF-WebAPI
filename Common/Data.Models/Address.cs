using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Address
    {
        private string _addressType;
        // Regex rules for the fields.
        // Notice that you might need more complex rules in your app.

        // We allow all Unicode letter and numeric characters as well as internal spaces, as long as these do not occur in sequences.
        private const string ADDRESS_REGEX_PATTERN = @"\A[\p{L}\p{N}]+([\p{Zs}][\p{L}\p{N}]+)*\z";

        // We allow all Unicode numeric characters and hypers, as long as these do not occur in sequences.
        private const string NUMBERS_REGEX_PATTERN = @"\A\p{N}+([\p{N}\-][\p{N}]+)*\z";

        public int AddressID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        public string AddressType 
        {
            get  { return _addressType[0] == '*' ? _addressType.Remove(0, 1) : _addressType; }
            set { _addressType = value; }     
        }
        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        [RegularExpression(ADDRESS_REGEX_PATTERN, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRegex")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        [RegularExpression(ADDRESS_REGEX_PATTERN, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRegex")]
        public string City { get; set; }
        public string StateProvince { get; set; }
        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        [RegularExpression(ADDRESS_REGEX_PATTERN, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRegex")]
        public string CountryRegion { get; set; }
        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        [RegularExpression(NUMBERS_REGEX_PATTERN, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRegex")]
        public string PostalCode { get; set; }
        
        public bool IsDefault
        {
            get { return _addressType[0] == '*'; }
        }
    }
}
