using System;
using System.Collections.Generic;
using System.Linq;
using Server.Entity;
using Server.Services.DAL;
using Server.Services.Utils;
using EntityAddress = Server.Entity.Address;
using DataAddress = Data.Models.Address;

namespace Server.Services
{
    public interface IAddressService
    {
        IEnumerable<DataAddress> GetAddresses(int customerId);
        void SetDefaultAddress(int customerId, int addressId);
        void DeleteAddress(int addressId);
        void AddOrSaveAddress(int customerId, Data.Models.Address address);
    }

    public class AddressService : IAddressService
    {
        private readonly IRepository<Server.Entity.CustomerAddress> _addressRepository;

        public AddressService() : this(new Repository<CustomerAddress>())
        {}

        public AddressService(IRepository<CustomerAddress> repository)
        {
            _addressRepository = repository;
        }

        /// <summary>
        /// Check real default address. If not exists set first address as virtual default address.
        /// </summary>
        /// <param name="addresses">Consumer addresses list.</param>
        private static void CheckDefaultAddress(IList<DataAddress> addresses)
        {
            if (addresses.Count == 0) return;
            if (addresses.Any(address => address.IsDefault))
                return;
            var item = addresses[0];
            if (item.AddressType[0] != '*')
                item.AddressType = '*' + item.AddressType;
        }

        public void DeleteAddress(int addressId)
        {
            var addressLink = _addressRepository.Get(item => item.AddressID == addressId);
            if(_addressRepository.GetMany(item => item.CustomerID == addressLink.CustomerID).Count() < 2) return;
            _addressRepository.Delete(addressLink);
            var repository = new Repository<EntityAddress>();
            var address = repository.Get(item => item.AddressID == addressId);
            repository.Delete(address);
            _addressRepository.Commit();
        }

        public void AddOrSaveAddress(int customerId, DataAddress address)
        {
            if(address == null) return;
            var repository = new Repository<EntityAddress>();
            if (address.AddressID == 0)
            {
                address.AddressID = _addressRepository.GetAll().Max(item => item.AddressID) + 1;
                _addressRepository.Add(new CustomerAddress()
                {
                    AddressID = address.AddressID, 
                    AddressType = address.AddressType, 
                    CustomerID = customerId, 
                    ModifiedDate = DateTime.Now, 
                    rowguid = Guid.NewGuid()
                });
                repository.Add(new EntityAddress()
                {
                    AddressID = address.AddressID, 
                    AddressLine1 = address.AddressLine1, 
                    AddressLine2 = address.AddressLine2, 
                    City = address.City, 
                    CountryRegion = address.CountryRegion, 
                    ModifiedDate = DateTime.Now, 
                    PostalCode = address.PostalCode, 
                    StateProvince = address.StateProvince, 
                    rowguid = Guid.NewGuid()});
            }
            else
            {
                var addressLink = _addressRepository.Get(item => item.AddressID == address.AddressID);
                if ((address.IsDefault && !addressLink.AddressType.Equals('*' + address.AddressType)) ||
                    (!address.IsDefault && !address.AddressType.Equals(addressLink.AddressType)))
                {
                    addressLink.AddressType = address.AddressType;
                    addressLink.ModifiedDate = DateTime.Now;
                    _addressRepository.Update(addressLink);
                }
                var target = repository.Get(item => item.AddressID == address.AddressID);
                target.AddressLine1 = address.AddressLine1;
                target.AddressLine2 = address.AddressLine2;
                target.City = address.City;
                target.CountryRegion = address.CountryRegion;
                target.ModifiedDate = DateTime.Now;
                target.PostalCode = address.PostalCode;
                target.StateProvince = address.StateProvince;
                repository.Update(target);
            }
            _addressRepository.Commit();
        }

        public void SetDefaultAddress(int customerId, int addressId)
        {
            var addresses = GetAddresses(customerId);
            foreach (var address in addresses)
            {
                address.SetDefault(address.AddressID == addressId);
                var target = _addressRepository.Get(item => item.AddressID == address.AddressID);
                if (target.IsDefault() == address.IsDefault) continue;
                target.SetDefault(address.IsDefault);
                target.ModifiedDate = DateTime.Now;
                _addressRepository.Update(target);
            }
            _addressRepository.Commit();
        }

        public IEnumerable<Data.Models.Address> GetAddresses(int customerId)
        {
            var customerAddressIDs = _addressRepository.GetMany(item => item.CustomerID == customerId);
            var customerAddressRepository = new Repository<Server.Entity.Address>();
            var result = (from addressItem in customerAddressIDs
                let address = customerAddressRepository.GetById(addressItem.AddressID)
                where address != null
                select new Data.Models.Address
                {
                    AddressID = addressItem.AddressID,
                    AddressType = addressItem.AddressType, 
                    City = address.City, 
                    AddressLine1 = address.AddressLine1, 
                    AddressLine2 = address.AddressLine2, 
                    CountryRegion = address.CountryRegion, 
                    StateProvince = address.CountryRegion, 
                    PostalCode = address.PostalCode
                }).ToList();
            CheckDefaultAddress(result);
            return result;
        }
    }
}
