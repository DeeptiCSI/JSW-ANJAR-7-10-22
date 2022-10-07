using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("user_role")]
    public class UserRole
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("user_role_id")]
        public long UserRoleId { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserRole")]
        public User User { get; set; }

        [Column("role_id")]
        public long RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("UserRole")]
        public Role Role { get; set; }

        [Column("module_id")]
        public long ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty("UserRole")]
        public Module Module { get; set; }
    }
}
