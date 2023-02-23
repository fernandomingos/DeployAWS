using Amazon.Runtime.Internal.Auth;
using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceOrder : IApplicationServiceOrder
    {
        private const string QUEUE_NAME = "orders";
        private readonly IServiceOrder _serviceOrder;
        private readonly IMapper _mapper;
        private readonly ConnectionFactory _connectionFactory; 

        public ApplicationServiceOrder(IServiceOrder serviceOrder, IMapper mapper)
        {
            _serviceOrder = serviceOrder;
            _mapper = mapper;
            _connectionFactory = new ConnectionFactory() 
            { 
                HostName = "localhost",
                UserName = "admin",
                Password = "q1w2e3r4",
                VirtualHost = "/"
            };
        }
        public void Add(OrderDto orderDto)
        {
            orderDto.AddNewId();
            orderDto.AddCreateDate();
            orderDto.AddCreatedStatus();

            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: QUEUE_NAME,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

                        var bodyMessage = JsonConvert.SerializeObject(orderDto);
                        var byteArray = Encoding.UTF8.GetBytes(bodyMessage);

                        channel.BasicPublish(
                            exchange: string.Empty,
                            routingKey: QUEUE_NAME,
                            basicProperties: null,
                            body: byteArray
                            );
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDto orderDto)
        {
            throw new NotImplementedException();
        }
    }
}
