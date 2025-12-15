using TeachersTimeTable.Application.Ai;
using TeachersTimeTable.Domain.Entities;
using TeachersTimeTable.Domain.Enums;
using TeachersTimeTable.Domain.ValueObjects;

namespace TeachersTimeTable.Application.Mapping;

public static class TimetableDomainMapper
{
    public static Timetable MapToDomain(
        IEnumerable<TimetableRowAiResult> rows)
    {
        var timetable = new Timetable();

        foreach (var row in rows)
        {
            var day = Enum.Parse<WeekDay>(row.Day, ignoreCase: true);

            var start = TimeOnly.Parse(row.StartTime);
            var end = TimeOnly.Parse(row.EndTime);

            var slot = new TimeSlot(
                new TimeRange(start, end),
                row.Subject);

            timetable
                .GetOrCreateDay(day)
                .AddSlot(slot);
        }

        return timetable;
    }
}
