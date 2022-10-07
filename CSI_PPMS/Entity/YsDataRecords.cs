using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("ys_data_records")]
    public class YsDataRecords
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("plate_id")]
        public long PlateId { get; set; }

        [ForeignKey(nameof(PlateId))]
        [InverseProperty("YsDataRecords")]
        public PlateDataFromSapColdLeveller PlateDataFromSapColdLeveller { get; set; }

        [Column("grade")]
        public string Grade { get; set; }

        [Column("ys_value")]
        public string YSValue { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
