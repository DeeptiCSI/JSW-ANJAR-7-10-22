using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("user")]
    public class User
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
            PlateInfoFromSAP = new HashSet<PlateInfoFromSAP>();
            TCPConfig = new HashSet<TCPConfig>();
            PlateDataFromSapColdLeveller = new HashSet<PlateDataFromSapColdLeveller>();
            TemplateMaster = new HashSet<TemplateMaster>();
            AppLogs = new HashSet<AppLogs>();
        }

        [Key]
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("idDeleted")]
        public bool? IsDeleted { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRole { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<PlateInfoFromSAP> PlateInfoFromSAP { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<TCPConfig> TCPConfig { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<PlateDataFromSapColdLeveller> PlateDataFromSapColdLeveller { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<TemplateMaster> TemplateMaster { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<AppLogs> AppLogs { get; set; }
    }
}
