using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Infrastructure.Messaging
{
    public static class KafkaProducerFactory
    {
        public static IProducer<string, T> Create<T>(
            IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };

            return new ProducerBuilder<string, T>(config).Build();
        }
    }
}
