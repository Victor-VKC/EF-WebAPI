using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Security;
using App.Entity;
using App.Model;
using App.Repository;

namespace App.Service
{
    public interface ICustomerService
    {
        PersonObject ValidateCustomer(int id, string password);
        int SaveOrUpdateCustomer(PersonObject customer);
    }

    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

#region Password Create methods
        //Below method will create any random strings of given size. Basically this type of algorithm reads the memory at random locations to form
        //the complete random string each time
        private static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        //The salt created in above function will be appended to the real password
        //and again SHA1 algorithm will be used to generate the hash which will eventually stored in database
        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
#pragma warning disable 618
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA1");
#pragma warning restore 618
            hashedPwd = String.Concat(hashedPwd, salt);
            return hashedPwd;
        }
#endregion

        public PersonObject ValidateCustomer(int id, string password)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                return null;
            var passwordHash = customer.Person.Password.PasswordHash;
            var passwordSalt = passwordHash.Substring(passwordHash.Length - 8);
            var passwordDoComapare = CreatePasswordHash(password, passwordSalt);
            if(passwordDoComapare.Equals(passwordHash))
                return CreatePersonObject(customer);
            else
                return null;
        }

        public int SaveOrUpdateCustomer(PersonObject customer)
        {
            var passwordSalt = CreateSalt(5);
            var resultCustomer = customer.CustomerId != 0 ? 
                _customerRepository.GetById(customer.CustomerId) : 
                new Customer {Person = new Person(){EmailAddresses = new Collection<EmailAddress>()}};
            resultCustomer.Person.FirstName = customer.FirstName;
            resultCustomer.Person.MiddleName = customer.MiddleName;
            resultCustomer.Person.LastName = customer.LastName;
            resultCustomer.Person.AdditionalContactInfo = customer.AdditionalContactInfo;
            resultCustomer.Person.Title = customer.Title;
            resultCustomer.Person.Suffix = customer.Suffix;
            resultCustomer.Person.Password.PasswordHash = CreatePasswordHash(customer.Password, passwordSalt);
            resultCustomer.Person.Password.PasswordSalt = passwordSalt;
            resultCustomer.ModifiedDate = DateTime.Now;
            resultCustomer.rowguid = Guid.NewGuid();
            //resultCustomer.Person.EmailAddresses.ElementAtOrDefault(1).EmailAddress1
            if (customer.CustomerId == 0)
            {
                resultCustomer.Person.EmailAddresses.Add(new EmailAddress()
                {
                    EmailAddress1 = customer.EmailAddress,
                    ModifiedDate = DateTime.Now,
                    Person = resultCustomer.Person,
                    rowguid = resultCustomer.rowguid,
                    EmailAddressID = 0
                });
                _customerRepository.Add(resultCustomer);
            }
            else
            {
                _customerRepository.Update(resultCustomer);
            }
            return resultCustomer.CustomerID;
        }

        private PersonObject CreatePersonObject(Customer customer)
        {
            var result = new PersonObject()
            {
                FirstName = customer.Person.FirstName,
                MiddleName = customer.Person.MiddleName,
                LastName = customer.Person.LastName,
                AdditionalContactInfo = customer.Person.AdditionalContactInfo,
                CustomerId = customer.CustomerID,
                Rowguid = customer.rowguid,
                Title = customer.Person.Title,
                Suffix = customer.Person.Suffix,
                EmailAddress = customer.Person.EmailAddresses.ElementAt(0).EmailAddress1,
                Password = customer.Person.Password.PasswordHash
            };
            return result;
        }
    }
}
