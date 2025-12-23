using StorageService.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Messaging
{
    public interface IUploadEventProducer
    {
        Task PublishAsync(FileUploadRequestedEvent @event);
    }
}
