using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTOs
{
    public class PolicySummaryDto
    {
        public string PolicyNumber { get; set; }
        public decimal CoverageAmount { get; set; }
        public int TotalClaimsRaised { get; set; }
        public decimal TotalApprovedAmount { get; set; }
        public decimal RemainingCoverage { get; set; }
    }
}
