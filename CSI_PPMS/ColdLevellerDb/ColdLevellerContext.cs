using Microsoft.EntityFrameworkCore;

namespace CSI_PPMS.ColdLevellerDb
{
    public class ColdLevellerContext : DbContext
    {
        public ColdLevellerContext()
        {

        }

        public ColdLevellerContext(DbContextOptions<ColdLevellerContext> options)
            : base(options)
        {

        }


        public virtual DbSet<CoilDataForLoading> CoilDataForLoading { get; set; }

        public virtual DbSet<TableMassGrade> TableMassGrade { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionStrings : ColdLevellerDb");
            }
        }
    }
}
