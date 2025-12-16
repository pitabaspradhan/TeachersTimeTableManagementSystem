using Microsoft.AspNetCore.Mvc;
using TeachersTimeTable.Application.Mapping;
using TeachersTimeTable.Application.Ocr;
using TeachersTimeTable.Application.Services;

namespace TeachersTimeTable.Api.Controllers;

[ApiController]
[Route("api/timetable")]
public sealed class TimetableController : ControllerBase
{
    private readonly IOcrService _ocrService;
    private readonly ITimetableExtractionService _extractionService;

    public TimetableController(
        IOcrService ocrService,
        ITimetableExtractionService extractionService)
    {
        _ocrService = ocrService;
        _extractionService = extractionService;
    }

    /// <summary>
    /// Extracts a timetable from an uploaded image, PDF, or DOCX file.
    /// </summary>
    [HttpPost("extract")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Extract(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is required.");

        // 1️st Run OCR
        await using var stream = file.OpenReadStream();
        var ocrResult = await _ocrService.ExtractAsync(
            stream,
            file.FileName,
            cancellationToken);

        // 2nd AI extraction (returns DTOs)
        var rows = await _extractionService.ExtractAsync(
            ocrResult,
            cancellationToken);

        // 3️rd  DTOs → Domain
       // var timetable = TimetableDomainMapper.MapToDomain(rows);

        // 4️th Return domain aggregate
        return Ok(rows);
    }
}
