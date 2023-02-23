using System;
using System.ComponentModel.DataAnnotations;

namespace DeployAWS.Application.Dtos
{
    public class LoginDto
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
