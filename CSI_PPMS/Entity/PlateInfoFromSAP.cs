using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSI_PPMS.Entity
{
    [Table("plate_info_from_sap")]
    public class PlateInfoFromSAP
    {
        public PlateInfoFromSAP()
        {
            PlatePunchingRecord = new HashSet<PlatePunchingRecord>();
            PlateMarkingRecord = new HashSet<PlateMarkingRecord>();
        }

        [Key]
        [Column("plate_id")]
        public long PlateId { get; set; }

        [Column("module_id")]
        public long ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty("PlateInfoFromSAP")]
        public Module Module { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("PlateInfoFromSAP")]
        public User User { get; set; }

        [Column("plate_number")]
        public string PlateNumber { get; set; }

        [Column("heat_number")]
        public string HeatNumber { get; set; }

        [Column("length")]
        public string Length { get; set; }

        [Column("width")]
        public string Width { get; set; }

        [Column("thickness")]
        public string Thickness { get; set; }

        [Column("weight")]
        public string Weight { get; set; }

        [Column("purchase_order")]
        public long? PurchaseOrder { get; set; }

        [Column("purchase_order_number")]
        public string PurchaseOrderNumber { get; set; }

        [Column("material_description")]
        public string MaterialDescription { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("customer_reference")]
        public string CustomerReference { get; set; }

        [Column("project_name")]
        public string ProjectName { get; set; }

        [Column("weighing_mode")]
        public int? WeighingMode { get; set; }

        [Column("theoretical_weight")]
        public bool? TheoreticalWeight { get; set; }

        [Column("actual_weight")]
        public string ActualWeight { get; set; }

        [Column("grade")]
        public string Grade { get; set; }

        [Column("grade_duel")]
        public string GradeDuel { get; set; }

        [Column("charecter_count")]
        public int? CharecterCount { get; set; }

        [Column("data_from_sap_date")]
        public DateTime? DataFromSAPDate { get; set; }

        [Column("weight_read_time")]
        public DateTime? WeightReadTime { get; set; }

        [Column("sent_to_punching_time")]
        public DateTime? SentToPunchingTime { get; set; }

        [Column("sent_to_marking_time")]
        public DateTime? SentToMarkingTime { get; set; }

        [Column("update_to_sap_time")]
        public DateTime? UpdateToSAPTime { get; set; }

        [InverseProperty("PlateInfoFromSAP")]
        public virtual ICollection<PlatePunchingRecord> PlatePunchingRecord { get; set; }

        [InverseProperty("PlateInfoFromSAP")]
        public virtual ICollection<PlateMarkingRecord> PlateMarkingRecord { get; set; }

        [InverseProperty("PlateInfoFromSAP")]
        public virtual ICollection<DownCoilerReportsData> DownCoilerReportsData { get; set; }
    }
}
