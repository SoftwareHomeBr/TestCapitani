﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoDomain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        // GET: api/<PessoaController>
        [HttpGet]
        public IEnumerable<PessoaVO> Get([FromQuery] int? pagina, [FromQuery] string filtro)
        {
            int paginaLen = 5;
            int offset = (pagina ?? 0) * paginaLen;
            if(offset >= Startup.pessoas.Count)
            {
                offset = Startup.pessoas.Count-1;
            }
            var filtered = Startup.pessoas.Values.Where(p =>
                filtro == null || (p.Nome?.Contains(filtro, StringComparison.CurrentCultureIgnoreCase) ?? false)
            );
            if (pagina != null)
                return filtered.Skip(offset).Take(paginaLen);
            else
                return filtered.Skip(offset);
        }

        // GET api/<PessoaController>/5
        [HttpGet("{id}")]
        public PessoaVO Get(int id)
        {
            var who = Startup.pessoas.Values.ToList().Find(p => p.PessoaId == id);
            return who;
        }

        // POST api/<PessoaController>
        [HttpPost]
        public void Post( PessoaVO pessoa)
        {
            if (pessoa.PessoaId < 1)
            {
                var newId = 0;
                Startup.pessoas.Values.ToList().ForEach(p =>
                {
                    if (newId < p.PessoaId)
                        newId = p.PessoaId;
                });
                newId++;
                pessoa.PessoaId = newId;
                Startup.pessoas.Add(pessoa.PessoaId, pessoa);
            }
            else
            {
                var who = Startup.pessoas.Values.ToList().Find(p => p.PessoaId == pessoa.PessoaId);
                if (who != null)
                {
                    if(pessoa.ParceiroId == pessoa.PessoaId)
                    {
                        throw new ApplicationException("Casamento Inválido: Mesmo indivíduo");
                    }
                    Startup.pessoas.Remove(who.PessoaId);
                    Startup.pessoas.Add(pessoa.PessoaId, pessoa);
                    if (pessoa.ParceiroId > 0)
                    {
                        var partner = Startup.pessoas.Values.ToList().Find(p => p.PessoaId == pessoa.ParceiroId);
                        partner.ParceiroId = pessoa.PessoaId;
                    }
                }
            }
        }

        // PUT api/<PessoaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PessoaVO value)
        {
            var who = Startup.pessoas.Values.ToList().Find(p => p.PessoaId == id);
            if (who != null)
            {
                Startup.pessoas.Remove(who.PessoaId);
                Startup.pessoas.Add(value.PessoaId, value);
            }
        }

        // DELETE api/<PessoaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var who = Startup.pessoas.Values.ToList().Find(p => p.PessoaId == id);
            if(who != null)
                Startup.pessoas.Remove(who.PessoaId);
        }
    }
}
