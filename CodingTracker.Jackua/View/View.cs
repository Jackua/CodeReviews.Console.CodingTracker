using CodingTracker.Jackua.Model;

namespace CodingTracker.Jackua.View;

internal static class View
{
    internal static void MainMenu()
    {
        Console.WriteLine("\nMAIN MENU");
        Console.WriteLine("\nWhat would you like to do?");
        Console.WriteLine("\nEnter 0 to Exit The Application.");
        Console.WriteLine("Enter 1 to View All Records.");
        Console.WriteLine("Enter 2 to Insert A Record");
        Console.WriteLine("Enter 3 to Delete A Record");
        Console.WriteLine("Enter 4 to Update A Record");
        Console.WriteLine("Enter 5 to View A Summary of The Records");
        DashLines();
    }

    internal static void InvalidCommand()
    {
        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
    }

    internal static void DateTimeRequest(string timeFrame)
    {
        Console.WriteLine($"\nPlease enter a {timeFrame} datetime in the format 1/1/1999 12:59:59 PM or -1 to exit to the main menu.\n");
    }

    internal static void IdRequest(string action)
    {
        Console.WriteLine($"\nPlease enter the id of the record you would like to {action} or -1 to exit to the main menu.\n");
    }

    internal static void DisplayAllRecords(List<CodingSessionModel> records)
    {
        foreach (var record in records)
        {
            Console.WriteLine($"Id: {record.Id, 2} StartDateTime: {record.StartDateTime, 23} EndDateTime: {record.EndDateTime, 23} Duration: {record.Duration.ToString(string.Format("G"))}");
        }
    }

    internal static void InsertInstructions()
    {
        Console.WriteLine("\nPlease enter the start and end datetime of your coding session or -1 to exit to the main menu.\n");
    }

    internal static void SummaryInstruction()
    {
        Console.WriteLine("\nPlease enter the duration or the start, end datetime you would like a summary of.\n");
    }

    internal static void SummaryRequest(string parameter)
    {
        Console.WriteLine($"Please enter the {parameter} to filter by, use % and _ for pattern matching or leave blank.");
    }

    internal static void Summary(int count, TimeSpan total, TimeSpan average)
    {
        Console.WriteLine($"\nNumber of Sessions: {count} Total: {total} Average: {average}\n");
    }

    internal static void Successful(string action)
    {
        Console.WriteLine($"\nThe record was {action}\n");
    }

    internal static void Successful(int id, string action)
    {
        Console.WriteLine($"\nThe record with the Id {id} was {action}\n");
    }
    internal static void Unsuccessful(string action)
    {
        Console.WriteLine($"\nThe record was not {action}\n");
    }

    internal static void Unsuccessful(int id, string action)
    {
        Console.WriteLine($"\nThe record with the Id {id} was not {action}\n");
    }

    internal static void Incorrect()
    {
        Console.WriteLine("\nYour input was incorrect, please try again!\n");
    }

    internal static void DashLines()
    {
        Console.WriteLine("----------------------------------------------------------------\n");
    }

    internal static void NoRows()
    {
        Console.WriteLine("No rows found");
    }

    internal static void Exit()
    {
        Console.WriteLine("\nGoodbye!\n");
    }
}
