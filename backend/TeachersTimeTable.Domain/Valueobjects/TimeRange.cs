using System;

namespace TeachersTimeTable.Domain.ValueObjects;

public sealed record TimeRange
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        if (end <= start)
            throw new ArgumentException("End time must be after start time", nameof(end));

        Start = start;
        End = end;
    }
}
