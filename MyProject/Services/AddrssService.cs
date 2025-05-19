using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.AddressModel;


namespace MyProject.Services
{
    public class AddrssService : IAddress
    {
        private readonly IMapper _mapper;
        private readonly MyContext _myContext;

        public AddrssService(MyContext myContext, IMapper mapper)
        {

            _myContext = myContext;
            _mapper = mapper;
        }
        public async Task<Address> AddAddress(AddressDto request, int userId)
        {
            try
            {


                var address = new Address
                {
                    UserId = userId,
                    Street = request.Street,
                    City = request.City,
                    State = request.State,
                    ZipCode = request.ZipCode,
                    Country = request.Country
                };
                _myContext.Addresses.Add(address);
                await _myContext.SaveChangesAsync();
                return address;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while deleting data", ex);
            }


        }

        public async Task<Address> DeleteAddress(int id, int userId)
        {
            var address = await _myContext.Addresses
                  .Where(a => a.Id == id && a.UserId == userId)
                  .FirstOrDefaultAsync();

            if (address == null)
            {
                throw new Exception("Address not found or does not belong to the user.");
            }

            _myContext.Addresses.Remove(address);
            await _myContext.SaveChangesAsync();

            return address;
        }

        //public async Task<Address> GetAddress(int id, int userId)
        //{
        //    var result= await _myContext.Addresses.FirstOrDefaultAsync(p=>p.Id==id && p.UserId == userId);
        //    if (result == null)
        //    {
        //        throw new Exception("nodata found");

        //    }
        //    return result;

        //}

        public async Task<IEnumerable<Address>> GetAllAddresses(int userId)
        {
            var result = await _myContext.Addresses.Where(p => p.UserId == userId).ToListAsync();
            if (result == null)
            {
                throw new Exception("address notfound");
            }
            return result;


        }

        public async Task<Address> UpdateAddress(int id, AddressDto request, int userId)
        {
            try
            {
                var address = await _myContext.Addresses.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
                if (address == null)
                {

                    throw new Exception("no data found");

                }
                address.Street = request.Street;
                address.City = request.City;
                address.State = request.State;
                address.ZipCode = request.ZipCode;
                address.Country = request.Country;
                await _myContext.SaveChangesAsync();
                return address;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occured while fetching data", ex);
            }

        }
    }
}
