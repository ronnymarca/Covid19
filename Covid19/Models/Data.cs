using Newtonsoft.Json;

namespace Covid19.Models;
// Comentario Base
public class dato
{
    public string iso { get; set; }
    public string name { get; set; }
}
public class regions
{
    [JsonProperty("data")]
    public List<dato> datos { get; set; }
}