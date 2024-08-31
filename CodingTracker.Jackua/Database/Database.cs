using CodingTracker.Jackua.Model;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Configuration;

namespace CodingTracker.Jackua.Database;

internal static class Database
{
    internal static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();

    internal static void CreateTable()
    {
        var sql =
            @"
                    CREATE TABLE IF NOT EXISTS coding_sessions (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        startDateTime TEXT,
                        endDateTime TEXT,
                        duration TEXT
                    );

                    INSERT INTO coding_sessions
                    SELECT *
                    FROM (
                        VALUES (1, '5/1/2024 4:30:10 PM', '5/1/2024 5:30:10 PM', '0.01:00:00'),
                               (2, '5/2/2024 4:30:10 PM', '5/2/2024 5:00:10 PM', '0.00:30:00'),
                               (3, '5/3/2024 4:30:10 PM', '5/3/2024 6:30:10 PM', '0.02:00:00'),
                               (4, '5/4/2024 4:30:10 PM', '5/4/2024 4:35:10 PM', '0.00:05:00'),
                               (5, '5/5/2024 4:30:10 PM', '5/6/2024 4:30:10 PM', '1.00:00:00')
                    )
                    WHERE NOT EXISTS (
                        SELECT * FROM coding_sessions
                    );
                ";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Execute(sql);
        }
    }

    internal static int InsertRecord(CodingSessionModel model)
    {
        var (StartDateTime, EndDateTime, Duration) = model;
        var parameters = new { StartDateTime, EndDateTime, Duration };

        var sql = "INSERT INTO coding_sessions (startDateTime, endDateTime, duration) VALUES (@StartDateTime, @EndDateTime, @Duration);";

        int rowsAffected;

        using (var connection = new SqliteConnection(connectionString))
        {
            rowsAffected = connection.Execute(sql, parameters);
        }

        return rowsAffected;
    }

    internal static int UpdateRecord(int id, CodingSessionModel model)
    {
        var (StartDateTime, EndDateTime, Duration) = model;
        var parameters = new { id, StartDateTime, EndDateTime, Duration };

        var sql = "UPDATE coding_sessions SET startDateTime = @StartDateTime, endDateTime = @EndDateTime, duration = @Duration WHERE id = @id;";

        int rowsAffected;

        using (var connection = new SqliteConnection(connectionString))
        {
            rowsAffected = connection.Execute(sql, parameters);
        }

        return rowsAffected;
    }


    internal static int DeleteRecord(int id)
    {
        var parameters = new { id };

        var sql = "DELETE FROM coding_sessions WHERE id = @id;";

        int rowsAffected;

        using (var connection = new SqliteConnection(connectionString))
        {
            rowsAffected = connection.Execute(sql, parameters);
        }

        return rowsAffected;
    }

    internal static List<CodingSessionModel> GetRecords()
    {
        var sql = "SELECT * FROM coding_sessions;";

        List<CodingSessionModel> records = new List<CodingSessionModel>();

        using (var connection = new SqliteConnection(connectionString))
        {
            records = connection.Query<CodingSessionModel>(sql).ToList();
        }

        return records;
    }

    internal static List<CodingSessionModel> GetRecordsBy(string startDateTime, string endDateTime, string duration)
    {
        var parameters = new { startDateTime = $"%{startDateTime}%", endDateTime = $"%{endDateTime}%", duration = $"%{duration}%" };

        var sql = "SELECT * FROM coding_sessions WHERE startDateTime like @startDateTime and endDateTime like @endDateTime and duration like @duration;";

        List<CodingSessionModel> records = new List<CodingSessionModel>();

        using (var connection = new SqliteConnection(connectionString))
        {
            records = connection.Query<CodingSessionModel>(sql, parameters).ToList();
        }

        return records;
    }

}
