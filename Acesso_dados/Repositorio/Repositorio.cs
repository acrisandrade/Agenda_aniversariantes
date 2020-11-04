using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acesso_dados.Repositorio
{
    public class Repositorio : IRepository
    {
        public Repository repository;
        public DbSet<Pessoas> _dataset;

        public Repositorio(Repository repository)
        {
            this.repository = repository;
            _dataset = repository.Set<Pessoas>();
        }

        public async Task<List<Pessoas>> ToListAsync()
        {
            return await _dataset.ToListAsync();
        }

        public async Task AddAsync(Pessoas p)
        {
            await _dataset.FindAsync(p);
        }

        public async Task<Pessoas> FindAsync(int? id)
        {
            var p = await _dataset.FindAsync(id);
            return p;
        }

        public List<Pessoas> OrderByNiver()
        {
            var l = _dataset.OrderBy(d => d.Aniversario).ToList();
            return l;
        }

        public void Remove(Pessoas p)
        {
            _dataset.Remove(p);
        }

        public async Task SaveChangesAsync()
        {
            await repository.SaveChangesAsync();
        }
    }
}
