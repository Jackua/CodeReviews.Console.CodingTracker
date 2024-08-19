namespace CodingTracker.Jackua.Model;

internal class CodingSessionModel
{
    internal int Id { get; init; }
    internal DateTime StartTime { get; init; }
    internal DateTime EndTime { get; init; }
    internal TimeSpan Duration { get; }

    internal CodingSessionModel(int id, DateTime startTime, DateTime endTime)
    {
        Id = id;
        StartTime = startTime;
        EndTime = endTime;
        Duration =StartTime.Subtract(EndTime);
    }
}
