// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Text.Json.Serialization;
using TestApi;Console.WriteLine("Hello, World!");

await Task.Delay(5000);

List<Task<WeatherForecast>> tasks = new List<Task<WeatherForecast>>();

for (var i = 0; i < 10000; i++)
{
    tasks.Add(GetWeatherForcastWithSocketExhaustion());
}

var result = await Task.WhenAll(tasks);

async Task<WeatherForecast> GetWeatherForcastWithSocketExhaustion()
{
    using var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri("https://localhost:7024");

    var result = await httpClient.GetAsync("WeatherForecast");
    if (result.IsSuccessStatusCode)
    {
        var content = await result.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(content))
        {
            throw new Exception("content is null or empty.");
        }

        return JsonSerializer.Deserialize<WeatherForecast>(content);
    }
    else
    {
        throw new Exception("content is null or empty.");
    }
}

async Task<WeatherForecast> GetWeatherForcastWorking()
{
    var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri("https://localhost:7024");

    var result = await httpClient.GetAsync("WeatherForecast");
    if (result.IsSuccessStatusCode)
    {
        var content = await result.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(content))
        {
            throw new Exception("content is null or empty.");
        }

        return JsonSerializer.Deserialize<WeatherForecast>(content);
    }
    else
    {
        throw new Exception("content is null or empty.");
    }
}