using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.ColdLevellerDb
{
    [Table("Tbl_Mas_Grade")]
    public class TableMassGrade
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("Sr_No")]
        public decimal SerialNumber { get; set; }

        [Column("GradeName")]
        public string GradeName { get; set; }

        [Column("MinThick")]
        public decimal MinimumThickNess { get; set; }

        [Column("MaxThick")]
        public decimal MaximumThickNess { get; set; }

        [Column("YS")]
        public decimal YS { get; set; }
    }
}
