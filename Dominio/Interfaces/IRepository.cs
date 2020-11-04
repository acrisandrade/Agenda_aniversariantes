using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IRepository
    {
        Task<List<Pessoas>> ToListAsync();

        Task AddAsync(Pessoas p);

        Task SaveChangesAsync();

        List<Pessoas> OrderByNiver();

        Task<Pessoas> FindAsync(int? id);

        void Remove(Pessoas p);
    }
}
