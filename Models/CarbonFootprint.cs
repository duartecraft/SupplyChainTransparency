using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupplyChainTransparency.Validation;

namespace SupplyChainTransparency.Models
{
    public class CarbonFootprint
    {
        public int Id { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
        [Required]
        public DateTime DateRecorded { get; set; }
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Emissions must be a non-negative value.")]
        public double EmissionsKgCO2e { get; set; }
        [RequiredNonEmptyString(ErrorMessage = "ActivityType is required and cannot be empty.")]
        public required string ActivityType { get; set; }
    }
}


