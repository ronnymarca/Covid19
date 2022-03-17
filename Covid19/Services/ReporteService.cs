using System.Xml.Serialization;
using Covid19.Models;
using Newtonsoft.Json;

namespace Covid19.Services;

public class Generar{
    public static void Xml(report reporte,string path){
        reporte.dt = reporte.dt.ToList();
        XmlSerializer x = new XmlSerializer(typeof(report));
        TextWriter writer = new StreamWriter(path);
        x.Serialize(writer, reporte);
        writer.Close();
    }
    public static void Json(report reporte,string path){
        var json = JsonConvert.SerializeObject(reporte);
        StreamWriter wr = File.CreateText(path);
        wr.Write(json);
        wr.Close();
    }
    public static void Csv(report reporte,string path){
        StreamWriter wr = File.CreateText(path);
        foreach(var item in reporte.dt){
            wr.Write(item.regions.province+';'+item.confirmed+';'+item.deaths+';');
            wr.WriteLine();
        }
        wr.Close();
    }
}