using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeployAWS.Application.Dtos
{
    public class OrderDto
    {
        [JsonIgnore]
        public String Id { get; set; }
        [Required]
        public ICollection<ProductDto> Items { get; set; }
        [JsonIgnore]
        public String Status { get; private set; }
        [JsonIgnore]
        public DateTime CreateDate { get; private set; }
        [JsonIgnore]
        public DateTime ModifiedDate { get; private set; }

        public void AddNewId()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void AddCreateDate()
        {
            CreateDate = DateTime.Now;
        }

        public void AddModifiedDate()
        {
            CreateDate = DateTime.Now;
        }

        public void AddCreatedStatus()
        {
            Status = "Created";
        }
    }
}
