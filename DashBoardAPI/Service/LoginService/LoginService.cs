using Azure;
using DashBoardAPI.Entity;
using DashBoardAPI.Repository;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.Data.SqlClient;
using static DashBoardAPI.Entity.LoginEntity;

namespace DashBoardAPI.Service.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<LoginEntity> _loginRepository;
        private readonly IRepository<RolePermissionEntity> _permissionRepository;

        public LoginService(IRepository<LoginEntity> loginRepository, IRepository<RolePermissionEntity> permissionRepository)
        {
            _loginRepository = loginRepository;
            _permissionRepository = permissionRepository;
        }

        public JsonResponseEntity GetUserDetailsByEmailAndPassword(string emailId, string password)
        {
            var response = new JsonResponseEntity();

            try
            {
                // Step 1: Authenticate user
                var authCommand = new SqlCommand("stpAuthenticateUser");
                authCommand.CommandType = CommandType.StoredProcedure;
                authCommand.Parameters.AddWithValue("@EmailId", emailId);
                authCommand.Parameters.AddWithValue("@Password", password);

                var users = _loginRepository.GetRecord(authCommand);
                if (users == null)
                {
                    response.Status = ApiStatus.AccessDenied;
                    response.Message = "Invalid email or password.";
                    return response;
                }

                // Step 3: Fetch role-based permissions for the authenticated user
                var rolePermissionCommand = new SqlCommand("stpGetUserRolePermissions");
                rolePermissionCommand.CommandType = CommandType.StoredProcedure;
                rolePermissionCommand.Parameters.AddWithValue("@UserId", users.Id);

                var permissions = _permissionRepository.GetRecords(rolePermissionCommand).ToList();

                //var userModel = new
                //{
                //    User = users,
                //    Permissions = permissions
                //};
                var userModel = new
                {
                    User = new
                    {
                        users.Id,
                        users.UserName,
                        users.Email,
                        users.RoleId,
                        Permissions = permissions
                    }
                };



                response.Status = ApiStatus.OK;
                response.Message = "Login successful.";
                response.Data = userModel;
            }
            catch (Exception ex)
            {
                response.Status = ApiStatus.Error;
                response.Message = "Error occurred while logging in: " + ex.Message;
            }

            return response;
        }

        public JsonResponseEntity GetPermissionByUserId(Int64 UserId)
        {
            var response = new JsonResponseEntity();

            try
            {
                var rolePermissionCommand = new SqlCommand("stpGetUserRolePermissions");
                rolePermissionCommand.CommandType = CommandType.StoredProcedure;
                rolePermissionCommand.Parameters.AddWithValue("@UserId", UserId);
                var permissions = _permissionRepository.GetRecords(rolePermissionCommand).ToList();
                var userModel = new
                {           
                    Permissions = permissions
                };


                response.Status = ApiStatus.OK;
                response.Message = "Permission Fetched successful.";
                response.Data = userModel;

            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public List<JsonResponseEntity> GetAllPermissions()
        {
            var response = new List<JsonResponseEntity>();

            try
            {
                var rolePermissionCommand = new SqlCommand("stpGetAllPermissions");
                rolePermissionCommand.CommandType = CommandType.StoredProcedure;

                var permissions = _permissionRepository.GetRecords(rolePermissionCommand).ToList();
                foreach (var item in permissions)
                {
                    response.Add(new JsonResponseEntity
                    {
                        Status = ApiStatus.OK,
                        Message = "Permission fetched successfully.",
                        Data = item
                    });
                }
            }
            catch (Exception ex)
            {
                response.Add(new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Error occurred while fetching Permission: " + ex.Message,
                    Data = null
                });
            }

            return response;
        }
        public List<JsonResponseEntity> GetPermissionsByRoleId(Int64 RoleId)
        {
            var response = new List<JsonResponseEntity>();

            try
            {
                var rolePermissionCommand = new SqlCommand("stpGetUserRolePermissionsByRoleId");
                rolePermissionCommand.CommandType = CommandType.StoredProcedure;
                rolePermissionCommand.Parameters.AddWithValue("@RoleId", RoleId);
                var permissions = _permissionRepository.GetRecords(rolePermissionCommand).ToList();
                foreach (var item in permissions)
                {
                    response.Add(new JsonResponseEntity
                    {
                        Status = ApiStatus.OK,
                        Message = "Permission fetched successfully.",
                        Data = item
                    });
                }

            }
            catch (Exception ex)
            {
                response.Add(new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Error occurred while fetching Permission: " + ex.Message,
                    Data = null
                });
            }

            return response;
        }

        public List<JsonResponseEntity> SavePermissionsByRoleId(PermissionEntity entity)
        {
            var response = new List<JsonResponseEntity>();

            try
            {
                var rolePermissionCommand = new SqlCommand("stpInserUpdateRolePermissions");
                rolePermissionCommand.CommandType = CommandType.StoredProcedure;
                rolePermissionCommand.Parameters.AddWithValue("@RoleId", entity.RoleId);
                rolePermissionCommand.Parameters.AddWithValue("@PermissionIds", entity.PermissionIds);

                var permissions = _permissionRepository.GetRecords(rolePermissionCommand);

                response.Add(new JsonResponseEntity
                {
                    Status = ApiStatus.OK,
                    Message = "Permission added successfully.",
                    Data = permissions
                });              

            }
            catch (Exception ex)
            {
                response.Add(new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Error occurred while fetching Permission: " + ex.Message,
                    Data = null
                });
            }

            return response;
        }
    }
}
