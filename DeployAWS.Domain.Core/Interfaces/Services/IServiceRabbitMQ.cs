using DeployAWS.Domain.Entitys;
using System;

namespace DeployAWS.Domain.Core.Interfaces.Services
{
    public interface IServiceRabbitMQ
    {
        String Consumer();
        void Producer(String message);
    }
}
