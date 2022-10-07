using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("cold_leveller_records")]
    public class ColdLevellerRecords
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }


        [Column("plate_id")]
        public long PlateId { get; set; }

        [ForeignKey(nameof(PlateId))]
        [InverseProperty("ColdLevellerRecords")]
        public PlateDataFromSapColdLeveller PlateDataFromSapColdLeveller { get; set; }

        [Column("plate_number")]
        public string PlateNumber { get; set; }

        [Column("steel_grade")]
        public string SteelGrade { get; set; }

        [Column("thickness")]
        public decimal Thickness { get; set; }

        [Column("width")]
        public decimal Width { get; set; }

        [Column("length")]
        public decimal Length { get; set; }

        [Column("weight")]
        public decimal Weight { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
