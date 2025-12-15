using TeachersTimeTable.Domain.ValueObjects;

namespace TeachersTimeTable.Domain.Entities;

public class TimeSlot
{
    public TimeRange TimeRange { get; }
    public string Subject { get; }

    public TimeSlot(TimeRange timeRange, string subject)
    {
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException("Subject cannot be empty");

        TimeRange = timeRange;
        Subject = subject.Trim();
    }
}
