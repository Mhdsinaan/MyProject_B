using MyProject.Models.AddressModel;

namespace MyProject.Interfaces
{
    public interface IAddress
    {
        //public Task<Address> GetAddress(int id, int userId);
        Task<IEnumerable<Address>> GetAllAddresses(int userId);
        Task<Address> AddAddress(AddressDto request, int userId);
        Task<Address> UpdateAddress(int id, AddressDto request, int userId);
        Task<Address> DeleteAddress(int id, int userId);
    }
}
