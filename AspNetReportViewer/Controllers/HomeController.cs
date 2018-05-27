using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;

namespace AspNetReportViewer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetData()
        {
            //Criação de uma lista de exemplo.
            /*
             Lembrando que o foco da aplicação é o uso do ReportViewer para MVC,
             então não investi muito em conexão com banco de dados, mas obviamente
             pode e é ate melhor ser feito com conexão a um banco.
             */
            var data = new List<Person>();
            data.Add(new Person() {
                Name = "Pessoa 1",
                Age = 21,
                SocialCode = "111.111.111-22"
            });
            data.Add(new Person()
            {
                Name = "Pessoa 2",
                Age = 22,
                SocialCode = "222.222.222-33"
            });
            data.Add(new Person()
            {
                Name = "Pessoa 3",
                Age = 20,
                SocialCode = "333.333.333-44"
            });
            data.Add(new Person()
            {
                Name = "Pessoa 4",
                Age = 25,
                SocialCode = "444.444.444-55"
            });
            data.Add(new Person()
            {
                Name = "Pessoa 5",
                Age = 45,
                SocialCode = "555.555.555-66"
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GenerateReport(List<Person> list)
        {
            var path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

            return Json(
                //Método que irá gerar o relatório na pasta temp e retornar a URL para o controlador
                ReportCore.ReportController.GetReportViewerURL
                (                   //É necessário informar:
                    "PersonReports", // - o nome do arquivo de modelo RDLC a cada chamada do método
                    path,           // - o diretório físico da aplicação para o controlador
                    list            // - os dados ou a coleção que deverá aparecer no relatório
                )
                , JsonRequestBehavior.AllowGet);
        }
    }
}