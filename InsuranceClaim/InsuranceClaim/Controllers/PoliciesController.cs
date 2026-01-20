using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Exceptions;

namespace InsuranceClaim.Controllers
{
    [ApiController]
    [Route("api/policies")]
    public class PoliciesController : ControllerBase
    {
        private readonly PolicyService _service;

        public PoliciesController(PolicyService service)
        {
            _service = service;
        }

        [HttpGet("{policyNumber}/summary")]
        public async Task<IActionResult> GetSummary(string policyNumber)
        {
            try
            {
                return Ok(await _service.GetSummary(policyNumber));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
