using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using hello.transaction.core.Services;
using hello.transaction.core.Models;
using hello.transaction.core.Exceptions;
using System.IO;
using hello.transaction.core.Extensions;

namespace hello.transaction.api.Controllers
{
    //[Authorize]
    //[ServiceFilter(typeof(EnsureUserAuthorizeInAsync))]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        //private readonly ICacheService _cacheService;
        private readonly IAttachmentService _attachmentService;
        public AttachmentController(IAttachmentService attachmentService) //, ICacheService cacheService)
        {
            _attachmentService = attachmentService ?? throw new ArgumentNullException(nameof(attachmentService));
            //_cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        //
        // Summary:
        //      create an attachment by file form data
        //
        // Returns:
        //      post attachment from db
        //
        // Params:
        //      attachment model with base64 encoding content
        //
        //[EnableCors("AllowCors")]
        [Route("upload")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Attachment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Attachment>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadAttachmentAsync(IFormCollection formdata)
        {
            //await Task.Delay(0);
            var files = HttpContext.Request.Form.Files;
            var file = files.FirstOrDefault();
            if (file == null)
            {
                throw new AttachmentNotValidException();
            }

            //var info = new FileInfo(file);
            var attachment = new Attachment()
            {
                Name = file.FileName,
                //convert to kb
                Size = file.Length / 1024,
                Extension = Path.GetExtension(file.FileName),
                Base64Content = file.OpenReadStream().ReadToEnd()
            };

            var entity = await _attachmentService.CreateAsync(attachment);
            return Ok(entity);
        }

        // Summary:
        //      return basic list of attachment
        //
        // Returns:
        //     list of Attachment
        //
        //[EnableCors("AllowCors")]
        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Attachment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Attachment>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListAsync()
        {
            var entities = await _attachmentService.ListAsync();
            return Ok(entities);
        }

        //
        // Summary:
        //      create an attachment by base64content 
        //
        // Returns:
        //      post attachment from db
        //
        // Params:
        //      attachment model with base64 encoding content
        //
        //[EnableCors("AllowCors")]
        [Route("post")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Attachment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Attachment>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAttachmentAsync(Attachment attachment)
        {
            var entity = await _attachmentService.CreateAsync(attachment);
            return Ok(entity);
        }

    }
}