using System.Collections.Generic;
using System.Web.Http;
using Data.Models;
using Server.Services;

namespace Server.Controllers
{
    public class AddressController : ApiController
    {
        private readonly IAddressService _addressService;

        public AddressController()
        {
            _addressService = new AddressService();
        }

        [HttpGet]
        public IEnumerable<Address> GetConsumerAddresses(int customerId)
        {
            return _addressService.GetAddresses(customerId);
        }

        [HttpPost]
        public void SetDefaultAddress(int customerId, int addressId)
        {
            _addressService.SetDefaultAddress(customerId, addressId);
        }

        [HttpDelete]
        public void DeleteAddress(int addressId)
        {
            _addressService.DeleteAddress(addressId);
        }

        /// <summary>
        /// Update or add new address
        /// </summary>
        /// <param name="consumerId"></param>
        /// <param name="address"></param>
        [HttpPut]
        public void UpdateAddress(int consumerId, Address address)
        {
            _addressService.AddOrSaveAddress(consumerId, address);
        }
    }
}
