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


    }
}
