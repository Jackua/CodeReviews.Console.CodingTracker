namespace CodingTracker.Jackua.Model;

internal class CodingSessionModel
{
    internal int Id { get; init; }
    internal DateTime StartDateTime { get; init; }
    internal DateTime EndDateTime { get; init; }
    internal TimeSpan Duration { get; }

    internal CodingSessionModel(int id, DateTime startTime, DateTime endTime)
    {
        Id = id;
        StartDateTime = startTime;
        EndDateTime = endTime;
        Duration = StartDateTime.Subtract(EndDateTime);
    }
}
