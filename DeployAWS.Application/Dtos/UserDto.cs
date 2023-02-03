using System;

namespace DeployAWS.Application.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Profile { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
