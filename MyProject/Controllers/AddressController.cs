using Microsoft.AspNetCore.Mvc;
using MyProject.Interfaces;
using MyProject.Models.AddressModel;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _addressService;

        public AddressController(IAddress addressService)
        {
            _addressService = addressService;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            var addresses = await _addressService.GetAllAddresses(userId);
            return Ok(addresses);
        }

        
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressDto request)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            var addedAddress = await _addressService.AddAddress(request, userId);
            return CreatedAtAction(nameof(GetAllAddresses), new { id = addedAddress.Id }, addedAddress);
        }

        
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressDto request)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            var updatedAddress = await _addressService.UpdateAddress(id, request, userId);
            if (updatedAddress == null)
                return NotFound();

            return Ok(updatedAddress);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            var deletedAddress = await _addressService.DeleteAddress(id, userId);
            if (deletedAddress == null)
                return NotFound();

            return Ok(deletedAddress);
        }
    }
}
