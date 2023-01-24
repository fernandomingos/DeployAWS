using System.ComponentModel.DataAnnotations;

namespace DeployAWS.Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal Valor { get; set; }
    }
}