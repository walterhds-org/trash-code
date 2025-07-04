using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace TrashCode.Api.Controllers;

[Controller]
[Route("api/[controller]")]
public class PartidosController : ControllerBase
{
    [HttpGet]
    public IResult GetAll()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        Log.Information($"Starting {nameof(PartidosController)}-{nameof(GetAll)}");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        var response = httpClient.GetAsync("https://dadosabertos.camara.leg.br/api/v2/partidos").Result;
        if (response.IsSuccessStatusCode)
        {
            stopwatch.Stop();
            Log.Information($"Finished {nameof(GetAll)} in {stopwatch.ElapsedMilliseconds} ms");
            var content = response.Content.ReadAsStringAsync().Result;
            return Results.Ok(content);
        }
        else
        {
            Log.Error($"Error in {nameof(GetAll)}: {response.ReasonPhrase}");
            stopwatch.Stop();
            return Results.Problem(response.ReasonPhrase);
        }
    }

    [HttpGet("{id}")]
    public IResult GetById(string id)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        Log.Information($"Starting {nameof(PartidosController)}-{nameof(GetById)} for ID: {id}");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        var response = httpClient.GetAsync($"https://dadosabertos.camara.leg.br/api/v2/partidos/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            stopwatch.Stop();
            Log.Information($"Finished {nameof(GetById)} for ID: {id} in {stopwatch.ElapsedMilliseconds} ms");
            var content = response.Content.ReadAsStringAsync().Result;
            return Results.Ok(content);
        }
        else
        {
            Log.Error($"Error in {nameof(GetById)} for ID: {id} - {response.ReasonPhrase}");
            stopwatch.Stop();
            return Results.Problem(response.ReasonPhrase);
        }
    }
}