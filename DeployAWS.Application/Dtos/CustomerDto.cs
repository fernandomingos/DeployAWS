using System;
using System.ComponentModel.DataAnnotations;

namespace DeployAWS.Application.Dtos
{
    public class CustomerDto
    {
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
        public DateTime CreateDate { get; private set; }
        public bool IsActive { get; set; }

        public void AddNewId()
        {
            Id = Guid.NewGuid().ToString();
        }

        //public void AddCreateDate()
        //{
        //    CreateDate = DateTime.Now;
        //}
    }
}