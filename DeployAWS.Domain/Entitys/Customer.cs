using System;

namespace DeployAWS.Domain.Entitys
{
    public class Customer : User
    {
        public bool IsActive { get; set; }
    }
}