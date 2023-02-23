using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeployAWS.Application.Dtos
{
    public class ProductDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public decimal Value { get; set; }
        [JsonIgnore]
        public bool IsAvaiable { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}