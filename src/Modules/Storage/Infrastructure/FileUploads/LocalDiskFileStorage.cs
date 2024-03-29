﻿using FoodVault.Framework.Application.FileUploads;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.FileUploads
{
    /// <summary>
    /// File interaction (save/load) with the local disk.
    /// </summary>
    internal class LocalDiskFileStorage : IFileStorage
    {
        private readonly IFileUploadSettings _fileUploadSettings;
        private readonly IFileUploadRepository _fileUploadRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalDiskFileStorage" /> class.
        /// </summary>
        /// <param name="fileUploadSettings">File upload settings.</param>
        /// <param name="fileUploadRepository">File upload repository.</param>
        public LocalDiskFileStorage(
            IFileUploadSettings fileUploadSettings,
            IFileUploadRepository fileUploadRepository)
        {
            _fileUploadSettings = fileUploadSettings;
            _fileUploadRepository = fileUploadRepository;
        }

        /// <inheritdoc />
        public async Task DeleteExpiredFilesAsync()
        {
            var expiredUploadInfos = await _fileUploadRepository.GetExpiredFilesAsync();
            
            foreach(var expiredFile in expiredUploadInfos)
            {
                var path = Path.Combine(_fileUploadSettings.RootFolder, expiredFile.RelativeFileLocation);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                await _fileUploadRepository.RemoveAsync(expiredFile.Id);
            }
        }

        /// <inheritdoc />
        public async Task DeleteFileAsync(Guid id)
        {
            var uploadInfo = await _fileUploadRepository.GetByIdAsync(id);

            if (uploadInfo != null)
            {
                var path = Path.Combine(_fileUploadSettings.RootFolder, uploadInfo.RelativeFileLocation);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                await _fileUploadRepository.RemoveAsync(id);
            }
        }

        /// <inheritdoc />
        public async Task<FileUploadStream> GetFileAsync(Guid id, string newFileName = null)
        {
            var uploadInfo = await _fileUploadRepository.GetByIdAsync(id);
            if (uploadInfo == null)
            {
                return null;
            }

            var path = Path.Combine(_fileUploadSettings.RootFolder, uploadInfo.RelativeFileLocation);
            var stream = File.OpenRead(path);

            newFileName ??= id.ToString();

            return new FileUploadStream(stream, $"{newFileName}.{uploadInfo.Extension}", uploadInfo.ContentType);
        }

        /// <inheritdoc />
        public Task PersistFileAsync(Guid id)
        {
            return _fileUploadRepository.PersistFileAsync(id);
        }

        /// <inheritdoc />
        public async Task<Guid> StoreFileTemporaryAsync(Stream fileStream, string fileName, string contentType, TimeSpan expirationTime)
        {
            if (fileStream == null)
            {
                throw new ArgumentNullException(nameof(fileStream));
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"Parameter '{nameof(fileName)}' cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(contentType))
            {
                throw new ArgumentException($"Parameter '{nameof(contentType)}' cannot be null or empty.");
            }

            var fileId = Guid.NewGuid();
            var extension = Path.GetExtension(fileName)[1..];
            var utcNow = DateTime.UtcNow;
            var relativeFolder = Path.Combine($"{utcNow.Year}_{utcNow.Month}");
            var absoluteFolder = Path.Combine(_fileUploadSettings.RootFolder, relativeFolder);

            ValidateUpload(fileStream, extension);

            await WriteUploadStreamAsync(absoluteFolder, fileId, fileStream);

            var upload = new FileUpload(fileId, Path.Combine(relativeFolder, fileId.ToString()), extension, contentType, fileStream.Length, utcNow.Add(expirationTime));

            await _fileUploadRepository.AddAsync(upload);

            return upload.Id;
        }

        private void ValidateUpload(Stream stream, string extension)
        {
            var validExtensions = _fileUploadSettings.AllowedExtensions.Select(x => x.ToLower());
            var ext = extension.ToLower();

            if (!validExtensions.Contains(ext))
            {
                throw new UploadFileException($"Files with the extension '{ext}' are not supported.");
            }

            var sizeInMb = stream.Length / 1024 / 1024;

            if (sizeInMb > _fileUploadSettings.MaximumFileSize)
            {
                throw new UploadFileException($"The file is too large. The maximum upload size is {_fileUploadSettings.MaximumFileSize} MB.");
            }
        }

        private async Task WriteUploadStreamAsync(string folder, Guid id, Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fullFileName = Path.Combine(folder, id.ToString());
            using var fs = File.Create(fullFileName);
            await stream.CopyToAsync(fs);
        }
    }
}
