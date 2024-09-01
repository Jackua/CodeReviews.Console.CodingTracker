using CodingTracker.Jackua.Database;
using CodingTracker.Jackua.View;
using CodingTracker.Jackua.Model;
using System.Globalization;
namespace CodingTracker.Jackua.Controller;

internal class Controller
{
    internal void Run()
    {
        do
        {
            View.View.MainMenu();

            string input = Console.ReadLine()!;

            switch (input)
            {
                case "0":
                    View.View.Exit();
                    Environment.Exit(0);
                    break;
                case "1":
                    Console.Clear();
                    GetAllRecords();
                    break;
                case "2":
                    InsertRecord();
                    break;
                case "3":
                    DeleteRecord();
                    break;
                case "4":
                    UpdateRecord();
                    break;
                case "5":
                    GetSummary();
                    break;
                default:
                    View.View.InvalidCommand();
                    break;
            }
        } while (true);
    }

    internal void GetDateTime(string timeFrame, out DateTime dateTime, out string input)
    {
        View.View.DateTimeRequest(timeFrame);
        dateTime = DateTime.Now;
        while (true)
        {
            try
            {
                input = Console.ReadLine()!;
                if (input == "-1")
                {
                    break;
                }
                dateTime = DateTime.ParseExact(input, "M/d/yyyy h:mm:ss tt", new CultureInfo("en-us"));
                break;
            }
            catch (System.FormatException)
            {
                View.View.Incorrect();
            }
        }
    }

    internal int GetId(string action)
    {
        View.View.IdRequest(action);
        int id;
        while (true)
        {
            try
            {
                id = Int32.Parse(Console.ReadLine()!);
                break;
            }
            catch
            {
                View.View.Incorrect();
            }
        }
        return id;
    }

    internal void GetAllRecords()
    {
        List<CodingSessionModel> records = Database.Database.GetRecords();
        if (records.Count == 0)
        {
            View.View.NoRows();
        }
        else
        {
            View.View.DisplayAllRecords(records);
        }
    }

    internal void InsertRecord()
    {
        Console.Clear();
        View.View.InsertInstructions();
        GetDateTime("start", out DateTime startDateTime, out string startInput);
        if (startInput == "-1") return;
        GetDateTime("start", out DateTime endDateTime, out string endInput);
        if (endInput == "-1") return;
        CodingSessionModel newRecord = new CodingSessionModel { StartDateTime = startDateTime, EndDateTime = endDateTime };
        if (Database.Database.InsertRecord(newRecord) == 1)
        {
            View.View.Successful("inserted");
        }
        else
        {
            View.View.Unsuccessful("inserted");
        }
    }

    internal void DeleteRecord()
    {
        Console.Clear();
        GetAllRecords();
        int id = GetId("delete");
        if (id == -1) return;
        if (Database.Database.DeleteRecord(id) == 1)
        {
            View.View.Successful(id, "deleted");
        }
        else
        {
            View.View.Unsuccessful(id, "deleted");
        }
    }

    internal void UpdateRecord()
    {
        Console.Clear();
        GetAllRecords();
        int id = GetId("update");
        if (id == -1) return;
        GetDateTime("start", out DateTime startDateTime, out string startInput);
        if (startInput == "-1") return;
        GetDateTime("start", out DateTime endDateTime, out string endInput);
        if (endInput == "-1") return;
        if (Database.Database.UpdateRecord(id, new CodingSessionModel { StartDateTime = startDateTime, EndDateTime = endDateTime}) == 1)
        {
            View.View.Successful(id, "updated");
        }
        else
        {
            View.View.Unsuccessful(id, "updated");
        }
    }

    internal void GetSummary()
    {
        Console.Clear();
        GetAllRecords();
        View.View.SummaryInstruction();
        View.View.SummaryRequest("start datetime");
        string startDateTime = Console.ReadLine()!;
        View.View.SummaryRequest("end datetime");
        string endDateTime = Console.ReadLine()!;
        View.View.SummaryRequest("duration");
        string duration = Console.ReadLine()!;
        var records = Database.Database.GetRecordsBy(startDateTime, endDateTime, duration);
        TimeSpan total = new TimeSpan(0, 0, 0, 0);
        foreach (var record in records)
        {
            total = total.Add(record.Duration);
        }

        TimeSpan average = records.Count > 0 ? total / records.Count : new TimeSpan(0, 0, 0, 0);
        View.View.Summary(records.Count, total, average);
    }
}
