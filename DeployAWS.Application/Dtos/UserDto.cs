using System;

namespace DeployAWS.Application.Dtos
{
    public class UserDto
    {
        public String Id { get; set; }
        public String UserName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }
        public String Profile { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
