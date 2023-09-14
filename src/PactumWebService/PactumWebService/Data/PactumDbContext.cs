using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PactumWebService.Data
{
    public class PactumDbContext:DbContext
    {
        public DbSet<ServiceAnswer> ServiceAnswers { get; set; }
        public PactumDbContext(DbContextOptions<PactumDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ServiceAnswer>().HasIndex(s => new { s.Code, s.Date });
        }
    }
}
