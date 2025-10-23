using DashBoardAPI.Entity;
using DashBoardAPI.Repository;
using System.Data;
using System.Data.SqlClient;

namespace DashBoardAPI.Service.RoleService
{
    public class RoleService:IRoleService
    {
        private readonly IRepository<RoleEntity> _roleRepository;

        public RoleService(IRepository<RoleEntity> roleRepository)
        {
            _roleRepository = roleRepository;

        }

        public List<JsonResponseEntity> GetRoles()
        {
            var responses = new List<JsonResponseEntity>();

            try
            {
                using (var authCommand = new SqlCommand("stpGetRoles"))
                {
                    authCommand.CommandType = CommandType.StoredProcedure;          
                    var complaints = _roleRepository.GetRecords(authCommand).ToList();

                    foreach (var item in complaints)
                    {
                        responses.Add(new JsonResponseEntity
                        {
                            Status = ApiStatus.OK,
                            Message = "Roles fetched successfully.",
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
                    Message = "Error occurred while fetching Roles: " + ex.Message,
                    Data = null
                });
            }

            return responses;
        }

    }
}
