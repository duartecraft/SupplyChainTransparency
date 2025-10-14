using System.ComponentModel.DataAnnotations;
using SupplyChainTransparency.Validation;

namespace SupplyChainTransparency.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [RequiredNonEmptyString(ErrorMessage = "Name is required and cannot be empty.")]
        public required string Name { get; set; }
        [RequiredNonEmptyString(ErrorMessage = "ContactInfo is required and cannot be empty.")]
        public required string ContactInfo { get; set; }
        public bool IsSustainable { get; set; }
        public bool IsFairTrade { get; set; }
    }
}


