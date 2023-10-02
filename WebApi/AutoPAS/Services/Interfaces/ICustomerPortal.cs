using AutoPAS.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoPAS.Services.Interfaces
{
    public interface ICustomerPortal
    {
        //public Task<portaluser> validateUserLogin(string username, string password);
        public Task<portaluser> validateUserLogin(portaluser portaluser);
        public Task<Policy> validatePolicy(int policyNumber);
        //public Task<object> get();
        public Task<Vehicle> validateChasis(string vehicleId, string chasisNumber);
        public Task<userpolicylist> AddPolicyNumber(int userid, int policynumber);
        public Task<Policyvehicle> GetPolicyVehicle(int policynumber);

        public Task<List<userpolicylist>> GetUserPolicyNumber(int userid);

        public Task<VehicleDetailsDTO> GetVehicleDetails(int policynumber);
        public Task<string> DeletePolicy(int policynumber);
    }
}
