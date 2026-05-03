using docs_garage_api.Interface;
using docs_garage_api.Modal;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace docs_garage_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly IMergePdfService _mergePdfService;
        private readonly IFileValidationService _validationService;
        public PdfController(IMergePdfService mergePdfService, IFileValidationService validationService)
        {
            _mergePdfService = mergePdfService;
            _validationService = validationService;
        }

        [HttpPost("merge")]
        public async Task<IActionResult> Merge([FromForm] List<IFormFile> files)
        {
            try
            {
                var validationResult = _validationService.ValidatePdfFiles(files);

                if (!validationResult.IsValid)
                {
                    var result = await _mergePdfService.MergeAsync(files);

                    return Ok(new Response() { status = HttpStatusCode.OK, message = CommonHelper.Success, data = JsonConvert.SerializeObject(result) });
                } 
                else
                { 
                    return StatusCode((int)HttpStatusCode.Forbidden, new Response() { status = HttpStatusCode.Forbidden, message = CommonHelper.Failed,data = JsonConvert.SerializeObject(validationResult) });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, new Response() { status = HttpStatusCode.Forbidden, message = CommonHelper.Failed, data = ex.ToString() });
            }           
        }
    }
}
