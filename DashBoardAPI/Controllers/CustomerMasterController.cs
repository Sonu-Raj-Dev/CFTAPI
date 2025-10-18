using DashBoardAPI.Entity;
using DashBoardAPI.Service.CustomerService;
using DashBoardAPI.Service.LoginService;
using Microsoft.AspNetCore.Mvc;
using static DashBoardAPI.Entity.MasterModels;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerMasterController : ControllerBase
    {

        private readonly ICustomerService _customerservice;


        public CustomerMasterController(ICustomerService customerservice)
        {

            this._customerservice = customerservice;
        }



        [HttpPost("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            var customer = _customerservice.GetCustomerDetails();

            return Ok(new
            {
                Success = true,
                Data = customer
            });
        }

    }
}
