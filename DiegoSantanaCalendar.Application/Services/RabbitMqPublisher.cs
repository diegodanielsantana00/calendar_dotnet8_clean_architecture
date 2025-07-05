using DiegoSantanaCalendar.Application.DTOs.Contact;
using DiegoSantanaCalendar.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DiegoSantanaCalendar.Application.Services
{
    public class RabbitMqPublisher : IMessagePublisher
    {
        private readonly IConnection _connection;
        private const string QueueName = "contact-status-updates";

        public RabbitMqPublisher(IConfiguration configuration)
        {
            var rabbitMqHost = configuration["RabbitMQ:Host"];
            var factory = new ConnectionFactory() { HostName = rabbitMqHost };
            _connection = factory.CreateConnection();
        }

        public void PublishUpdateContactStatus(UpdateContactStatusDto message)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var jsonMessage = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: QueueName,
                                     basicProperties: properties,
                                     body: body);
            }
        }
    }
}
