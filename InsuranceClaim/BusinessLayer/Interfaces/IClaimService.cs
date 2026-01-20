using ModelLayer.Requests;
namespace BusinessLayer.Interfaces
{
    public interface IClaimService
    {
        Task<int> SubmitClaimAsync(SubmitClaimRequest request);
        Task ApproveClaimAsync(int claimId);
    }
}