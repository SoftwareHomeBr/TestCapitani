using CatalogoDomain;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Services
{
    public class PessoaService
    {
        public PessoaVO GetPessoa(int pessoaId)
        {
            using (var cli = new HttpClient())
            {
                var resp = cli.GetStringAsync($"https://localhost:44395/pessoa/{pessoaId}").Result;
                var model = JsonConvert.DeserializeObject<PessoaVO>(resp);
                Console.WriteLine(resp);
                return model;
            }
        }
    }
}
