namespace TeachersTimeTable.Application.TimetableExtraction;

public sealed class TimetableExtractionResult
{
    public IReadOnlyList<TimetableEntryDto> Entries { get; init; }
        = Array.Empty<TimetableEntryDto>();
}
