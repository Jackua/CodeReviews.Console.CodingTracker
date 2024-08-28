using System.Reflection;

namespace CodingTracker.Jackua.Model;

internal class CodingSessionModel
{
    internal DateTime StartDateTime { get; init; }
    internal DateTime EndDateTime { get; init; }
    internal TimeSpan Duration { get { return EndDateTime.Subtract(StartDateTime); } }

    internal void Deconstruct(out string startDateTime, out string endDateTime, out string duration)
    {
        startDateTime = this.StartDateTime.ToString();
        endDateTime = this.EndDateTime.ToString();
        duration = this.Duration.ToString(string.Format("G"));
    }

}
