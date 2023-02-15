using System;

namespace DeployAWS.Domain.Entitys
{
    public class User : Base
    {
        public String UserName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }
        public String Profile { get; set; }
        public String Password { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
