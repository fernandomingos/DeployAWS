using System.ComponentModel.DataAnnotations;

namespace DeployAWS.Application.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Value { get; set; }
        public bool IsAvaiable { get; set; }
    }
}