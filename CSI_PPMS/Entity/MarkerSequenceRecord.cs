using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("marker_sequence_record")]
    public class MarkerSequenceRecord
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("mode")]
        public string Mode { get; set; }

        [Column("sequence")]
        public string Sequence { get; set; }
    }
}
