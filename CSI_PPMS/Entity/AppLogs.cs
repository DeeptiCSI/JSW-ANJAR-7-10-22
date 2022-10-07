using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("app_logs")]
    public class AppLogs
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("user_id")]
        public long? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("AppLogs")]
        public User User { get; set; }

        [Column("module_id")]
        public long? ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty("AppLogs")]
        public Module Module { get; set; }

        [Column("log")]
        public string Log { get; set; }

        [Column("log_type")]
        public string LogType { get; set; }

        [Column("log_date")]
        public DateTime LogDate { get; set; } = DateTime.Now;
    }
}
