using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using database = RepoSentry.Models.RepoRastreio;
using RepoSentry.Models;
using TesteAutenticacaoHttp;

namespace RepoSentry.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/

        [RequireBasicAuthentication]
        public ActionResult Index(string cpf, int? parCodCliente)
        {
            var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(Request.Headers["Authorization"].Substring(6))).Split(':');
            var user = new { Name = cred[0], Pass = cred[1] };

            if (user.Name.Equals("searcher") && user.Pass.Equals("sentry8495"))
            {
                database db = new database();
                List<DadosRastreio> dados = db.consultarDadosRastreio(cpf, parCodCliente);
                return View(dados);
            }
            else
            return Content(String.Format("user:{0}, password:{1}", user.Name, user.Pass));
        }
    }
}
