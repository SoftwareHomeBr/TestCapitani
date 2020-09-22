using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CatalogoDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using webCore02._1.Models;
using webCore02._1.Services;

namespace webCore02._1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult PessoaList()
        {
            return Pessoas(0, null, null);
        }
        public IActionResult Pessoas([FromQuery] int? pageNum, [FromQuery] string filtro, [FromQuery]string pagin)
        {
            using (var cli = new HttpClient())
            {
                if(pageNum == null)
                {
                    pageNum = 0;
                }
                var resp = cli.GetStringAsync($"https://localhost:44395/api/Pessoa?pagina={pageNum}&filtro={filtro}").Result;
                var model = JsonConvert.DeserializeObject<List<PessoaModel>>(resp);

                foreach (var p in model)
                {
                    if(p.ParceiroId >0)
                        p.parceiro = p.CriaPessoa( PessoaService.GetPessoa(p.ParceiroId));
                }

                var view = View("PessoaListView", model);
                ViewBag.fitro = filtro;
                pageNum = pagin == "search" ? pageNum = 0 : ( pagin == "next" ? pageNum += 1 : pageNum -= 1);
                if(pageNum == null || pageNum < 0)
                {
                    pageNum = 0;
                }
                else if (pageNum > 4)
                {
                    pageNum = 4;
                }
                ViewBag.currentPage =  pageNum;
                return view;
            }
        }
        public IActionResult PessoaCreate()
        {
            var model = new PessoaModel();
            var view = View("PessoaCreateView", model);
            List<SelectListItem> pessoasList = new List<SelectListItem>();
            foreach (var p in PessoaService.ListaPessoas())
            {
                pessoasList.Add(new SelectListItem { Value = p.PessoaId.ToString(), Text = p.Nome });
            }
            ViewBag.lista = pessoasList;
            return view;
        }
        [HttpPost("Home/PessoaCreateView")]
        public IActionResult PessoaCreatePost( PessoaModel pessoa)
        {
            using (var cli = new HttpClient())
            {
                string url = "https://localhost:44395/api/Pessoa";
                using (var content = new StringContent(JsonConvert.SerializeObject(pessoa), System.Text.Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage result = cli.PostAsync(url, content).Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return PessoaList();
                    }
                }
                return PessoaList();
            }
        }

        [HttpGet("Home/PessoaEdit/{pessoaId}")]

        public IActionResult PessoaEdit([FromRoute]int pessoaId)
        {
            using (var cli = new HttpClient())
            {
                var resp = cli.GetStringAsync("https://localhost:44395/api/Pessoa/" + pessoaId).Result;
                var model = JsonConvert.DeserializeObject<PessoaModel>(resp);
                List<SelectListItem> pessoasList = new List<SelectListItem>();
                foreach (var p in PessoaService.ListaPessoas())
                {
                    pessoasList.Add(new SelectListItem {Value=p.PessoaId.ToString(), Text=p.Nome });
                }
                ViewBag.lista = pessoasList;
                var view = View("PessoaEditView", model);
                return view;
            }
        }
        [HttpPost("Home/PessoaEdit/{pessoaId}")]
        public IActionResult PessoaEditSave( PessoaModel pessoaModel)
        {
            using (var cli = new HttpClient())
            {
                string url = "https://localhost:44395/api/Pessoa";
                using (var content = new StringContent(JsonConvert.SerializeObject(pessoaModel), System.Text.Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage result = cli.PostAsync(url, content).Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        Response.Redirect("/Home/Pessoas");
                        return PessoaList();
                    }
                }
                Response.Redirect("/Home/Pessoas");
                return PessoaList();
            }
        }
        [HttpGet("Home/PessoaDelete/{id}")]
        public IActionResult PessoaDelete([FromRoute] int id)
        {
            using (var cli = new HttpClient())
            {
                var resp = cli.GetStringAsync("https://localhost:44395/api/Pessoa/" + id).Result;
                var model = JsonConvert.DeserializeObject<PessoaModel>(resp);
                var view = View("PessoaDeleteView", model);
                return view;
            }
        }
        [HttpPost("Home/PessoaDelete/{pessoaId}")]
        public IActionResult PessoaDel(int pessoaId)
        {
            using (var cli = new HttpClient())
            {
                var resp = cli.DeleteAsync("https://localhost:44395/api/Pessoa/" + pessoaId).Result;
//                Response.Redirect("Home/Pessoas");
                return PessoaList();

            }
        }
        [HttpGet("Home/PessoaDetails/{pessoaId}")]
        public IActionResult PessoaDetails(int pessoaId)
        {
            using (var cli = new HttpClient())
            {
                var resp = cli.GetStringAsync("https://localhost:44395/api/Pessoa/" + pessoaId).Result;
                var model = JsonConvert.DeserializeObject<PessoaModel>(resp);
                if (model.ParceiroId > 0)
                    model.parceiro = model.CriaPessoa(PessoaService.GetPessoa(model.ParceiroId));
                var view = View("PessoaDetailsView", model);
                return view;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
