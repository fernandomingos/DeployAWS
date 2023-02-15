using DeployAWS.Domain.Entitys;
using System;

namespace DeployAWS.Application.Dtos
{
    public class OrderDto
    {
        public String Id { get; set; }
        public String Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Product Items { get; set; }
        public Customer Customer { get; set; }
    }
}
