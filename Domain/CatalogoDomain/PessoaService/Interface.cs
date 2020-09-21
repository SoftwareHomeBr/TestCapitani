using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDomain.PessoaService
{
    interface IPessoaService
    {
        public PessoaVO GetPessoa(int pessoaId);
    }
}
