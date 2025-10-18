using DashBoardAPI.Entity;

namespace DashBoardAPI.Service.CustomerService
{
    public interface ICustomerService
    {
        List<JsonResponseEntity> GetCustomerDetails();
    }
}
