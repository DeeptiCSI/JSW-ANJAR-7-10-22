using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CSI_PPMS.Entity
{
    public class PPMSContext : DbContext
    {
        public PPMSContext(DbContextOptions<PPMSContext> options)
            : base(options)
        {

        }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Role> Role { get; set; }

        public virtual DbSet<UserRole> UserRole { get; set; }

        public virtual DbSet<PlateInfoFromSAP> PlateInfoFromSAP { get; set; }

        public virtual DbSet<Module> Module { get; set; }

        public virtual DbSet<PlatePunchingRecord> PlatePunchingRecord { get; set; }

        public virtual DbSet<PlateMarkingDataForReceipe> PlateMarkingDataForReceipe { get; set; }

        public virtual DbSet<PlatePunchingDataForReceipe> PlatePunchingDataForReceipe { get; set; }

        public virtual DbSet<PlateMarkingRecord> PlateMarkingRecord { get; set; }

        public virtual DbSet<CheckBoxTable> CheckBoxTable { get; set; }

        public virtual DbSet<SapCredentials> SapCredential { get; set; }

        public virtual DbSet<TCPConfig> TCPConfig { get; set; }

        public virtual DbSet<PunchingCycleStatus> PunchingCycleStatus { get; set; }

        public virtual DbSet<ColdLevellerRecords> ColdLevellerRecords { get; set; }

        public virtual DbSet<PlateDataFromSapColdLeveller> PlateDataFromSapColdLeveller { get; set; }

        public virtual DbSet<TemplateMaster> TemplateMaster { get; set; }

        public virtual DbSet<TemplateRows> TemplateRows { get; set; }

        public virtual DbSet<MarkerSequenceRecord> MarkerSequenceRecord { get; set; }

        public virtual DbSet<AppLogs> AppLogs { get; set; }

        public virtual DbSet<YsDataRecords> YsDataRecords { get; set; }
        public virtual DbSet<DownCoilerReportsData> DownCoilerReportsData { get; set; }

        public virtual DbSet<CoilWeightUpdateData> CoilWeightUpdateData { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionStrings : Default Connection");
            }
        }
    }
}
