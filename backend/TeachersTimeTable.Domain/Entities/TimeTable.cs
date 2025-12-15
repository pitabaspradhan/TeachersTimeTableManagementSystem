namespace TeachersTimeTable.Domain.Entities;

public class Timetable
{
    public Guid Id { get; } = Guid.NewGuid();

    private readonly List<DaySchedule> _days = new();
    public IReadOnlyCollection<DaySchedule> Days => _days.AsReadOnly();

    public void AddDay(DaySchedule daySchedule)
    {
        if (_days.Any(d => d.Day == daySchedule.Day))
            throw new InvalidOperationException(
                $"Duplicate day schedule for {daySchedule.Day}");

        _days.Add(daySchedule);
    }

    public DaySchedule GetOrCreateDay(Enums.WeekDay day)
    {
        var existing = _days.SingleOrDefault(d => d.Day == day);
        if (existing != null) return existing;

        var newDay = new DaySchedule(day);
        _days.Add(newDay);
        return newDay;
    }
}
