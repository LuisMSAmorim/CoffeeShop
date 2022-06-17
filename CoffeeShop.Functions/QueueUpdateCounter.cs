using System;
using System.Data.SqlClient;
using CoffeeShop.Domain.Model.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.Functions;

public static class QueueUpdateCounter
{
    [FunctionName("QueueUpdateCounter")]
    public static void Run([QueueTrigger("func-update-last-time-visualization", Connection = "AzureWebJobsStorage")]Coffee coffee, ILogger log)
    {
        log.LogInformation($"C# Queue trigger function processed");

        var dbConnectionString = Environment.GetEnvironmentVariable("DatabaseConnection");

        SqlConnection connection = new(dbConnectionString);

        connection.Open();

        var actualVisualizationsNumber = coffee.VisualizationsNumber;
        var updatedVisualizationsNumber = actualVisualizationsNumber + 1;

        var textSql = $@"UPDATE [dbo].[Coffee] SET [VisualizationsNumber] = {updatedVisualizationsNumber} WHERE [Id] = {coffee.Id};";

        SqlCommand cmd = new(textSql, connection);

        var rowsAffected = cmd.ExecuteNonQuery();

        log.LogInformation($"rowsAffected: {rowsAffected}");
    }
}
