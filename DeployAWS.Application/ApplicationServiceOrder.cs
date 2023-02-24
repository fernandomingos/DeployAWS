using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceOrder : IApplicationServiceOrder
    {
        private readonly IMapper _mapper;
        private readonly IServiceRabbitMQ _serviceRabbitMQ;
        private readonly ILogger<ApplicationServiceOrder> _logger;

        public ApplicationServiceOrder(IServiceRabbitMQ serviceRabbitMQ, IMapper mapper, ILogger<ApplicationServiceOrder> logger)
        {
            _serviceRabbitMQ = serviceRabbitMQ;
            _mapper = mapper;
            _logger = logger;
        }

        public void Add(OrderDto orderDto)
        {
            _logger.LogInformation($"##### Executando request Add => ApplicationServiceOrder order.id: {orderDto.Id} #####");
            orderDto.AddNewId();
            orderDto.AddCreateDate();
            orderDto.AddCreatedStatus();

            var order = _mapper.Map<Order>(orderDto);
            var message = JsonConvert.SerializeObject(order);

            _serviceRabbitMQ.Producer(message);
        }

        public IEnumerable<OrderDto> Get()
        {
            _logger.LogInformation($"##### Executando request Get => ApplicationServiceOrder #####");
            var message = _serviceRabbitMQ.Consumer();

            var orderDTO = JsonConvert.DeserializeObject<OrderDto>(message);

            return new List<OrderDto>();
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
