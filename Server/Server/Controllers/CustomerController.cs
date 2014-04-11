using System.Web.Http;
using Data.Models;
using Server.Services;

namespace Server.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomerController()
        {
            _customerService = new CustomerService();
        }

        public Customer Get(int id)
        {
            return 
                _customerService.GetCustomer(id);
        }

        public void Put(Customer customer)
        {
            _customerService.SaveCustomer(customer);
        }
    }
}
