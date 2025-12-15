using TeachersTimeTable.Domain.Enums;

namespace TeachersTimeTable.Domain.Entities;

public class DaySchedule
{
    public WeekDay Day { get; }

    private readonly List<TimeSlot> _slots = new();
    public IReadOnlyCollection<TimeSlot> Slots => _slots.AsReadOnly();

    public DaySchedule(WeekDay day)
    {
        Day = day;
    }

    public void AddSlot(TimeSlot slot)
    {
        // Domain rule: no overlapping slots
        if (_slots.Any(existing =>
            existing.TimeRange.Start < slot.TimeRange.End &&
            slot.TimeRange.Start < existing.TimeRange.End))
        {
            throw new InvalidOperationException(
                $"Time slot overlaps on {Day}");
        }

        _slots.Add(slot);
    }
}
