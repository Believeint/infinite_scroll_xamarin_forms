using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfiniteScrollingExample4.Models;

namespace InfiniteScrollingExample4.Service
{
    public class PessoaService
    {
        List<Pessoa> _containerPessoas;

        public PessoaService()
        {
            _containerPessoas = new List<Pessoa>(preencherContainer());
        }

        private IEnumerable<Pessoa> preencherContainer()
        {
            var random = new Random(0);
            var dataNasc = new DateTime(1901, 1, 1);

            for(int i = 1; i <= 20000; i++)
            {
                yield return new Pessoa()
                {
                    Id = i,
                    Nome = "Nome " + i,
                    Idade = random.Next(),
                    DataNascimento = dataNasc.AddYears(random.Next(1, 98))
                };
            }
        }

        public IEnumerable<Pessoa> Carregar(int? fromDate)
        {
            if (!fromDate.HasValue)
                return _containerPessoas.OrderByDescending(p => p.Id).Take(20);

            if (!_containerPessoas.Any(o => o.Id < fromDate))
                return new List<Pessoa>();

            return _containerPessoas.Where(pessoa => pessoa.Id <= fromDate).OrderByDescending(pessoa => pessoa.Id).Take(20);
        }
    }
}
