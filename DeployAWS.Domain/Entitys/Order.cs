using System;

namespace DeployAWS.Domain.Entitys
{
    public class Order
    {
        public String Id { get; set; }
        public String Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Product Items { get; set; }
        public Customer Customer { get; set; }
    }
}
