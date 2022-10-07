using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("template_master")]
    public class TemplateMaster
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("template_name")]
        public string TemplateName { get; set; }

        [Column("module_id")]
        public long ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty("TemplateMaster")]
        public Module Module { get; set; }

        [Column("createdDate")]
        public DateTime CreatedDate { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        [Column("created_by")]
        public long CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty("TemplateMaster")]
        public User User { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("is_default")]
        public bool IsDefault { get; set; }

        public virtual ICollection<TemplateRows> TemplateRows { get; set; }

    }
}
