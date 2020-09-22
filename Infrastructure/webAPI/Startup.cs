using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoDomain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace webAPI
{
    public class Startup
    {
        public static SortedList<int, PessoaVO> pessoas = new SortedList<int, PessoaVO>();
        public Startup(IConfiguration configuration)
        {
            var pess= new PessoaVO[] {
                new PessoaVO{ PessoaId=1, Nome="Jose Bonifácio", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=2, Nome="Maria Scobar", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=3, Nome="Ana Furatdo", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_CASADA, DataNascimento= randDates(), ParceiroId =10 },
                new PessoaVO{ PessoaId=4, Nome="Ricardo Assunção", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=5, Nome="Antonio Prado", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=6, Nome="Jose Arruda", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=7, Nome="Maria Antonia", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=8, Nome="Ana Benvinda de Andrade", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=9, Nome="Roberto Antunes", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=10, Nome="Antonio Abujanra", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_CASADA, DataNascimento= randDates(), ParceiroId =3 },
                new PessoaVO{ PessoaId=11, Nome="Julio Mesquita", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=12, Nome="Maria Lisboa", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=13, Nome="Amanda Ordena", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=14, Nome="Ruth Alcantara", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=15, Nome="Osmar Santos", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=16, Nome="Oscar Neiro", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=17, Nome="Takaro Kobi Nomuro", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=18, Nome="Rita Passaquatro", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=19, Nome="Hugo Mudo", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=20, Nome="Heitor Queiroz", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=21, Nome="Mario Lago", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=22, Nome="Rui Barbosa", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=23, Nome="Anastacia Friasa", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=24, Nome="Hermano Rening", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
                new PessoaVO{ PessoaId=25, Nome="Patricia Colunas", EstadoCivil=PessoaVO.ESTADO_CIVIL.PESSOA_SOLTEIRA, DataNascimento= randDates(), ParceiroId =0 },
            };
            foreach (var pes in pess)
            {
                pessoas.Add(pes.PessoaId,pes);
            }
            Configuration = configuration;
        }
        private DateTime randDates()
        {
            var rd = new Random((int)DateTime.Now.Ticks);
            return new DateTime(1960 + rd.Next(55), 1 + rd.Next(11), 1 + rd.Next(27));
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
