﻿using System.ComponentModel.DataAnnotations;

namespace DeployAWS.Application.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        [Required]
        public string Email { get; set; }
    }
}