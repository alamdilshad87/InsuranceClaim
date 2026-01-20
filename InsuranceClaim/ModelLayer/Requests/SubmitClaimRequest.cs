using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Requests
{
    public class SubmitClaimRequest
    {
        [Required]
        public string PolicyNumber { get; set; } = null!;
        [Required]
        public decimal ClaimAmount { get; set; }
        [Required]
        public DateTime ClaimDate { get; set; }
    }
}