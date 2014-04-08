﻿using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class PaymentMethod
    {
        // Regex rules for the fields.
        // Notice that you might need more complex rules in your app.

        // We allow all Unicode letter characters as well as internal spaces and hypens, as long as these do not occur in sequences.
        private const string NAMES_REGEX_PATTERN = @"\A\p{L}+([\p{Zs}\-][\p{L}]+)*\z";

        // We allow all Unicode numeric characters and hypens, as long as these do not occur in sequences.
        private const string NUMBERS_REGEX_PATTERN = @"\A\p{N}+([\p{N}\-][\p{N}]+)*\z";

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        public string CardNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        [RegularExpression(NAMES_REGEX_PATTERN, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRegex")]
        public string CardholderName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        [RegularExpression(NUMBERS_REGEX_PATTERN, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRegex")]
        public string ExpirationMonth { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        [RegularExpression(NUMBERS_REGEX_PATTERN, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRegex")]
        public string ExpirationYear { get; set; }

        [RegularExpression(NUMBERS_REGEX_PATTERN, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRegex")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "ErrorRequired")]
        public string CardVerificationCode { get; set; }

        public string Id { get; set; }

        public bool IsDefault { get; set; }
    }
}