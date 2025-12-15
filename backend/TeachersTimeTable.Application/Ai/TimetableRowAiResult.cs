namespace TeachersTimeTable.Application.Ai;

public sealed record TimetableRowAiResult
{
    public string Day { get; init; } = string.Empty;
    public string Subject { get; init; } = string.Empty;
    public string StartTime { get; init; } = string.Empty;
    public string EndTime { get; init; } = string.Empty;
}
