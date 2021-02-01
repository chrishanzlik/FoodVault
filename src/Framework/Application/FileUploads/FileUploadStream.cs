using System;
using System.IO;

namespace FoodVault.Framework.Application.FileUploads
{
    /// <summary>
    /// Represents a <see cref="Stream"/> class with additional file upload meta data.
    /// </summary>
    public class FileUploadStream : Stream
    {
        readonly Stream _innerStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadStream" /> class.
        /// </summary>
        /// <param name="innerStream">File stream</param>
        /// <param name="fileName">New name of the file.</param>
        /// <param name="contentType">Content type of the file.</param>
        public FileUploadStream(Stream innerStream, string fileName, string contentType)
        {
            _innerStream = innerStream ?? throw new ArgumentNullException(nameof(innerStream));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName)); ;
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType)); ;
        }

        /// <summary>
        /// Gets the file name of the target stream.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets or the content type of the target stream.
        /// </summary>
        public string ContentType { get; }

        /// <inheritdoc />
        public override bool CanRead => _innerStream.CanRead;

        /// <inheritdoc />
        public override bool CanSeek => _innerStream.CanSeek;

        /// <inheritdoc />
        public override bool CanWrite => _innerStream.CanWrite;

        /// <inheritdoc />
        public override long Length => _innerStream.Length;

        /// <inheritdoc />
        public override long Position { get => _innerStream.Position; set => _innerStream.Position = value; }

        /// <inheritdoc />
        public override void Flush() => _innerStream.Flush();

        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count) => _innerStream.Read(buffer, offset, count);

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin) => _innerStream.Seek(offset, origin);

        /// <inheritdoc />
        public override void SetLength(long value) => _innerStream.SetLength(value);

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count) => _innerStream.Write(buffer, offset, count);

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _innerStream.Dispose();

            base.Dispose(disposing);
        }
    }
}
