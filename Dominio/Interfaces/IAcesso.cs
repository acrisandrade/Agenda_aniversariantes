using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Iterfaces
{
    public interface IAcesso
    {
        Task<List<Pessoas>> ListaAssincrona();
        void remove(Pessoas p);

        Task Adicionar(Pessoas p);

        List<Pessoas> Ordenar();

        Task<Pessoas> Encontrar(int? id);

        Task SaveChanges();
    }
}
