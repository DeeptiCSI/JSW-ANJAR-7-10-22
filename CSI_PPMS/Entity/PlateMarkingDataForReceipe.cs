using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("plate_marking_data_for_receipe")]
    public class PlateMarkingDataForReceipe
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("plate_id")]
        public long PlateId { get; set; }

        [Column("module_id")]
        public long ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty("PlateMarkingDataForReceipe")]
        public Module Module { get; set; }

        [Column("plate_number")]
        public string PlateNumber { get; set; }

        [Column("line1")]
        public string Line1 { get; set; }

        [Column("line2")]
        public string Line2 { get; set; }

        [Column("line3")]
        public string Line3 { get; set; }

        [Column("line4")]
        public string Line4 { get; set; }

        [Column("line5")]
        public string Line5 { get; set; }

        [Column("line6")]
        public string Line6 { get; set; }

        [Column("time_stamp")]
        public DateTime? TimeStamp { get; set; }

        [Column("marking_position")]
        public long MarkingPosition { get; set; }

        [Column("plc_ack")]
        public int? PlcAck { get; set; }
    }
}
