using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("plate_data_from_sap_cold_leveller")]
    public class PlateDataFromSapColdLeveller
    {
        public PlateDataFromSapColdLeveller()
        {
            ColdLevellerRecords = new HashSet<ColdLevellerRecords>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("plate_number")]
        public string PlateNumber { get; set; }

        [Column("grade")]
        public string Grade { get; set; }

        [Column("length")]
        public string Length { get; set; }

        [Column("thickness")]
        public string Thickness { get; set; }

        [Column("width")]
        public string Width { get; set; }

        [Column("weight")]
        public string Weight { get; set; }

        [Column("ys_t")]
        public string YST { get; set; }

        [Column("return1")]
        public string Return1 { get; set; }

        [Column("module_id")]
        public long ModuleId { get; set; }

        [Column("created_date")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty("PlateDataFromSapColdLeveller")]
        public Module Module { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("PlateDataFromSapColdLeveller")]
        public User User { get; set; }

        [InverseProperty("PlateDataFromSapColdLeveller")]
        public virtual ICollection<ColdLevellerRecords> ColdLevellerRecords { get; set; }

        [InverseProperty("PlateDataFromSapColdLeveller")]
        public virtual ICollection<YsDataRecords> YsDataRecords { get; set; }
    }
}
