using Microsoft.EntityFrameworkCore;
using ModelLayer.DTOs;
using DatabaseLayer.InsuranceContext;

namespace BusinessLayer.Services
{
    public class PolicyService
    {
        private readonly InsuranceDbContext _context;

        public PolicyService(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<PolicySummaryDto> GetSummary(string policyNumber)
        {
            var policy = await _context.Policies
                .Include(p => p.Claims)
                .FirstOrDefaultAsync(p => p.PolicyNumber == policyNumber);

            if (policy == null)
                throw new Exception("Policy not found");

            decimal approved = policy.Claims
                .Where(c => c.Status == "Approved")
                .Sum(c => c.ClaimAmount);

            return new PolicySummaryDto
            {
                PolicyNumber = policy.PolicyNumber,
                CoverageAmount = policy.CoverageAmount,
                TotalClaimsRaised = policy.Claims.Count,
                TotalApprovedAmount = approved,
                RemainingCoverage = policy.CoverageAmount - approved
            };
        }
    }

}
