using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("table_rows")]
    public class TemplateRows
    {
        [Key]
        [Column("id")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("template_master_id")]
        public long TemplateMasterId { get; set; }

        [ForeignKey(nameof(TemplateMasterId))]
        [InverseProperty("TemplateRows")]
        public TemplateMaster TemplateMaster { get; set; }

        [Column("row")]
        public string Row { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("shell_marking")]
        public bool? ShellMarking { get; set; }

        [Column("disc_marking")]
        public bool? DiscMarking { get; set; }
    }
}
