using Jinhe.CoreFx.Extensions;
using Jinhe.Storage.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jinhe.Storage.Api
{
    [Route("api/streaming")]
    public class StreamingController : Controller
    {
        private readonly IFileStorageProvider _fileStorageProvider;
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public StreamingController()
        {
            _fileStorageProvider = new FileStorageProvider($"{ Directory.GetCurrentDirectory()}", "upload");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Upload()
        {

            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            // 用于在请求中累积所有表单URL编码的键值对
            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = null;

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            //var bindingSuccessful = false;
            var section = await reader.ReadNextSectionAsync();

            List<Impl.StoredFile> archives = new List<Impl.StoredFile>();
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await section.Body.CopyToAsync(ms);
                            //TODO:此MD5无效，全是相同的值
                            var md5 = ms.ComputeHash();
                            md5 = Guid.NewGuid().ToString("N");
                            var archive = new StoredFile(contentDisposition.FileName, md5);
                            targetFilePath = _fileStorageProvider.GetStoragePhysicalPath(archive);
                            archives.Add(archive);
                            var directoryName = Path.GetDirectoryName(targetFilePath);
                            if (!Directory.Exists(directoryName))
                            {
                                Directory.CreateDirectory(directoryName);
                            }
                            using (var targetStream = System.IO.File.Create(targetFilePath))
                            {
                                await ms.CopyToAsync(targetStream);
                            }
                        }
                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {
                        // Content-Disposition: form-data; name="key"
                        //
                        // value

                        // 不用在这里限制key名称的长度
                        // 因为 multipart headers  长度限制已经生效
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name).ToString();
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // 值长度限制强制执行为MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key, value);

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }
            return Json(archives);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="d">是否下载</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public FileResult Get(string id, string title, bool d = false)
        {

            var response = new HttpResponseMessage();
            var storedFile = new StoredFile(id);
            if (string.IsNullOrEmpty(title))
                title = storedFile.Md5;
            if (d == true)
            {
                return File(_fileStorageProvider.GetFileStream(storedFile), storedFile.MimeType.Type, $"{title}{storedFile.MimeType.Ext}");
            }
            else
            {
                var etag = new StringSegment($"\"{ storedFile.Md5}\"");
                return File(_fileStorageProvider.GetFileStream(storedFile), storedFile.MimeType.Type, DateTimeOffset.Now.AddDays(20), new EntityTagHeaderValue(etag));
            }
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }

        /// <summary>
        /// 获取PDF文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="d">是否下载</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/pdf")]
        [AllowAnonymous]
        public FileResult GetPDF(string id, string title, bool d = false)
        {
            var response = new HttpResponseMessage();
            var storedFile = new StoredFile(id);
            if (string.IsNullOrEmpty(title))
                title = storedFile.Md5;
            string path = _fileStorageProvider.GetStoragePhysicalPath(storedFile, Dto.FileType.pdf);
            FileStream fileStream = null;
            if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            if (d == true)
            {
                return File(fileStream, storedFile.MimeType.Type, $"{title}.pdf");
            }
            else
            {
                var etag = new StringSegment($"\"{ storedFile.Md5}\"");
                return File(fileStream, storedFile.MimeType.Type, DateTimeOffset.Now.AddDays(20), new EntityTagHeaderValue(etag));
            }
        }
    }


}
