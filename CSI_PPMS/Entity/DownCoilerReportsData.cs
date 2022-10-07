using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("down_coiler_reports_data")]
    public class DownCoilerReportsData
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("plate_id")]
        public long PlateId { get; set; }

        [ForeignKey(nameof(PlateId))]
        [InverseProperty("DownCoilerReportsData")]
        public PlateInfoFromSAP PlateInfoFromSAP { get; set; }

        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }

        [Column("disc_line1")]
        public string DiscLine1 { get; set; }

        [Column("disc_line2")]
        public string DiscLine2 { get; set; }

        [Column("shell_line1")]
        public string ShellLine1 { get; set; }

        [Column("shell_line2")]
        public string ShellLine2{ get; set; }

        [Column("shell_line3")]
        public string ShellLine3 { get; set; }

        [Column("shell_line4")]
        public string ShellLine4 { get; set; }

        [Column("logo_status")]
        public string LogoStatus { get; set; }

        [Column("coil_width")]
        public string CoilWidth { get; set; }

        [Column("coil_dismeter")]
        public string CoilDiameter { get; set; }

        [Column("mat_id")]
        public string MatId { get; set; }
    }
}
