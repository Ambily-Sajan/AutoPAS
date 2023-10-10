using AutoPAS.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoPAS.Services.Interfaces
{
    public interface ICustomerPortal
    {
        public Task<portaluser> validateUserLogin(portaluser portaluser);
        public Task<bool> ValidatePolicy(int policyNumber);
        public Task<bool> ValidateChasis(string chasisnumber);
        public Task<bool> AddPolicyNumber(UserPolicyListDTO userPolicyListDTO);
        public Task<Policyvehicle?> GetPolicyVehicle(int policynumber);

        public Task<List<userpolicylist>> GetUserPolicyNumber(int userid);

        public Task<VehicleDetailsDTO?> GetVehicleDetails(int policynumber);

        public Task<bool> DeletePolicy(UserPolicyListDTO userPolicyListDTO);
    }
}
