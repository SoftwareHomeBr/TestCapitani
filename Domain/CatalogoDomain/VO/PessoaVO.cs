using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDomain
{
    public class PessoaVO
    {
        public enum ESTADO_CIVIL { PESSOA_SOLTEIRA, PESSOA_CASADA }
        public int PessoaId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public ESTADO_CIVIL EstadoCivil 
        { 
            get 
            {
                return _parceiroId > 0 ? ESTADO_CIVIL.PESSOA_CASADA : ESTADO_CIVIL.PESSOA_SOLTEIRA;
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
                    EstadoCivil = ESTADO_CIVIL.PESSOA_CASADA;
                }
                else
                {
                    EstadoCivil = ESTADO_CIVIL.PESSOA_SOLTEIRA;
                }
            }
        }

    }
    public class PessoasLista : ICollection<PessoaVO>
    {
        private List<PessoaVO> pessoas = new List<PessoaVO>();
        public int Count => pessoas.Count;

        public bool IsReadOnly => false;

        public void Add(PessoaVO item)
        {
            pessoas.Add(item);
        }

        public void Clear()
        {
            pessoas.Clear();
        }

        public bool Contains(PessoaVO item)
        {
            return pessoas.Contains(item);
        }

        public void CopyTo(PessoaVO[] array, int arrayIndex)
        {
            pessoas.CopyTo(array, arrayIndex);
        }

        public IEnumerator<PessoaVO> GetEnumerator()
        {
            return pessoas.GetEnumerator();
        }

        public bool Remove(PessoaVO item)
        {
            return pessoas.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return pessoas.GetEnumerator();
        }
    }
}
