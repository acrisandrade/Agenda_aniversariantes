using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acesso_dados.Repositorio;
using Microsoft.Extensions.DependencyInjection;
using Dominio.Models;

namespace Acesso_dados.Repositorio
{
    public class MigrationsData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Repository(
                serviceProvider.GetRequiredService<DbContextOptions<Repository>>()))
            {
                if (context.pessoas.Any())
                {
                    return;
                }

                context.pessoas.AddRange(
                    new Pessoas
                    {
                        Nome = "Jon",
                        Sobrenome = "Snow",
                        Aniversario = DateTime.Parse("22/12/1986")
                    },
                     new Pessoas
                     {
                         Nome = "Pumba",
                         Sobrenome = "Amigo do Timão",
                         Aniversario = DateTime.Parse("16/05/1998")
                     },
                      new Pessoas
                      {
                          Nome = "Daenerys",
                          Sobrenome = "Targaryen",
                          Aniversario = DateTime.Parse("23/10/1986")
                      }
                    );
                context.SaveChanges();
            }
        }
    }
}
