using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("check_box_table")]
    public class CheckBoxTable
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("is_marked")]
        public bool IsMarked { get; set; }

        [Column("is_punched")]
        public bool IsPunched { get; set; }

        [Column("module_id")]
        public long? ModuleId { get; set; }
    }
}
