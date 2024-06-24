using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

const string url = "https://localhost:7281/hub";

var connection = new HubConnectionBuilder()
    .WithUrl(url)
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Information);
    })
    .Build();

connection.On<DateTime>("ReceiveDateTime", (date) =>
{
    Console.WriteLine($"Received DateTime: {date}");
});

await connection.StartAsync();
Console.WriteLine("Connection started. Listening for DateTime updates...");

await foreach (var date in connection.StreamAsync<DateTime>("Streaming"))
{
    Console.WriteLine($"Received DateTime: {date}");
}

// Manter o programa em execução para continuar recebendo mensagens
await Task.Delay(Timeout.Infinite);

//teste