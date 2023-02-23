using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeployAWS.Application.Dtos
{
    public class CustomerDto
    {
        [JsonIgnore]
        public String Id { get; private set;  }
        [Required]
        public String UserName { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String EmailAddress { get; set; }
        [Required]
        public String Profile { get; set; }
        [Required]
        public String Password { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public bool IsActive { get; set; }

        public void AddNewId() =>
            Id = Guid.NewGuid().ToString();

        public void AddCreateDate() =>
            CreateDate = DateTime.Now;

        public void AddModifiedDate() =>
            ModifiedDate = DateTime.Now;

        public void ChangePassword(string password) => 
            Password = password;

    }
}