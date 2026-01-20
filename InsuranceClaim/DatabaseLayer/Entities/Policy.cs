using System.ComponentModel.DataAnnotations;
using DatabaseLayer.Entities;

public class Policy
{
    [Key, Required]
    public int PolicyId { get; set; }
    [Required]
    public string? PolicyNumber { get; set; }
    [Required]
    public decimal CoverageAmount { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = "Active";

    public ICollection<Claim> Claims { get; set; } = new List<Claim>();
}
