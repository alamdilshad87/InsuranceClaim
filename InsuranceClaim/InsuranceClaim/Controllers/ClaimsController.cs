using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Exceptions;
using ModelLayer.Requests;

namespace InsuranceClaim.Controllers
{
    [ApiController]
    [Route("api/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly ClaimService _service;

        public ClaimsController(ClaimService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(SubmitClaimRequest request)
        {
            try
            {
                var id = await _service.SubmitClaim(request);
                return Ok(new { ClaimId = id });
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            try
            {
                await _service.ApproveClaim(id);
                return Ok("Claim approved");
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}