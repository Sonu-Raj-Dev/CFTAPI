using DashBoardAPI.Entity;

namespace DashBoardAPI.Service.EngineerService
{
    public interface IEngineerService
    {
        List<JsonResponseEntity> GetEngineerDetails();
    }
}
