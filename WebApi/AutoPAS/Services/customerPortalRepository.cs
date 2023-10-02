using AutoPAS.Data;
using AutoPAS.Models;
using AutoPAS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoPAS.Services
{
    public class customerPortalRepository:ICustomerPortal
    {
        private readonly UserDbContext userDbContext;
        private readonly AutoPasDbContext autoPasDbContext;

        public customerPortalRepository(UserDbContext userDbContext, AutoPasDbContext autoPasDbContext)
        {
            this.userDbContext = userDbContext;
            this.autoPasDbContext = autoPasDbContext;
        }

        /*public async Task<object> get()
        {
            var result = await userDbContext.portaluser.ToListAsync();
            return result;
        }*/

        public async Task<portaluser> validateUserLogin(portaluser portaluser)
        {
            var result = await userDbContext.portaluser.FirstOrDefaultAsync(u => u.username == portaluser.username && u.password == portaluser.password);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public async Task<Policy> validatePolicy(int policyNumber)
        {
            var policy = await autoPasDbContext.Policies.FirstOrDefaultAsync(p => p.PolicyNumber == policyNumber);
            if (policy != null)
            {
                return policy;
            }
            return null;
        }
        public async Task<Vehicle> validateChasis(string vehicleId, string chasisNumber)
        {
            var vehicle = await autoPasDbContext.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == vehicleId && v.ChasisNumber == chasisNumber);
            if (vehicle != null)
            {
                return vehicle;
            }
            return null;


        }
        public async Task<Policyvehicle> GetPolicyVehicle(int policynumber)
        {
            var policy = await autoPasDbContext.Policies.FirstOrDefaultAsync(p => p.PolicyNumber == policynumber);
            var policyvehicle= await autoPasDbContext.Policyvehicles.FirstOrDefaultAsync(v => v.PolicyId == policy.PolicyId);
            if (policyvehicle != null)
            {
                return policyvehicle;
            }
            return null;
        }
        public async Task<userpolicylist> AddPolicyNumber(int userid, int policynumber)
        {
            userpolicylist userpolicylist = new userpolicylist
            {
               
                userid = userid,
                policynumber = policynumber
            };

            await userDbContext.userpolicylist.AddAsync(userpolicylist);
            await userDbContext.SaveChangesAsync();
            return userpolicylist;
        }

        public async Task<List<userpolicylist>> GetUserPolicyNumber(int userid)
        {
            var policynumbers = await userDbContext.userpolicylist.Where(p => p.userid == userid).ToListAsync();
            return policynumbers;
        }

        public async Task<VehicleDetailsDTO> GetVehicleDetails(int policynumber)
        {
            
         
                var policy = await autoPasDbContext.Policies.FirstOrDefaultAsync(p => p.PolicyNumber == policynumber);

                if (policy == null)
                {
                    return null; // Policy not found for the given policy number
                }

                var policyVehicle = await autoPasDbContext.Policyvehicles.FirstOrDefaultAsync(v => v.PolicyId == policy.PolicyId);

                if (policyVehicle == null)
                {
                    return null; // Policy vehicle not found for the given policy
                }

                var vehicleDetails = await autoPasDbContext.Vehicles
                    .Where(v => v.VehicleId == policyVehicle.VehicleId)
                    .Select(v => new VehicleDetailsDTO
                    {
                        PolicyNumber = policynumber,
                        VehicleType = v.VehicleType.VehicleType1,
                        Rtoname = v.Rto.Rtoname,
                        City = v.Rto.City,
                        State = v.Rto.State,
                        RegistrationNumber = v.RegistrationNumber,
                        DateOfPurchase = v.DateOfPurchase,
                        Brand = v.Brand.Brand1,
                        Modelname = v.Model.Modelname,
                        Variant = v.Variant.Variant1,
                        BodyType = v.BodyType.BodyType1,
                        FuelType = v.FuelType.FuelType1,
                        TransmissionType = v.TransmissionType.TransmissionType1,
                        Color = v.Color,
                        ChasisNumber = v.ChasisNumber,
                        EngineNumber = v.EngineNumber,
                        CubicCapacity = v.CubicCapacity,
                        SeatingCapacity = v.SeatingCapacity,
                        YearOfManufacture = v.YearOfManufacture,
                        Idv = v.Idv,
                        ExShowroomPrice = v.ExShowroomPrice
                    })
                    .FirstOrDefaultAsync();

                return vehicleDetails;
            
        }
        public async Task<string> DeletePolicy(int policynumber)
        {
            var userPolicy = await userDbContext.userpolicylist.FirstOrDefaultAsync(p => p.policynumber == policynumber);
            userDbContext.userpolicylist.Remove(userPolicy);
            await userDbContext.SaveChangesAsync();
            return "Deleted";
        }
    }
}
