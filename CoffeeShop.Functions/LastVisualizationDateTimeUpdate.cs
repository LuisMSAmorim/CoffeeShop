using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace CoffeeShop.Functions;

public static class LastVisualizationUpdate
{
    [FunctionName("LastVisualizationUpdate")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);

        int coffeId = data?.Id;

        var dbConnectionString = Environment.GetEnvironmentVariable("DatabaseConnection");

        SqlConnection connection = new(dbConnectionString);

        connection.Open();

        var textSql = $@"UPDATE [dbo].[Coffee] SET [LastVisualization] = GETDATE() WHERE [Id] = {coffeId};";

        SqlCommand cmd = new(textSql, connection);

        var rowsAffected = cmd.ExecuteNonQuery();

        log.LogInformation($"rowsAffected: {rowsAffected}");

        return new OkResult();
    }
}
