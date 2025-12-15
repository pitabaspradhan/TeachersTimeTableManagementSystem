namespace TeachersTimeTable.Application.Timetable;

public sealed class TimetableExtractionResult
{
    public IReadOnlyList<TimetableEntryDto> Entries { get; init; }
        = Array.Empty<TimetableEntryDto>();
}
