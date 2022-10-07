using Microsoft.EntityFrameworkCore;

namespace CSI_PPMS.DownCoilerDb
{
    public class DownCoilerContext : DbContext
    {
        public DownCoilerContext()
        {

        }

        public DownCoilerContext(DbContextOptions<DownCoilerContext> options)
            : base(options)
        {

        }





        public virtual DbSet<Coil> Coil { get; set; }











        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("ConnectionStrings : DownCoilerDb");
            }
        }
    }
}
