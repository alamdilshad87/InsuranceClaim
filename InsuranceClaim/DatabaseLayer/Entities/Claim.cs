using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    public class Claim
    {
        [Key, Required]
        public int ClaimId { get; set; }
        public int PolicyId { get; set; }
        public decimal ClaimAmount { get; set; }
        public DateTime ClaimDate { get; set; }
        public string Status { get; set; } = "Submitted";

        public Policy Policy { get; set; } = null!;
    }
}