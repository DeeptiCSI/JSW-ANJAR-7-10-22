using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("module")]
    public class Module
    {
        public Module()
        {
            PlateInfoFromSAP = new HashSet<PlateInfoFromSAP>();
            PlatePunchingRecord = new HashSet<PlatePunchingRecord>();
            PlateMarkingDataForReceipe = new HashSet<PlateMarkingDataForReceipe>();
            PlateMarkingRecord = new HashSet<PlateMarkingRecord>();
            UserRole = new HashSet<UserRole>();
            PlatePunchingDataForReceipe = new HashSet<PlatePunchingDataForReceipe>();
            TCPConfig = new HashSet<TCPConfig>();
            PlateDataFromSapColdLeveller = new HashSet<PlateDataFromSapColdLeveller>();
            SapCredentials = new HashSet<SapCredentials>();
            TemplateMaster = new HashSet<TemplateMaster>();
            AppLogs = new HashSet<AppLogs>();
        }

        [Key]
        [Column("module_id")]
        public long ModuleId { get; set; }

        [Column("module_name")]
        public string ModuleName { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<PlateInfoFromSAP> PlateInfoFromSAP { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<PlatePunchingRecord> PlatePunchingRecord { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<PlateMarkingDataForReceipe> PlateMarkingDataForReceipe { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<PlateMarkingRecord> PlateMarkingRecord { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<UserRole> UserRole { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<PlatePunchingDataForReceipe> PlatePunchingDataForReceipe { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<TCPConfig> TCPConfig { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<PlateDataFromSapColdLeveller> PlateDataFromSapColdLeveller { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<SapCredentials> SapCredentials { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<TemplateMaster> TemplateMaster { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<AppLogs> AppLogs { get; set; }
    }
}
