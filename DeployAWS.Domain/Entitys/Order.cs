using System;
using System.Collections.Generic;

namespace DeployAWS.Domain.Entitys
{
    public class Order
    {
        public String Id { get; set; }
        public ICollection<Product> Items { get; set; }
        public String Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; private set; }
    }
}
