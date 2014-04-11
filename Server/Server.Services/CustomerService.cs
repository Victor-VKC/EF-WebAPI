using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Services.DAL;
using EntityCustomer = Server.Entity.Customer;
using DataCustomer = Data.Models.Customer;

namespace Server.Services
{
    public interface ICustomerService
    {
        DataCustomer GetCustomer(int id);
        void SaveCustomer(DataCustomer customer);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IRepository<EntityCustomer> _customerRepository;

        public CustomerService(): this(new Repository<EntityCustomer>())
        {}

        public CustomerService(IRepository<EntityCustomer> customeRepository)
        {
            _customerRepository = customeRepository;
        }

        public DataCustomer GetCustomer(int id)
        {
            var customer = _customerRepository.Get(item => item.CustomerID == id);
            return new DataCustomer
            {
                NameStyle = customer.NameStyle,
                CompanyName = customer.CompanyName,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MiddleName = customer.MiddleName,
                Phone = customer.Phone,
                SalesPerson = customer.SalesPerson,
                Suffix = customer.Suffix, 
                Title = customer.Title, 
                CustomerId = id
            };
        }

        public void SaveCustomer(DataCustomer customer)
        {
            EntityCustomer result;
            //if (customer.CustomerId == 0)
            //    result = new EntityCustomer
            //    {
            //        CustomerID = _customerRepository.GetAll().Max(item => item.CustomerID) + 1, 
            //        rowguid = Guid.NewGuid(), PasswordSalt = "LATER", PasswordHash = "LATER"
            //    };
            //else
            //    result = _customerRepository.Get(item => item.CustomerID == customer.CustomerId);
            //=========================================
            if(customer.CustomerId == 0) return;
            result = _customerRepository.Get(item => item.CustomerID == customer.CustomerId);
            //=========================================
            result.NameStyle = customer.NameStyle;
            result.CompanyName = customer.CompanyName;
            result.EmailAddress = customer.EmailAddress;
            result.FirstName = customer.FirstName;
            result.LastName = customer.LastName;
            result.MiddleName = customer.MiddleName;
            result.Phone = customer.Phone;
            result.SalesPerson = customer.SalesPerson;
            result.Suffix = customer.Suffix;
            result.Title = customer.Title;
            result.ModifiedDate = DateTime.Now;
            //if (customer.CustomerId == 0)
            //{
            //    _customerRepository.Add(result);
            //    customer.CustomerId = result.CustomerID;
            //}
            //else
                _customerRepository.Update(result);
            _customerRepository.Commit();
        }
    }
}
