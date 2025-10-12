using DashBoardAPI.Entity;
using DashBoardAPI.Repository;
using System.Data;
using System.Data.SqlClient;

namespace DashBoardAPI.Service.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<CustomerEntity> _customerRepository;

        public CustomerService(IRepository<CustomerEntity> customerRepository)
        {
            _customerRepository = customerRepository;

        }

        public JsonResponseEntity GetCustomerDetails()
        {
            var response = new JsonResponseEntity();

            try
            {
                // Step 1: Authenticate user
                var authCommand = new SqlCommand("stpGetCustomerDetails");
                authCommand.CommandType = CommandType.StoredProcedure;

                var Customer = _customerRepository.GetRecord(authCommand);


                response.Status = ApiStatus.OK;
                response.Message = "Customer Fetched";
                response.Data = Customer;
            }
            catch (Exception ex)
            {
                response.Status = ApiStatus.Error;
                response.Message = "Error occurred while logging in: " + ex.Message;
            }

            return response;
        }

    }
}
