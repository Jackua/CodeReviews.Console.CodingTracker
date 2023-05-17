﻿using Serilog;
using System.Globalization;

namespace CodingTracker.barakisbrown;

public static class Input
{
    private readonly static string _validDateFormat = "MM-dd-yyyy";
    private readonly static string _validTimeFormat = "hh:mm";
    private readonly static string _dateInputString = $"Enter the date in the following format [{_validDateFormat}] or Enter for today :>";
    private readonly static string _timeInputString = $"Enter the time in the following format [{_validTimeFormat}] or Enter for current time  :>";
    
    public static bool GetYesNo()
    {
        ConsoleKeyInfo input = Console.ReadKey(true);
        if (input.Key == ConsoleKey.Y)
            return true;
        else
            return false;
    }

    public static void GetKeyReturnMenu()
    {
        Console.Write("Press any key to return to the main menu.");
        Console.ReadKey(true);
        Thread.Sleep(800);
        Console.Clear();
    }

    public static DateOnly GetDate()
    {
        Console.Write(_dateInputString);
        string? result = Console.ReadLine();

        while (true)
        {
            if (result == string.Empty)
                return DateOnly.FromDateTime(DateTime.Now);
            try
            {
                DateTime.TryParseExact(result, _validDateFormat, new CultureInfo("en-us"), DateTimeStyles.None, out DateTime date);
                return DateOnly.FromDateTime(date);
            }
            catch (FormatException _)
            {
                Log.Error("F> GetDate() raised exception and caught. Exception Message : {0}", _.Message);
                Console.WriteLine($"Date has to in the following format: {_validDateFormat} ");
                Console.WriteLine("Please try again.");
                Console.WriteLine(_dateInputString);
                result = Console.ReadLine();
            }
        }
    }

    public static TimeOnly GetTime()
    {       
        while (true)
        {
            Console.Write(_timeInputString);
            string? result = Console.ReadLine();
            int hourInt = 0, minuteInt = 0;

            if (result == string.Empty)
            {
                hourInt = TimeOnly.FromDateTime(DateTime.Now).Hour;
                minuteInt = TimeOnly.FromDateTime(DateTime.Now).Minute;
            }
            else if (!result.Contains(':'))
            {
                Log.Debug("F> GetTime() -- User entered wrong information. Will be told to reenter.");
                Console.WriteLine("Invalid Time Entered. It must be HH:MM");
            }
            else
            {
                var parse = result?.Split(":");
                try
                {
                    hourInt = int.Parse(parse[0]);
                    if (hourInt >= 24)
                    {
                        Log.Debug("F> GetTime() -- User entered wrong information. Will be told to reenter.");
                        Console.WriteLine("Invalid Hour : Hour should be between 0 and 24");
                        continue;
                    }
                }
                catch (FormatException _)
                {
                    Log.Error("F> GetTime() raised an exception and caught. Exception message is {0}", _.Message);
                    Console.WriteLine("Invalid Information. Please make sure it is numerical.");
                    continue;
                }
                try
                {
                    minuteInt = int.Parse(parse[1]);
                    if (minuteInt >= 60)
                    {
                        Log.Debug("F> GetTime() -- User entered wrong information. Will be told to reenter.");
                        Console.WriteLine("Invalid time. Time should be between 0 and 60.");
                        continue;
                    }
                }
                catch (FormatException _)
                {
                    Log.Error("F> GetTime() raised an exception and caught. Exception message is {0}", _.Message);
                    Console.WriteLine("Invalid Information. Please make sure it is numerical.");
                    continue;
                }
            }
            // Confirm if this is the correct time entered.
            TimeOnly retTime = new(hourInt, minuteInt);

            Console.Write($"Did you enter {retTime}  (Y/N)?");
            if (GetYesNo())
                return retTime;
            else
                Console.WriteLine("\nOkay. Lets try it again.");
        }
    }

    public static DTSeperated GetSessionInfo()
    {
        return new() 
        {
            Date = GetDate(),
            Time = GetTime()
        };       
    }  
}
