namespace DashBoardAPI.Entity
{
    public class MasterModels
    {
        public class UserModel
        {
            public long UserId { get; set; }
            public string Name { get; set; } = "";
            public string EmailId { get; set; } = "";
            public string MobileNumber { get; set; } = "";
            public string Address { get; set; } = "";
            public bool IsActive { get; set; }
        }

        public class RoleModel
        {
            public long RoleId { get; set; }
            public string RoleName { get; set; } = "";
            public bool IsActive { get; set; }
        }

        public class PermissionModel
        {
            public long PermissionId { get; set; }
            public string PermissionKey { get; set; } = "";
            public string Description { get; set; } = "";
        }

        public class CustomerModel
        {
            public long CustomerId { get; set; }
            public string CustomerName { get; set; } = "";
            public string MobileNumber { get; set; } = "";
            public string Email { get; set; } = "";
            public string Address { get; set; } = "";
        }

        public class EngineerModel
        {
            public long EngineerId { get; set; }
            public string Name { get; set; } = "";
            public string Email { get; set; } = "";
            public string MobileNumber { get; set; } = "";
            public bool IsActive { get; set; }
        }
    }

}
