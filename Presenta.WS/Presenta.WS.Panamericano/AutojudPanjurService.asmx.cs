using Presenta.Common.Util;
using Presenta.WS.Panamericano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Presenta.WS.Panamericano
{
    /// <summary>
    /// Summary description for AutojudPanjurService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AutojudPanjurService : System.Web.Services.WebService
    {

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        [WebMethod]
        public OrdensJudiciais ProcessarOrdensJudiciais(string dataDe, string dataAte, string tipo)
        {
            try
            {
                var ordensJudiciais = new OrdensJudiciais();

                //throw new Exception("asd");

                return ordensJudiciais.ProcessarOrdensJudiciais(dataDe, dataAte, tipo);
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                throw new Exception(errorMessage);
            }
        }

        //[WebMethod]
        //public List<Oficio> Processar()
        //{
        //    var autojudXPanjur = new AutojudXPanjur();

        //    return autojudXPanjur.Processar();
        //}

        //[WebMethod]
        //public List<Oficio> ProcessarTeste()
        //{
        //    var lstOficios = new List<Oficio>();

        //    var oficio = new Oficio();

        //    oficio.IdOficio = 10;
        //    oficio.Protocolo = "1";
        //    oficio.Envolvidos.Add(new Envolvido()
        //    {
        //        CpfCnpj = "123",
        //        NomeEnvolvido = "Jose"
        //    });

        //    lstOficios.Add(oficio);

        //    return lstOficios;
        //}

    }
}