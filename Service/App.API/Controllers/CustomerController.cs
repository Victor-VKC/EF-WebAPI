using System.Web.Http;
using App.Model;
using App.Service;


namespace App.API.Controllers
{
    public class CustomersController : ApiController
    {
        readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        /// <summary>
        /// To validate Customer
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public PersonObject ValidateCustomer(int id, string password)
        {
            return _customerService.ValidateCustomer(id, password);
        }

        /// <summary>
        /// To Save Customer
        /// </summary>
        /// <returns></returns>
        public int PostCustomer(PersonObject customer)
        {
            return _customerService.SaveOrUpdateCustomer(customer);
        }

    }
}
