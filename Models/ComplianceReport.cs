using System;
using System.ComponentModel.DataAnnotations;
using SupplyChainTransparency.Validation;

namespace SupplyChainTransparency.Models
{
    public class ComplianceReport
    {
        public int Id { get; set; }
        [RequiredNonEmptyString(ErrorMessage = "Company Name is required and cannot be empty.")]
        public required string CompanyName { get; set; }
        [Required]
        public DateTime ReportDate { get; set; }
        public bool IsCompliant { get; set; }
        [RequiredNonEmptyString(ErrorMessage = "Details are required and cannot be empty.")]
        public required string Details { get; set; }
    }
}


