namespace DashBoardAPI.Entity
{
    public class LoginEntity:BaseEntity
    {
           
       
          //  public Int64 Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public List<string> Roles { get; set; }
            public Dictionary<long, string> Permissions { get; set; }
            public string Token { get; set; }
            public long RoleId { get; set; }
            public string Password { get; set; }
       


    }
}
