using Api.Extensions;
using Application.Abstractions;
using Application.DTOs.Incoming;
using Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsService _resultsService;
        private readonly IValidator<ResultIncomingDto> _resultIncomingDtoValidator;
        private readonly IValidator<UpdateResultIncomingDto> _updateResultIncomingDtoValidator;

        public ResultsController(IResultsService resultsService,
            IValidator<ResultIncomingDto> resultIncomingDtoValidator,
            IValidator<UpdateResultIncomingDto> updateResultIncomingDtoValidator)
        {
            _resultsService = resultsService;
            _resultIncomingDtoValidator = resultIncomingDtoValidator;
            _updateResultIncomingDtoValidator = updateResultIncomingDtoValidator;
        }

        [HttpPost]
        //[Authorize(Roles = "Doctor")]
        public async Task<ActionResult> CreateResultAsync([FromBody] ResultIncomingDto incomingDto) 
        {
            var validationResult = await _resultIncomingDtoValidator.ValidateAsync(incomingDto);
            validationResult.HandleValidationResult();
            var resultId = await _resultsService.CreateAsync(incomingDto);
            return Created("", resultId);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Patient")]
        public async Task<ActionResult> GetResultByIdAsync(Guid id)
        {
            var result = await _resultsService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Doctor")]
        public async Task<ActionResult> UpdateResultAsync(Guid id, [FromBody] UpdateResultIncomingDto incomingDto)
        {
            var validationResult = await _updateResultIncomingDtoValidator.ValidateAsync(incomingDto);
            validationResult.HandleValidationResult();
            await _resultsService.UpdateAsync(id, incomingDto);
            return NoContent();
        }

        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> GetResultAsPdfAsync(Guid id)
        {
            var pdf = await _resultsService.GetAsPdfAsync(id);
            return new FileStreamResult(new MemoryStream(pdf), "application/pdf") 
            { 
                FileDownloadName = "result.pdf" 
            };
        }
    }
}
