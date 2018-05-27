using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCore
{
    public static class ReportController
    {   
        //Método global para retornar o diretório do formulário apenas pelo nome passado 
        //no parâmetro e o dataSource
        public static string GetReportViewerURL(string reportName ,string path , 
            //IEnumerable suporta todos os tipos de coleções,
            //isso faz com que o método se torne mais genérico
            IEnumerable<Object> data)
        {           
            //Objeto já configurado de acordo com a estrutura da aplicação
            ReportViewer report = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Local
            };
            report.LocalReport.ReportPath = path + @"Reports\" + reportName + ".rdlc";
            report.LocalReport.DataSources.Add
                (new ReportDataSource(reportName, data));
            report.LocalReport.Refresh();

            var pathTemp = path + @"Content\Temp\";
            var fileName = DateTime.Now.ToShortDateString() + ".pdf";
            fileName = fileName.Replace('/' , '-');

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            object ErrorTry;
            //Remove todos os arquivos temporários para não deixar sujeira no servidor
            try
            {
                Array.ForEach(Directory.GetFiles(pathTemp),
                  delegate (string pathDirectory) { File.Delete(path); });
            }
            catch (Exception e)
            {
                ErrorTry = e;
                //Se não conseguir executar a ação de remoção, veja a questão de permissões
                //da pasta aplicação em seu computador
            }

            //Renderiza para o arquivo PDF para salvar no dirétorio da pasta Temp
            byte[] bytes = report.LocalReport.Render(
                "Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            
            using (FileStream fs = new FileStream(pathTemp + fileName
                , FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            return fileName;
        }
    }
}
