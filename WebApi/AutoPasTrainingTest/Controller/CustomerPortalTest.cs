using AutoFixture;
using AutoPAS.Controllers;
using AutoPAS.Models;
using AutoPAS.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AutoPasTrainingTest.Controller
{
    public class CustomerPortalTest
    {
        private readonly IFixture fixture;
        private readonly Mock<ICustomerPortal> _customerPortalInterface;
        private readonly customerPortalController _customerPortalController;
        public CustomerPortalTest()
        {
            fixture = new Fixture();
            _customerPortalInterface = fixture.Freeze<Mock<ICustomerPortal>>();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _customerPortalController = new customerPortalController(_customerPortalInterface.Object);
        }

        //Test Cases For ValidateUserLogin
        [Fact]
        public async Task ValidateUserLogin_ShouldReturnValidResult_WhenResultIsNotNull()
        {
            //Arrange
            
            var portaluser = fixture.Create<portaluser>();
            var returndata = fixture.Create<portaluser>();

            _customerPortalInterface.Setup(t => t.validateUserLogin(portaluser))
                                  .ReturnsAsync(returndata);
            // Act
            var result = await _customerPortalController.ValidateUserLogin(portaluser);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(returndata);


            _customerPortalInterface.Verify(t => t.validateUserLogin(portaluser), Times.Once);
        }

        [Fact]
        public async Task ValidateUserLogin_ShouldReturnNull_WhenResultIsNull()
        {
            // Arrange
            var portaluser = fixture.Create<portaluser>();
           

            _customerPortalInterface.Setup(t => t.validateUserLogin(portaluser))
                                   .ReturnsAsync((portaluser)null);

            // Act
            var result = await _customerPortalController.ValidateUserLogin(portaluser);


            //Assert
            result.Should().BeOfType<NotFoundResult>();
            _customerPortalInterface.Verify(t => t.validateUserLogin(portaluser), Times.Once);

        }

        [Fact]
        public async Task ValidateUserLogin_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var portaluser = fixture.Create<portaluser>();

            _customerPortalInterface.Setup(t => t.validateUserLogin(portaluser))
                .Throws(new Exception("Exception Occured"));

            // Act
            var result = await _customerPortalController.ValidateUserLogin(portaluser);

            // Assert

            result.Should().BeAssignableTo<BadRequestObjectResult>().Subject.Value.Should().Be("Exception Occured");
            _customerPortalInterface.Verify(t => t.validateUserLogin(portaluser), Times.Once());
        }

        //Test cases For AddPolicyNumber
        [Fact]
        public async Task AddPolicyNumber_ShouldReturnOkResult_WhenDataIsValid()
        {
            // Arrange
            var userpolicyListDto = fixture.Create<UserPolicyListDTO>();
            var res = fixture.Create<bool>();

            _customerPortalInterface.Setup(t => t.AddPolicyNumber(userpolicyListDto)).ReturnsAsync(res);


            // Act
            var result = await _customerPortalController.AddPolicyNumber(userpolicyListDto);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(res);


            _customerPortalInterface.Verify(t => t.AddPolicyNumber(userpolicyListDto), Times.Once());
        }
        [Fact]
        public async Task AddPolicyNumber_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            //Arrange
            var userpolicyListDto = fixture.Create<UserPolicyListDTO>();

            _customerPortalInterface.Setup(u => u.AddPolicyNumber(userpolicyListDto)).ThrowsAsync(new Exception("Exception Occured"));

            // Act
            var result = await _customerPortalController.AddPolicyNumber(userpolicyListDto);

            // Assert

            result.Should().BeAssignableTo<BadRequestObjectResult>().Subject.Value.Should().Be("Exception Occured");
            _customerPortalInterface.Verify(t => t.AddPolicyNumber(userpolicyListDto), Times.Once());
        }

        //Test Cases for ValidatePolicy
        [Fact]
        public async Task ValidatePolicyN_ShouldReturnsOkWithTrue_WhenValidPolicyNumber()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            _customerPortalInterface.Setup(u => u.ValidatePolicy(policyNumber)).ReturnsAsync(true);

            // Act
            var result = await _customerPortalController.ValidatePolicy(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(true);

            _customerPortalInterface.Verify(t => t.ValidatePolicy(policyNumber), Times.Once());
        }

        [Fact]
        public async Task ValidatePolicy_ShouldReturnOkWithFalse_WhenInvalidPolicyNumber()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            _customerPortalInterface.Setup(u => u.ValidatePolicy(policyNumber)).ReturnsAsync(false);

            // Act
            var result = await _customerPortalController.ValidatePolicy(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(false);

            _customerPortalInterface.Verify(t => t.ValidatePolicy(policyNumber), Times.Once());
        }

        [Fact]
        public async Task ValidatePolicy_ShouldReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            _customerPortalInterface.Setup(u => u.ValidatePolicy(policyNumber)).ThrowsAsync(new Exception("Exception Occured when validating  Policy Number"));

            // Act
            var result = await _customerPortalController.ValidatePolicy(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be("Exception Occured when validating  Policy Number");

            _customerPortalInterface.Verify(t => t.ValidatePolicy(policyNumber), Times.Once());
        }

        //Test Cases for ValidateChasis

        [Fact]
        public async Task ValidateChasis_ShouldReturnsOkWithTrue_WhenValidChasisNumber()
        {
            // Arrange
            var chasisNumber = fixture.Create<string>();

            _customerPortalInterface.Setup(u => u.ValidateChasis(chasisNumber)).ReturnsAsync(true);

            // Act
            var result = await _customerPortalController.ValidateChasis(chasisNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(true);

            _customerPortalInterface.Verify(t => t.ValidateChasis(chasisNumber), Times.Once());
        }

        [Fact]
        public async Task ValidateChasis_ShouldReturnsOkWithFalse_WhenInvalidChasisNumber()
        {
            // Arrange
            var chasisNumber = fixture.Create<string>();

            _customerPortalInterface.Setup(u => u.ValidateChasis(chasisNumber)).ReturnsAsync(false);

            // Act
            var result = await _customerPortalController.ValidateChasis(chasisNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(false);

            _customerPortalInterface.Verify(t => t.ValidateChasis(chasisNumber), Times.Once());
        }

        [Fact]
        public async Task ValidateChasis_ShouldReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var chasisNumber = fixture.Create<string>();
            _customerPortalInterface.Setup(u => u.ValidateChasis(chasisNumber)).ThrowsAsync(new Exception("Exception Occured when validating  Chasis Number"));

            // Act
            var result = await _customerPortalController.ValidateChasis(chasisNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be("Exception Occured when validating  Chasis Number");

            _customerPortalInterface.Verify(t => t.ValidateChasis(chasisNumber), Times.Once());
        }


        //Test Cases for GetUserPolicyNumber
        [Fact]
        public async Task GetUserPolicyNumber_ShouldReturnsOkResult_WhenValidData()
        {
            // Arrange
            int userid = fixture.Create<int>();
            var policynumbers = fixture.CreateMany<userpolicylist>(3).ToList();

            _customerPortalInterface.Setup(x => x.GetUserPolicyNumber(userid)).ReturnsAsync(policynumbers);

            // Act
            var result = await _customerPortalController.GetUserPolicyNumber(userid);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(policynumbers);

            _customerPortalInterface.Verify(m => m.GetUserPolicyNumber(userid), Times.Once);
        }
        [Fact]
        public async Task GetUserPolicyNumber_ShouldReturnNotFoundResult_WhenNoPolicyNumbers()
        {
            // Arrange
            int userid = fixture.Create<int>();

            _customerPortalInterface.Setup(x => x.GetUserPolicyNumber(userid)).ReturnsAsync(new List<userpolicylist>());

            // Act
            var result = await _customerPortalController.GetUserPolicyNumber(userid);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be("Policy numbers do not exist");

            _customerPortalInterface.Verify(m => m.GetUserPolicyNumber(userid), Times.Once);
        }

        [Fact]
        public async Task GetUserPolicyNumber_ShouldReturnsBadRequestResult_WhenExceptionThrown()
        {
            // Arrange
            int userid = fixture.Create<int>();

            _customerPortalInterface.Setup(x => x.GetUserPolicyNumber(userid)).ThrowsAsync(new Exception());

            // Act
            var result = await _customerPortalController.GetUserPolicyNumber(userid);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Exception Occurred during fetching");
        }

        //Test cases for GetVehicleDetails
        [Fact]
        public async Task GetVehicleDetails_ShouldReturnsOkResult_WhenValidData()
        {
            // Arrange
            int policynumber = fixture.Create<int>();
            var vehicleDetailsDTO = fixture.Create<VehicleDetailsDTO>();

            _customerPortalInterface.Setup(x => x.GetVehicleDetails(policynumber)).ReturnsAsync(vehicleDetailsDTO);

            // Act
            var result = await _customerPortalController.GetVehicleDetails(policynumber);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(vehicleDetailsDTO);

            _customerPortalInterface.Verify(m => m.GetVehicleDetails(policynumber), Times.Once);
        }

        [Fact]
        public async Task GetVehicleDetails_ShouldReturnsNotFoundResult_WhenNoRecords()
        {
            // Arrange
            int policynumber = fixture.Create<int>();

            _customerPortalInterface.Setup(x => x.GetVehicleDetails(policynumber)).ReturnsAsync((VehicleDetailsDTO)null);

            // Act
            var result = await _customerPortalController.GetVehicleDetails(policynumber);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be("No Records");

            _customerPortalInterface.Verify(m => m.GetVehicleDetails(policynumber), Times.Once);
        }

        [Fact]
        public async Task GetVehicleDetails_ShouldReturnsBadRequestResult_WhenExceptionThrown()
        {
            // Arrange
            int policynumber = fixture.Create<int>();

            _customerPortalInterface.Setup(x => x.GetVehicleDetails(policynumber)).ThrowsAsync(new Exception());

            // Act
            var result = await _customerPortalController.GetVehicleDetails(policynumber);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Exception Occured");
        }


        //Test cases for DeletePolicyNumber

        [Fact]
        public async Task DeletePolicy_ShouldReturnsOkWithResult_WhenDeletedSuccessfully()
        {
            // Arrange
            var userPolicyListDto = fixture.Create<UserPolicyListDTO>();
            var Result = fixture.Create<bool>();

            _customerPortalInterface.Setup(x => x.DeletePolicy(userPolicyListDto)).ReturnsAsync(Result);

            // Act
            var result = await _customerPortalController.DeletePolicynumber(userPolicyListDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be(Result);

            _customerPortalInterface.Verify(t => t.DeletePolicy(userPolicyListDto), Times.Once());
        }

        [Fact]
        public async Task DeletePolicy_ShouldReturnsBadRequestResult_WhenExceptionIsThrown()
        {
            // Arrange
            var userPolicyListDto = fixture.Create<UserPolicyListDTO>();

            _customerPortalInterface.Setup(u => u.DeletePolicy(userPolicyListDto)).ThrowsAsync(new Exception("Exception Occured While Deleting"));

            // Act
            var result = await _customerPortalController.DeletePolicynumber(userPolicyListDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Exception Occured While Deleting");
        }

    }

}
        
