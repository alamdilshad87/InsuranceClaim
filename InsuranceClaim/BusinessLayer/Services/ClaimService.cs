using BusinessLayer.Interfaces;
using DatabaseLayer.Entities;
using DatabaseLayer.InsuranceContext;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Requests;
using ModelLayer.Exceptions;

namespace BusinessLayer.Services
{
    public class ClaimService
    {
        private readonly InsuranceDbContext _context;

        public ClaimService(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<int> SubmitClaim(SubmitClaimRequest request)
        {
            var policy = await _context.Policies
                .Include(p => p.Claims)
                .FirstOrDefaultAsync(p => p.PolicyNumber == request.PolicyNumber);

            if (policy == null)
                throw new BusinessException("Policy not found");

            if (policy.Status != "Active")
                throw new BusinessException("Policy is not active");

            if (request.ClaimDate < policy.StartDate || request.ClaimDate > policy.EndDate)
                throw new BusinessException("Claim date outside policy period");

            bool claimedToday = policy.Claims
                .Any(c => c.ClaimDate.Date == request.ClaimDate.Date);

            if (claimedToday)
                throw new BusinessException("Only one claim allowed per policy per day");

            decimal usedAmount = policy.Claims
                .Where(c => c.Status == "Approved" || c.Status == "Submitted")
                .Sum(c => c.ClaimAmount);

            if (usedAmount + request.ClaimAmount > policy.CoverageAmount)
                throw new BusinessException("Claim exceeds coverage amount");

            var claim = new Claim
            {
                PolicyId = policy.PolicyId,
                ClaimAmount = request.ClaimAmount,
                ClaimDate = request.ClaimDate,
                Status = "Submitted"
            };

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return claim.ClaimId;
        }

        public async Task ApproveClaim(int claimId)
        {
            var claim = await _context.Claims
                .Include(c => c.Policy)
                .ThenInclude(p => p.Claims)
                .FirstOrDefaultAsync(c => c.ClaimId == claimId);

            if (claim == null)
                throw new BusinessException("Claim not found");

            if (claim.Status != "Submitted")
                throw new BusinessException("Only submitted claims can be approved");

            decimal approvedAmount = claim.Policy.Claims
                .Where(c => c.Status == "Approved")
                .Sum(c => c.ClaimAmount);

            if (approvedAmount + claim.ClaimAmount > claim.Policy.CoverageAmount)
                throw new BusinessException("Coverage exceeded during approval");

            claim.Status = "Approved";
            await _context.SaveChangesAsync();
        }
    }
}