using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("sap_credentials")]
    public class SapCredentials
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("sap_link")]
        public string SAPLink { get; set; }

        [Column("sap_username")]
        public string SAPUserName { get; set; }

        [Column("sap_password")]
        public string SAPPassword { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        [Column("type")]
        public long? Type { get; set; }

        [Column("module_id")]
        public long? ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty("SapCredentials")]
        public Module Module { get; set; }
    }
}
