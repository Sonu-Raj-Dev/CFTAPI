namespace DashBoardAPI.Entity
{
    public class EmailEntity:BaseEntity
    {
        public Int64 ComplaintId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailFrom {  get; set; }
          public string EmailTo { get; set; }
        public string Host {  get; set; }
        public string Password { get; set; }
        public int Port { get; set; }

        public string UserName { get; set; }

    }
}
