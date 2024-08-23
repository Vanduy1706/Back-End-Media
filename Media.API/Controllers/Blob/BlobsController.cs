using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Media.API.Controllers.Blob
{
    [Route("files")]
    public class BlobsController : ApiController
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const string ContainerName = "mediacontainer";

        public BlobsController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobName = Guid.NewGuid().ToString();
            var blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            var blobUrl = blobClient.Uri.ToString();

            return Ok(new { Url = blobUrl, FileName = blobName });
        }


        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var downloadInfo = await blobClient.DownloadAsync();

            return File(downloadInfo.Value.Content, downloadInfo.Value.ContentType, fileName);
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> Delete(string fileName)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
                var blobClient = containerClient.GetBlobClient(fileName);

                await blobClient.DeleteIfExistsAsync();

                return Ok("File deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while deleting the file: {ex.Message}");
            }
        }

    }
}
