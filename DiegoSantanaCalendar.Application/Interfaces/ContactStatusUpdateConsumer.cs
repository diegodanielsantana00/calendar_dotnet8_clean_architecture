using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using DiegoSantanaCalendar.Application.DTOs.Contact;
using DiegoSantanaCalendar.Domain.Interfaces;

namespace DiegoSantanaCalendar.Application.Interfaces
{
    public class ContactStatusUpdateConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _scopeFactory;
        private const string QueueName = "contact-status-updates";

        public ContactStatusUpdateConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);
                var updateDto = JsonSerializer.Deserialize<UpdateContactStatusDto>(messageJson);

                if (updateDto != null)
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var contactRepository = scope.ServiceProvider.GetRequiredService<IContactRepository>();

                        var contact = await contactRepository.GetByIdAsync(updateDto.Id);
                        if (contact != null)
                        {
                            contact.StatusContactEnum = updateDto.NewStatus;
                            await contactRepository.UpdateAsync(contact);
                        }
                    }
                }

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
