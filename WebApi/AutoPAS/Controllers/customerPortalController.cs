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
        /*[HttpGet("userid")]
        public async Task<object> Get()
        {
            var result = await customerPortalInterface.get();
            return result;
        }*/
        [HttpPost]
        public async Task<IActionResult> AddPolicyNumber(int userid, int policynumber, string chasisNumber)
        {
            try
            {
                var policy = await customerPortalInterface.validatePolicy(policynumber);
                if (policy != null)
                {
                    var policyvehicle = await customerPortalInterface.GetPolicyVehicle(policynumber);
                    if (policyvehicle != null)
                    {
                        var vehicle = await customerPortalInterface.validateChasis(policyvehicle.VehicleId, chasisNumber);
                        if (vehicle != null)
                        {
                            var userpolicy = await customerPortalInterface.AddPolicyNumber(userid, policynumber);
                            if (userpolicy != null)
                            {
                                return Ok(userpolicy);
                            }

                        }
                        return NotFound("ChasisNumber not matched");
                    }
                    return NotFound("No Vehicle Found for entered PolicyNumber");
                }
                return NotFound("Incorrect PolicyNumber");
            }
            catch
            {
                return BadRequest("Exception Occured");
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
        [HttpDelete]
        [Route("{policynumber:int}")]
        public async Task<IActionResult> DeletePolicynumber([FromRoute] int policynumber)
        {
            try
            {
                string res = await customerPortalInterface.DeletePolicy(policynumber);
                if (res != null)
                {
                    return Ok("Deleted Successfully");
                }
               
                return BadRequest("Failed");
            }
            catch 
            {
                return BadRequest("Exception Occured While Deleting");
            }
        }

    }
}
