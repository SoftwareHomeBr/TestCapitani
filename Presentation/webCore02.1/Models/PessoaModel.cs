using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDomain
{
    public class PessoaModel
    {
        public enum ESTADO_CIVIL { PESSOA_SOLTEIRA, PESSOA_CASASADA }
        public int PessoaId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public ESTADO_CIVIL EstadoCivil 
        { 
            get 
            {
                return _parceiroId > 0 ? ESTADO_CIVIL.PESSOA_CASASADA : ESTADO_CIVIL.PESSOA_SOLTEIRA;
            } 
            set 
            {  
                if(value == ESTADO_CIVIL.PESSOA_SOLTEIRA)
                {
                    _parceiroId = 0;
                }
            } 
        }
        private int _parceiroId = 0;
        public int ParceiroId
        {
            get
            {
                return _parceiroId;
            }
            set
            {
                _parceiroId = value;
                if(value > 0)
                {
                    EstadoCivil = ESTADO_CIVIL.PESSOA_CASASADA;
                }
                else
                {
                    EstadoCivil = ESTADO_CIVIL.PESSOA_SOLTEIRA;
                }
            }
            
        }
        public PessoaModel parceiro { get; set; }

        public PessoaModel CriaPessoa(PessoaVO pessoaVO)
        {
            if (pessoaVO != null)
                return new PessoaModel
                {
                    Nome = pessoaVO.Nome,
                    EstadoCivil = (ESTADO_CIVIL)pessoaVO.EstadoCivil,
                    DataNascimento = pessoaVO.DataNascimento,
                    ParceiroId = pessoaVO.ParceiroId
                };
            else return null;
        }
        public  ICollection<PessoaModel> CriaPessoasLista(ICollection<PessoaVO> pessoasVO)
        {
            var pessoas = new List<PessoaModel>();
            foreach (var item in pessoasVO)
            {
                pessoas.Add( new PessoaModel
                {
                    Nome = item.Nome,
                    EstadoCivil = (ESTADO_CIVIL)item.EstadoCivil,
                    DataNascimento = item.DataNascimento,
                    ParceiroId = item.ParceiroId
                });
            }
            return pessoas;
        }
    }
}
