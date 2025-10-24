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
                using (var authCommand = new SqlCommand("stpGetUserDetails"))
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
        public JsonResponseEntity InsertUpdateUserMaster(UserEntity users)
        {
            var response = new JsonResponseEntity();

            try
            {
                var authCommand = new SqlCommand("stpInsertUpdateUserMaster");
                authCommand.CommandType = CommandType.StoredProcedure;

                authCommand.Parameters.AddWithValue("@UserId", users.Id);
                authCommand.Parameters.AddWithValue("@UserName", users.Name);
                authCommand.Parameters.AddWithValue("@Email", users.Email);
                authCommand.Parameters.AddWithValue("@Password", users.Password);
                authCommand.Parameters.AddWithValue("@IsActive", users.IsActive);

                // If repository returns UserEntity, convert it to JsonResponseEntity
                var result = _userRepository.GetRecord(authCommand);

                if (result != null)
                {
                    response.Status = ApiStatus.Success;
                    response.Message = "User saved successfully";
                    response.Data = result; // Store the UserEntity in Data property
                }
                else
                {
                    response.Status = ApiStatus.Error;
                    response.Message = "Failed to save user";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.Status = ApiStatus.Error;
                response.Message = "Error occurred while inserting/updating user: " + ex.Message;
                response.Data = null;
            }

            return response;
        }
        public JsonResponseEntity InsertUpdateUserRolePermission(UserEntity users)
        {
            var response = new JsonResponseEntity();

            try
            {
                var authCommand = new SqlCommand("stpInsertUpdateUserRolePermissionMaster");
                authCommand.CommandType = CommandType.StoredProcedure;

                authCommand.Parameters.AddWithValue("@Id", users.Id);
                authCommand.Parameters.AddWithValue("@UserId", users.UserId);
                authCommand.Parameters.AddWithValue("@RoleId", users.RoleId);
                authCommand.Parameters.AddWithValue("@IsActive", users.IsActive);
                var result = _userRepository.GetRecord(authCommand);

                if (result != null)
                {
                    response.Status = ApiStatus.Success;
                    response.Message = "Role Permission saved successfully";
                    response.Data = result; // Store the UserEntity in Data property
                }
                else
                {
                    response.Status = ApiStatus.Error;
                    response.Message = "Failed to save user";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.Status = ApiStatus.Error;
                response.Message = "Error occurred while inserting/updating user: " + ex.Message;
                response.Data = null;
            }

            return response;
        }
        public List<JsonResponseEntity> GetUserRoleDetails()
        {
            var responses = new List<JsonResponseEntity>();

            try
            {
                using (var authCommand = new SqlCommand("stp_getuserroledetails"))
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
