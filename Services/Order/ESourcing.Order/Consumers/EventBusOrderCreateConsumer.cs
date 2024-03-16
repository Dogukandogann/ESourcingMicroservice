using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands.OrderCreate;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ESourcing.Order.Consumers
{
    public class EventBusOrderCreateConsumer
    {
        private readonly IRabbitMQPersistentConnection _rabbitMQPersistentConnection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventBusOrderCreateConsumer(IRabbitMQPersistentConnection rabbitMQPersistentConnection, IMediator mediator, IMapper mapper)
        {
            _rabbitMQPersistentConnection = rabbitMQPersistentConnection;
            _mediator = mediator;
            _mapper = mapper;
        }
        
        public void Consume()
        {
            if (!_rabbitMQPersistentConnection.IsConnected)
            {
                _rabbitMQPersistentConnection.TryConnect();
            }

            var channel = _rabbitMQPersistentConnection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstant.OrderCreateQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ReceivedEvent;
            channel.BasicConsume(queue: EventBusConstant.OrderCreateQueue,autoAck:true,consumer:consumer);

        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var @event = JsonConvert.DeserializeObject<OrderCreateEvent>(message);
            if (e.RoutingKey.Equals(EventBusConstant.OrderCreateQueue)) 
            {
                var command = _mapper.Map<OrderCreateCommand>(@event);
                command.CreatedAt = DateTime.Now;
                command.TotalPrice = (@event.Quantity * @event.Price);
                command.UnitPrice = @event.Price;
                var result = await _mediator.Send(command);
            }
        }
        public void Disconnect()
        {
            _rabbitMQPersistentConnection.Dispose();
        }
    }
}
