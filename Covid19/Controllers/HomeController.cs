using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Covid19.Models;
using Covid19.Services;
using Covid19.Models.ViewModels;
using X.PagedList;
using System.Xml.Serialization;

namespace Covid19.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _environment;
    public HomeController(IWebHostEnvironment environment,ILogger<HomeController> logger)
    {
        _logger = logger;
        _environment = environment;
    }

    public async Task<IActionResult> Index(int? size,int? page,string iso)
    {
        try{
            size = (size ?? 10);
            page = (page ?? 1);
            ViewBag.size = size;
            Service est = new Service();
            
            List<DataViewModel> dvm = new List<DataViewModel>();
            
            if(!string.IsNullOrEmpty(iso)){
                var regiones = await est.getRegions();
                var dt0 = await est.getReporteregiones(iso);
                var dt1 = await est.getReporteprovincia(iso);
                dvm.Add(new DataViewModel{
                    regiones = regiones.Value,
                    Reporteregiones = dt0.Value,
                    Reporteprovincias = dt1.Value
                });
                string filename = string.Concat("Provincias",".xml");
                string path = Path.Combine(_environment.WebRootPath,"Reportes/");
                
                Generar.Xml(dt1.Value,path+filename);
                filename = "Provincias.json";
                Generar.Json(dt1.Value,path+filename);
                filename = "Provincias.csv";
                Generar.Csv(dt1.Value,path+filename);
                ViewBag.carga = true;
                ViewBag.iso = iso;
                TempData["selected"] = iso;
            }else{
                var regiones = await est.getRegions();
                var dt0 = await est.getReporteregiones("");
                var dt1 = await est.getReporteprovincia("");
                dvm.Add(new DataViewModel{
                    regiones = regiones.Value,
                    Reporteregiones = dt0.Value,
                    Reporteprovincias = dt1.Value
                });
                string filename = string.Concat("Regiones",".xml");
                string path = Path.Combine(_environment.WebRootPath,"Reportes/");
                Generar.Xml(dt0.Value,path+filename);
                filename = "Regiones.json";
                Generar.Json(dt0.Value,path+filename);
                filename = "Regiones.csv";
                Generar.Csv(dt0.Value,path+filename);
                ViewBag.carga = false;
                ViewBag.iso = iso;
                TempData["selected"]  = string.Empty;
            }
            return View(await dvm.ToPagedListAsync(page.Value,size.Value));
        }catch(Exception Ex){
            return StatusCode(400,Ex.Message);
        }
    }
    public FileResult ReporteXml(){
        if(TempData["selected"] !=string.Empty){
         //if(!string.IsNullOrEmpty(iso)) {   
            string filename = string.Concat("Provincias",".xml");
            string path = Path.Combine(_environment.WebRootPath,"Reportes/")+filename;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/xml", filename);
        }else{
            string filename = string.Concat("Regiones",".xml");
            string path = Path.Combine(_environment.WebRootPath,"Reportes/")+filename;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/xml", filename);
        }
    }
    public FileResult ReporteJson(){
        if(TempData["selected"] !=string.Empty){
        // if(!string.IsNullOrEmpty(iso)){   
            string filename = string.Concat("Provincias",".json");
            string path = Path.Combine(_environment.WebRootPath,"Reportes/")+filename;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/xml", filename);
        }else{
            string filename = string.Concat("Regiones",".json");
            string path = Path.Combine(_environment.WebRootPath,"Reportes/")+filename;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/xml", filename);
        }
    }
    public FileResult ReporteCsv(){
        if(TempData["selected"] !=string.Empty){
            string filename = string.Concat("Provincias",".csv");
            string path = Path.Combine(_environment.WebRootPath,"Reportes/")+filename;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/xml", filename);
        }else{
            string filename = string.Concat("Regiones",".csv");
            string path = Path.Combine(_environment.WebRootPath,"Reportes/")+filename;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/xml", filename);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
