using Confluent.Kafka;
using StorageService.Application.Events;
using StorageService.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Infrastructure.Messaging
{
    public class KafkaUploadProducer : IUploadEventProducer
    {
        private readonly IProducer<string, FileUploadRequestedEvent> _producer;

        public KafkaUploadProducer(IProducer<string, FileUploadRequestedEvent> producer)
        {
            _producer = producer;
        }

        public async Task PublishAsync(FileUploadRequestedEvent @event)
        {
            await _producer.ProduceAsync(
                "file-upload-requested",
                new Message<string, FileUploadRequestedEvent>
                {
                    Key = @event.Id,
                    Value = @event
                });
        }
    }

}
