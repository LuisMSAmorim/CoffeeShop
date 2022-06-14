using System;
using System.Data.SqlClient;
using CoffeeShop.Domain.Model.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.Functions;

public static class QueueUpdateLastTimeVisualization
{
    [FunctionName("QueueUpdateLastTimeVisualization")]
    public static void Run([QueueTrigger("func-update-last-time-visualization", Connection = "AzureWebJobsStorage")]Coffee coffee, ILogger log)
    {
        log.LogInformation($"C# Queue trigger function processed");

        var dbConnectionString = Environment.GetEnvironmentVariable("DatabaseConnection");

        SqlConnection connection = new(dbConnectionString);

        connection.Open();

        var textSql = $@"UPDATE [dbo].[Coffee] SET [LastVisualization] = GETDATE() WHERE [Id] = {coffee.Id};";

        SqlCommand cmd = new(textSql, connection);

        var rowsAffected = cmd.ExecuteNonQuery();

        log.LogInformation($"rowsAffected: {rowsAffected}");
    }
}
