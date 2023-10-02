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
        public async Task ValidateUserLogin_ShouldReturnNotNull_WhenResultIsNull()
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
        public async Task AddPolicyNumber_ShouldReturnValidData_WhenDataIsValid()
        {
            // Arrange
            int userid = fixture.Create<int>();
            int policynumber = fixture.Create<int>();
            string chasisNumber = fixture.Create<string>();

            var policy = fixture.Create<Policy>();
            var policyvehicle = fixture.Create<Policyvehicle>();
            var vehicle = fixture.Create<Vehicle>();
            var userpolicy = fixture.Create<userpolicylist>();

            _customerPortalInterface.Setup(t => t.validatePolicy(policynumber)).ReturnsAsync(policy);
            _customerPortalInterface.Setup(t => t.GetPolicyVehicle(policynumber)).ReturnsAsync(policyvehicle);
            _customerPortalInterface.Setup(t => t.validateChasis(policyvehicle.VehicleId, chasisNumber)).ReturnsAsync(vehicle);
            _customerPortalInterface.Setup(t => t.AddPolicyNumber(userid, policynumber)).ReturnsAsync(userpolicy);

            // Act
            var result = await _customerPortalController.AddPolicyNumber(userid, policynumber, chasisNumber);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(userpolicy);

            _customerPortalInterface.Verify(t => t.validatePolicy(policynumber), Times.Once);
            _customerPortalInterface.Verify(t => t.GetPolicyVehicle(policynumber), Times.Once);
            _customerPortalInterface.Verify(t => t.validateChasis(policyvehicle.VehicleId, chasisNumber), Times.Once);
            _customerPortalInterface.Verify(t => t.AddPolicyNumber(userid, policynumber), Times.Once);
        }

        [Fact]
        public async Task AddPolicyNumber_ShouldReturnsNotFoundResult_WhenPolicyIsNull()
        {
            // Arrange
            int userid = fixture.Create<int>();
            int policynumber = fixture.Create<int>();
            string chasisNumber = fixture.Create<string>();

            _customerPortalInterface.Setup(t => t.validatePolicy(policynumber)).ReturnsAsync((Policy)null);

            // Act
            var result = await _customerPortalController.AddPolicyNumber(userid, policynumber, chasisNumber);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be("Incorrect PolicyNumber");

            _customerPortalInterface.Verify(t => t.validatePolicy(policynumber), Times.Once);
        }

        [Fact]
        public async Task AddPolicyNumber_ShouldReturnsNotFoundResult_WhenPolicyVehicleIsNull()
        {
            // Arrange
            int userid = fixture.Create<int>();
            int policynumber = fixture.Create<int>();
            string chasisNumber = fixture.Create<string>();

            var policy = fixture.Create<Policy>();

            _customerPortalInterface.Setup(x => x.validatePolicy(policynumber)).ReturnsAsync(policy);
            _customerPortalInterface.Setup(x => x.GetPolicyVehicle(policynumber)).ReturnsAsync((Policyvehicle)null);

            // Act
            var result = await _customerPortalController.AddPolicyNumber(userid, policynumber, chasisNumber);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be("No Vehicle Found for entered PolicyNumber");

            _customerPortalInterface.Verify(m => m.validatePolicy(policynumber), Times.Once);
            _customerPortalInterface.Verify(m => m.GetPolicyVehicle(policynumber), Times.Once);
            _customerPortalInterface.Verify(m => m.validateChasis(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _customerPortalInterface.Verify(m => m.AddPolicyNumber(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task AddPolicyNumber_ShouldReturnsNotFoundResult_WhenVehicleIsNull()
        {
            // Arrange
            int userid = fixture.Create<int>();
            int policynumber = fixture.Create<int>();
            string chasisNumber = fixture.Create<string>();

            var policy = fixture.Create<Policy>();
            var policyvehicle = fixture.Create<Policyvehicle>();

            _customerPortalInterface.Setup(x => x.validatePolicy(policynumber)).ReturnsAsync(policy);
            _customerPortalInterface.Setup(x => x.GetPolicyVehicle(policynumber)).ReturnsAsync(policyvehicle);
            _customerPortalInterface.Setup(x => x.validateChasis(policyvehicle.VehicleId, chasisNumber)).ReturnsAsync((Vehicle)null);

            // Act
            var result = await _customerPortalController.AddPolicyNumber(userid, policynumber, chasisNumber);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be("ChasisNumber not matched");

            _customerPortalInterface.Verify(m => m.validatePolicy(policynumber), Times.Once);
            _customerPortalInterface.Verify(m => m.GetPolicyVehicle(policynumber), Times.Once);
            _customerPortalInterface.Verify(m => m.validateChasis(policyvehicle.VehicleId, chasisNumber), Times.Once);
            _customerPortalInterface.Verify(m => m.AddPolicyNumber(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task AddPolicyNumber_ShouldReturnsBadRequestResult_WhenExceptionThrown()
        {
            // Arrange
            int userid = fixture.Create<int>();
            int policynumber = fixture.Create<int>();
            string chasisNumber = fixture.Create<string>();

            _customerPortalInterface.Setup(x => x.validatePolicy(policynumber)).ThrowsAsync(new Exception());

            // Act
            var result = await _customerPortalController.AddPolicyNumber(userid, policynumber, chasisNumber);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                  .Which.Value.Should().BeEquivalentTo("Exception Occured");
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
        public async Task DeletePolicynumber_ShouldReturnsOkResult_WhenPolicyDeleted()
        {
            // Arrange
            int policynumber = fixture.Create<int>();

            _customerPortalInterface.Setup(x => x.DeletePolicy(policynumber)).ReturnsAsync("Deleted");

            // Act
            var result = await _customerPortalController.DeletePolicynumber(policynumber);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be("Deleted Successfully");
        }

        [Fact]
        public async Task DeletePolicynumber_ShouldReturnBadRequestResult_WhenPolicyNotFound()
        {
            // Arrange
            int policynumber = fixture.Create<int>();

            _customerPortalInterface.Setup(x => x.DeletePolicy(policynumber)).ReturnsAsync((string)null);

            // Act
            var result = await _customerPortalController.DeletePolicynumber(policynumber);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Failed");
        }

        [Fact]
        public async Task DeletePolicynumber_ShouldReturnsBadRequestResult_WhenExceptionThrown()
        {
            // Arrange
            int policynumber = fixture.Create<int>();

            _customerPortalInterface.Setup(x => x.DeletePolicy(policynumber)).ThrowsAsync(new Exception());

            // Act
            var result = await _customerPortalController.DeletePolicynumber(policynumber);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Exception Occured While Deleting");
        }

    }

}
        
