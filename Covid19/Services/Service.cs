using Covid19.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Covid19.Services;

public class Service : ControllerBase
{
    public async Task<ActionResult<regions>> getRegions()
    {
        // Comentario Base
        try
        {
            var client = new RestClient("https://covid-19-statistics.p.rapidapi.com/regions");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("x-rapidapi-host", "covid-19-statistics.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "cd019ecb16msh67d6b51edfa93cfp1d3b37jsn75d101181bff");
            RestResponse response = await client.ExecuteAsync(request);
            var list = JsonConvert.DeserializeObject<regions>(response.Content!);
            return list!;
        }
        catch (Exception Ex)
        {
            return StatusCode(400, Ex.Message);
        }
    }
    public async Task<ActionResult<report>> getReporteregiones(string iso)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(iso))
            {
                var client = new RestClient("https://covid-19-statistics.p.rapidapi.com/reports");
                var request = new RestRequest();
                request.Method = Method.Get;
                request.AddHeader("x-rapidapi-host", "covid-19-statistics.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "cd019ecb16msh67d6b51edfa93cfp1d3b37jsn75d101181bff");
                RestResponse response = await client.ExecuteAsync(request);
                var list = JsonConvert.DeserializeObject<report>(response.Content!);
                var dt = list.dt.GroupBy(gp => gp.regions.iso).Select(s => new data
                {

                    regions = s.FirstOrDefault().regions,
                    confirmed = s.Sum(s => s.confirmed),
                    deaths = s.Sum(s => s.deaths),

                }).OrderByDescending(or => or.confirmed).Take(10).ToList();
                list.dt = dt;
                return list!;
            }
            else
            {
                var client = new RestClient($"https://covid-19-statistics.p.rapidapi.com/reports?iso={iso}");
                var request = new RestRequest();
                request.Method = Method.Get;
                request.AddHeader("x-rapidapi-host", "covid-19-statistics.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "cd019ecb16msh67d6b51edfa93cfp1d3b37jsn75d101181bff");
                RestResponse response = await client.ExecuteAsync(request);
                var list = JsonConvert.DeserializeObject<report>(response.Content!);
                var dt = list.dt.GroupBy(gp => gp.regions.iso).Select(s => new data
                {
                    regions = s.FirstOrDefault().regions,
                    confirmed = s.Sum(s => s.confirmed),
                    deaths = s.Sum(s => s.deaths),

                }).OrderByDescending(or => or.confirmed).Take(10).ToList();
                list.dt = dt;
                return list;
            }
        }
        catch (Exception Ex)
        {
            return StatusCode(400, Ex.Message);
        }
    }
    public async Task<ActionResult<report>> getReporteprovincia(string iso)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(iso))
            {
                var client = new RestClient($"https://covid-19-statistics.p.rapidapi.com/reports?iso={iso}");
                var request = new RestRequest();
                request.Method = Method.Get;
                request.AddHeader("x-rapidapi-host", "covid-19-statistics.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "cd019ecb16msh67d6b51edfa93cfp1d3b37jsn75d101181bff");
                RestResponse response = await client.ExecuteAsync(request);
                var list = JsonConvert.DeserializeObject<report>(response.Content!);
                //list.dt = list.dt.OrderByDescending(x=>x.confirmed).Take(10).ToList();
                var dt = list.dt.Select(s => new data
                {
                    regions = s.regions,
                    confirmed = s.confirmed,
                    deaths = s.deaths,

                }).OrderByDescending(or => or.confirmed).Take(10).ToList();
                list.dt = dt;
                return list;
            }
            return new report();
        }
        catch (Exception Ex)
        {
            return StatusCode(400, Ex.Message);
        }
    }
}