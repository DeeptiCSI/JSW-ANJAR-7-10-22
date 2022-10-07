using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("punching_cycle_status")]
    public class PunchingCycleStatus
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("Marking_Status_Ack")]
        public long MarkingStatusACK { get; set; }

        [Column("PLC_MODE")]
        public int PLC_MODE { get; set; }

        [Column("Data_Request_Ack")]
        public long DataRequestACK { get; set; }

        [Column("Start_Punching")]
        public long StartPunching { get; set; }

        [Column("Marking_Complete_Ack")]
        public long MarkingCompleteACK { get; set; }
    }
}
