using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("tcp_config")]
    public class TCPConfig
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("technifor_ip_address")]
        public string TechniforIPAddress { get; set; }

        [Column("plc_ip_address")]
        public string PLCIPAddress { get; set; }

        [Column("technifor_port_no")]
        public int TechniforPortNo { get; set; }

        [Column("plc_port_no")]
        public int PLCPortNo { get; set; }

        [Column("technifor_rack")]
        public int? TechniforRack { get; set; }

        [Column("technifor_slot")]
        public int? TechniforSlot { get; set; }

        [Column("plc_rack")]
        public int? PLCRack { get; set; }

        [Column("PLC_slot")]
        public int? PLCSlot { get; set; }

        [Column("user_id")]
        public long? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("TCPConfig")]
        public User User { get; set; }

        [Column("module_id")]
        public long? ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty("TCPConfig")]
        public Module Module { get; set; }
    }
}
