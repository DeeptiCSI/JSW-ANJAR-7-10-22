using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.DownCoilerDb
{
    [Table("COILWEIGHT")]
    public class Coil
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("MATID")]
        public int MatId { get; set; }

        [Column("DISCHARGETIM")]
        public DateTime? DischargeTime { get; set; }

        [Column("PRODUCTIONID")]
        public string ProductionId { get; set; }

        [Column("STEELGRADE")]
        public string SteelGrade { get; set; }

        [Column("HEATNO")]
        public string HeatNumber { get; set; }

        [Column("CUSTOMERDETAIL")]
        public string CustomerDetail { get; set; }

        [Column("ORDERTHICK")]
        public decimal? OrderThickness { get; set; }

        [Column("ORDERWIDTH")]
        public decimal? OrderWidth { get; set; }

        [Column("ORDERLEN")]
        public decimal? OrderLength { get; set; }

        [Column("STATUS")]
        public int? Status { get; set; }

        [Column("WEIGHT")]
        public int? Weight { get; set; }

        [Column("POS")]
        public int? Position { get; set; }

        [Column("COIL_DIA")]
        public int? CoilDiameter { get; set; }

        [Column("MSG_FLAG")]
        public int? MessegeFlag { get; set; }

        [Column("SLABWIDTH")]
        public decimal? SlabWidth { get; set; }

        [Column("SLABTHICK")]
        public decimal? SlabThick { get; set; }

        [Column("SLABLEN")]
        public decimal? SlabThickness { get; set; }

    }
}
