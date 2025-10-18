using DashBoardAPI.Entity;
using DashBoardAPI.Repository;
using System.Data;
using System.Data.SqlClient;

namespace DashBoardAPI.Service.UserService
{
    public class UserService:IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;

        public UserService(IRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;

        }


        public List<JsonResponseEntity> GetUserDetails()
        {
            var responses = new List<JsonResponseEntity>();

            try
            {
                using (var authCommand = new SqlCommand("stpGetEngineerDetails"))
                {
                    authCommand.CommandType = CommandType.StoredProcedure;

                    var Users = _userRepository.GetRecords(authCommand).ToList();

                    foreach (var item in Users)
                    {
                        responses.Add(new JsonResponseEntity
                        {
                            Status = ApiStatus.OK,
                            Message = "Users fetched successfully.",
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
                    Message = "Error occurred while fetching Users: " + ex.Message,
                    Data = null
                });
            }

            return responses;
        }

    }
}
