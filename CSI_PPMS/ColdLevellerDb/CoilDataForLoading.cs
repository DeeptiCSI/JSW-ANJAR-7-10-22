using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.ColdLevellerDb
{
    [Table("PDI_CL")]
    public class CoilDataForLoading
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("RecordID")]
        public int RecordId { get; set; }

        [Column("DateTimeInsert")]
        public DateTime? DateTimeInsert { get; set; }

        [Column("PlateNumber")]
        public string PlateNumber { get; set; }

        [Column("SteelGrade")]
        public int SteelGrade { get; set; }

        [Column("Thickness")]
        public double Thickness { get; set; }

        [Column("Width")]
        public double Width { get; set; }

        [Column("Length")]
        public int Length { get; set; }

        [Column("PlateStatus")]
        public byte PlateStatus { get; set; }

        [Column("Weight")]
        public double Weight { get; set; }
    }
}
