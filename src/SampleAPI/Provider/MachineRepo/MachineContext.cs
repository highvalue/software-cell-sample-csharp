using Microsoft.EntityFrameworkCore;
using SampleAPI.Contracts;

namespace SampleAPI.Provider
{
    public class MachineContext : DbContext
    {
        public MachineContext(DbContextOptions<MachineContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>().HasKey(x => new { x.Id });          
        }

        public DbSet<Machine> Machines { get; set; }      
    }
}
