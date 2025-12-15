namespace TeachersTimeTable.Application.Ai;

public interface IAiClient
{
    Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken);
}
