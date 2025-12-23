using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Events
{
    public class FileUploadRequestedEvent
    {
        public string Id { get; init; } = default!;
        public string Filename { get; init; } = default!;
        public byte[] Content { get; init; } = default!;
    }
}
