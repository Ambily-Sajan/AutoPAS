using AutoPAS.Models;
using AutoPAS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoPAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customerPortalController : ControllerBase
    {
        private readonly ICustomerPortal customerPortalInterface;

        public customerPortalController(ICustomerPortal customerPortalInterface)
        {
            this.customerPortalInterface = customerPortalInterface;
        }

        [HttpPost("/userlogin")]
        
        public async Task<IActionResult> ValidateUserLogin([FromBody] portaluser portaluser)
        {
            try
            {
                var result = await customerPortalInterface.validateUserLogin(portaluser);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest("Exception Occured");
            }

        }
        
        [HttpPost]
        public async Task<IActionResult> AddPolicyNumber([FromBody]UserPolicyListDTO userPolicyListDTO)
        {
            try
            {
                var result = await customerPortalInterface.AddPolicyNumber(userPolicyListDTO);
                return Ok(result);

            }
            catch
            {
                return BadRequest("Exception Occured");
            }
        }

        [HttpGet("validatePolicy/{policynumber}")]
        public async Task<IActionResult> ValidatePolicy([FromRoute] int policynumber)
        {
            try
            {
                var record = await customerPortalInterface.ValidatePolicy(policynumber);
                if (record != true)
                {
                    return Ok(false);
                }
                return Ok(true);


            }
            catch
            {
                return BadRequest("Exception Occured when validating  Policy Number");
            }

        }

        [HttpGet("validateChasis/{chasisnumber}")]
        public async Task<IActionResult> ValidateChasis([FromRoute] string chasisnumber)
        {
            try
            {
                var record = await customerPortalInterface.ValidateChasis(chasisnumber);
                if (record != true)
                {
                    return Ok(false);
                    
                }
                return Ok(true);


            }
            catch
            {
                return BadRequest("Exception Occured when validating  Chasis Number");
            }

        }

        [HttpGet]
        [Route("/user/{userid}")]
        public async Task<IActionResult> GetUserPolicyNumber([FromRoute]int userid)
        {
            try
            {
                var policynumbers = await customerPortalInterface.GetUserPolicyNumber(userid);
                if (policynumbers != null && policynumbers.Count > 0)
                {
                    return Ok(policynumbers);
                }
                return NotFound("Policy numbers do not exist");
            }
            catch
            {
                return BadRequest("Exception Occurred during fetching");
            }
        }

        [HttpGet]
        [Route("{policynumber:int}")]
        public async Task<IActionResult> GetVehicleDetails([FromRoute] int policynumber)
        {
            try
            {
                var vehicleDetails = await customerPortalInterface.GetVehicleDetails(policynumber);
                if (vehicleDetails != null)
                {
                    return Ok(vehicleDetails);
                }
                return NotFound("No Records");
            }
            catch 
            {
                return BadRequest("Exception Occured");
            }
        }

        [HttpDelete("/removepolicy")]
        public async Task<IActionResult> DeletePolicynumber([FromBody] UserPolicyListDTO userPolicyListDto)
        {
            try
            {
                var result = await customerPortalInterface.DeletePolicy(userPolicyListDto);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Exception Occured While Deleting");
            }
        }

    }
}
