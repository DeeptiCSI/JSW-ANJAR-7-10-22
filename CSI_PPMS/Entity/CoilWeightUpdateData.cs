using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("coil_weight_update_data")]
    public class CoilWeightUpdateData
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("coil_id")]
        public string CoilId { get; set; }

        [Column("weight")]
        public string Weight { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}
