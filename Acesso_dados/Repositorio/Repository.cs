using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace Acesso_dados.Repositorio
{
    public class Repository : DbContext
    {
        public Repository (DbContextOptions<Repository> options) : base (options)
        {

        }

        public DbSet<Pessoas> pessoas { get; set; }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Repository>
        {
            public Repository CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../AT_asp/appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<Repository>();
                var connectionString = configuration.GetConnectionString("PessoasRepositorio");
                builder.UseSqlServer(connectionString);
                return new Repository(builder.Options);
            }
        }
    }
   
}
