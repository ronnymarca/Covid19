#nullable enable

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Covid19.Models;
public class cities{
    public string? regiones {get;set;}
    public string? name {get;set;}
    public string? date {get;set;}
    public int? fips {get;set;}
    public string? lat {get;set;}
    [JsonProperty("long")]
    public string? longitud {get;set;}
    public int? confirmed {get;set;}
    public int? deaths {get;set;}
    public int? confirmed_diff {get;set;}
    public int? deaths_diff {get;set;}
    public string? last_update {get;set;}
}
public class region{
    public string? iso {get;set;}
    public string? name {get;set;}
    public string? province {get;set;}
    public string? lat {get;set;}
    [JsonProperty("long")]
    public string? longitud {get;set;}
    [JsonProperty("cities",Order = 2)]
    public List<cities?> _cities = new List<cities?>();
    public IOrderedEnumerable<cities> TotalConfirmados(){
        var sum = _cities.OrderBy(x=>x.regiones);
        return sum;
    }
}

public class data{
    public string? date {get;set;}

    public int confirmed {get;set;}
    public int deaths {get;set;}
    public int recovered {get;set;}
    public int confirmed_diff {get;set;}
    public int deaths_diff {get;set;}
    public int recovered_diff {get;set;}
    public string? last_update {get;set;}
    public int active {get;set;}
    public int active_diff {get;set;}
    public double fatality_rate {get;set;}
    [JsonProperty("region",Order = 1)]
    public region? regions {get;set;}
}

public class report{
    [JsonProperty("data")]
    public List<data?> dt = new List<data?>(); 
}