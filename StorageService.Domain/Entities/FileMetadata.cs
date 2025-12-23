
namespace StorageService.Domain.Entities
{
    public class FileMetadata
    {
        public string Id { get; private set; }
        public string Filename { get; private set; }
        public long Size { get; private set; }
        public DateTime UploadedAt { get; private set; }
        public bool IsInfected { get; private set; }

        private FileMetadata(string id, string filename, long size)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id cannot be empty.", nameof(id));

            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException("Filename cannot be empty.", nameof(filename));

            if (size < 0)
                throw new ArgumentException("File size cannot be negative.", nameof(size));

            Id = id;
            Filename = filename;
            Size = size;
            UploadedAt = DateTime.UtcNow;
            IsInfected = false;
        }

        // Factory when storage generates ID
        public static FileMetadata CreateWithId(string id, string filename, long size)
            => new FileMetadata(id, filename, size);

        // Factory when domain generates ID
        public static FileMetadata Create(string filename, long size)
            => new FileMetadata(Guid.NewGuid().ToString(), filename, size);

        public void MarkAsInfected() => IsInfected = true;

        public void Rename(string newFilename)
        {
            if (string.IsNullOrWhiteSpace(newFilename))
                throw new ArgumentException("Filename cannot be empty.", nameof(newFilename));
            Filename = newFilename;
        }

        // Internal for EF rehydration
        internal FileMetadata(string id, string filename, long size, DateTime uploadedAt, bool isInfected)
        {
            Id = id;
            Filename = filename;
            Size = size;
            UploadedAt = uploadedAt;
            IsInfected = isInfected;
        }
    }
}