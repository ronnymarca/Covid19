using NUnit.Framework;
using Covid19.Services;
using Covid19.Models;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestRegiones()
        {
            Service service = new Service();
            var regiones = service.getRegions();
            Assert.IsNotNull(regiones.Result);
        }
        [Test]
        public void TestReporteregiones()
        {
            Service service = new Service();
            var provincias = service.getReporteregiones("");
            Assert.IsNotNull(provincias.Result);
        }
        [Test]
        public void TestReporteprovincias()
        {
            Service service = new Service();
            var provincias = service.getReporteprovincia("");
            Assert.IsNotNull(provincias.Result);
        }
    }
}