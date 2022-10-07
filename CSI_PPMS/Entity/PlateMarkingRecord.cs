using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("plate_marking_record")]
    public class PlateMarkingRecord
    {
        [Key]
        [Column("marking_id")]
        public long MarkingId { get; set; }

        [Column("module_id")]
        public long? ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty("PlateMarkingRecord")]
        public Module Module { get; set; }

        [Column("plate_number")]
        public string PlateNumber { get; set; }

        [Column("plate_id")]
        public long PlateId { get; set; }

        [ForeignKey(nameof(PlateId))]
        [InverseProperty("PlateMarkingRecord")]
        public PlateInfoFromSAP PlateInfoFromSAP { get; set; }

        [Column("time_stamp")]
        public DateTime? TimeStamp { get; set; }

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
    }

}
