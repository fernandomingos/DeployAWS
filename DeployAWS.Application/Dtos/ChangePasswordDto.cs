using System;
using System.ComponentModel.DataAnnotations;

namespace DeployAWS.Application.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public String ActualPassword { get; set; }
        [Required]
        public String NewPassword { get; set; }
        [Required]
        public String ConfirmNewPassword { get; set; }
    }
}
