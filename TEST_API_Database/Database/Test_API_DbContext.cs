using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace TEST_API_Database.Database
{
    public class Test_API_DbContext : DbContext
    {
        public DbSet<Personne> Personnes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=Test_API;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
