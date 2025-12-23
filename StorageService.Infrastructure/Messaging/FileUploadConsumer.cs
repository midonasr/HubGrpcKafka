using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StorageService.Application.Commands;
using StorageService.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Infrastructure.Messaging
{
    public class FileUploadConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public FileUploadConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<string, FileUploadRequestedEvent>(
                new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "storage-upload",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                }).Build();

            consumer.Subscribe("file-upload-requested");

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);

                using var scope = _scopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await mediator.Send(new UploadFileCommand(
                    result.Message.Value.Filename,
                    result.Message.Value.Content
                ));
            }
        }
    }

}
