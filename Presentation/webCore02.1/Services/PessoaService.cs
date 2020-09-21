using CatalogoDomain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace webCore02._1.Services
{
    public class PessoaService
    {
        public static PessoaVO GetPessoa(int pessoaId)
        {
            using (var cli = new HttpClient())
            {
                var resp = cli.GetStringAsync($"https://localhost:44395/api/pessoa/{pessoaId}").Result;
                var model = JsonConvert.DeserializeObject<PessoaVO>(resp);
                Console.WriteLine(resp);
                return model;
            }
        }
        public static List<PessoaModel> ListaPessoas()
        {
            using (var cli = new HttpClient())
            {
                var resp = cli.GetStringAsync("https://localhost:44395/api/Pessoa").Result;
                var model = JsonConvert.DeserializeObject<List<PessoaModel>>(resp);
                return model;
            }
        }
    }
}
