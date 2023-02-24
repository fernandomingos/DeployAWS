using DeployAWS.Domain.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;

namespace DeployAWS.Domain.Services
{
    public class ServiceRabbitMQ : IServiceRabbitMQ
    {
        private const string QUEUE_NAME = "orders";
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ServiceRabbitMQ> _logger;

        public ServiceRabbitMQ(IConfiguration configuration, ILogger<ServiceRabbitMQ> logger) 
        {
            _configuration = configuration;
            _logger = logger;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = _configuration.GetValue<string>("RabbitMQ:HostName"),
                UserName = _configuration.GetValue<string>("RabbitMQ:UserName"),
                Password = _configuration.GetValue<string>("RabbitMQ:Password"),
                VirtualHost = _configuration.GetValue<string>("RabbitMQ:VirtualHost")
            };
        }

        public String Consumer()
        {
            _logger.LogInformation("##### Enviando requisição Consumer => ServiceRabbitMQ #####");

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


                    var data = channel.BasicGet(QUEUE_NAME, true);

                    if (data == null)
                        return string.Empty;

                    var messageJSON = Encoding.UTF8.GetString(data.Body.ToArray());
                    return messageJSON;      
                }
            }
        }

        public void Producer(String message)
        {
            _logger.LogInformation($"##### Enviando requisição Producer => ServiceRabbitMQ #####");
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

                    var byteArray = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: byteArray
                        );
                }
            }
        }
    }
}
