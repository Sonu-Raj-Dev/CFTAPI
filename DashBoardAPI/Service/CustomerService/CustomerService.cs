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


        public List<JsonResponseEntity> GetCustomerDetails()
        {
            var responses = new List<JsonResponseEntity>();

            try
            {
                using (var authCommand = new SqlCommand("stpGetCustomerDetails"))
                {
                    authCommand.CommandType = CommandType.StoredProcedure;

                    var complaints = _customerRepository.GetRecords(authCommand).ToList();

                    foreach (var item in complaints)
                    {
                        responses.Add(new JsonResponseEntity
                        {
                            Status = ApiStatus.OK,
                            Message = "Customer fetched successfully.",
                            Data = item
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                responses.Add(new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Error occurred while fetching customer: " + ex.Message,
                    Data = null
                });
            }

            return responses;
        }

        public JsonResponseEntity InsertUpdateCustomerMaster(CustomerEntity customer)
        {

            var responses = new JsonResponseEntity();

            try
            {

                var authCommand = new SqlCommand("stpInsertUpdateCustomerMaster");
                    authCommand.CommandType = CommandType.StoredProcedure;
                authCommand.Parameters.AddWithValue("@Id", customer.Id);
                authCommand.Parameters.AddWithValue("@Name", customer.Name);
                authCommand.Parameters.AddWithValue("@Mobile", customer.Mobile);
                authCommand.Parameters.AddWithValue("@Email", customer.Email);
                authCommand.Parameters.AddWithValue("@Address", customer.Address);
                authCommand.Parameters.AddWithValue("@IsActive", customer.IsActive);
                var complaints = _customerRepository.GetRecords(authCommand).ToList();


                responses.Status = ApiStatus.OK;
                           responses.Message = "Customer Created successfully.";
                           responses.Data = complaints;

            }
            catch (Exception ex)
            {

                responses.Status = ApiStatus.Error;
                responses.Message = "Error occurred while fetching customer: " + ex.Message;
                responses.Data = null;
            }

            return responses;
        }
    
    }
}
