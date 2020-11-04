using Acesso_dados.Repositorio;
using Dominio.Interfaces;
using Dominio.Iterfaces;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.servicos
{
    public class Acesso : IAcesso
    {
        public IRepository repository { get; set; }

        public Acesso (IRepository repositorio)
        {
            repository = repositorio;
        }

        public async Task<List<Pessoas>> ListaAssincrona()
        {
            var r = await repository.ToListAsync();
            return r;
        }

        public async Task Adicionar(Pessoas p)
        {
            await repository.AddAsync(p);
        }

        public async Task SaveChanges()
        {
            await repository.SaveChangesAsync();

        }

        public List<Pessoas> Ordenar()
        {
            return repository.OrderByNiver();
        }

        public async Task<Pessoas> Encontrar(int? id)
        {
            return  await repository.FindAsync(id);
        }

        public void remove(Pessoas p)
        {
            repository.Remove(p);
        }


    }
}
