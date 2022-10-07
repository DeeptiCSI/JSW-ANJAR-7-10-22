using System;
using System.Collections.Generic;

namespace CSI_PPMS.Models
{
    public class ReportModels
    {
    }

    public class PunchingReportModel
    {
        public long SLNO { get; set; }

        public string PunchingTime { get; set; }

        public string PlateNumber { get; set; }

        public string HeatNumber { get; set; }

        public string Size { get; set; }

        public string Weight { get; set; }

        public long PurchaseOrder { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string MaterialDescription { get; set; }

        public string CustomerName { get; set; }

        public string CustomerReference { get; set; }

        public string Grade { get; set; }

        public string GradeDuel { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string Line4 { get; set; }

        public string Line5 { get; set; }

        public string Line6 { get; set; }
    }

    public class MarkingReportModel
    {
        public long SLNO { get; set; }

        public string MarkingTime { get; set; }

        public string PlateNumber { get; set; }

        public string HeatNumber { get; set; }

        public string Size { get; set; }

        public string Weight { get; set; }

        public long PurchaseOrder { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string MaterialDescription { get; set; }

        public string CustomerName { get; set; }

        public string CustomerReference { get; set; }

        public string Grade { get; set; }

        public string GradeDuel { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string Line4 { get; set; }

        public string Line5 { get; set; }

        public string Line6 { get; set; }
    }


    public class PagedResponse<T>
    {
        public T ReportData { get; set; }

        public int TotalRecords { get; set; }
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }


    public class ReportData
    {
        public long SLNO { get; set; }

        public string User { get; set; }

        public string Log { get; set; }

        public string LogType { get; set; }

        public string LogTime { get; set; }
    }


    public class ColdLevellerReport
    {
        public long SLNO { get; set; }
        public string SAPFetchTime { get; set; }
        public string PlateNo { get; set; }
        public string Grade { get; set; }
        public string Length { get; set; }
        public string Thick { get; set; }
        public string Width { get; set; }
        public string Weight { get; set; }
        public string YST { get; set; }
        public string PlateNo1 { get; set; }
        public string DBYST { get; set; }
        public string LDDate { get; set; }
        public string LDPlateNo { get; set; }
        public string LDSteelGrade { get; set; }
        public string LDLength { get; set; }
        public string LDThick { get; set; }
        public string LDWidth { get; set; }
        public string LDWeight { get; set; }
    }



    public class DownCoilerReport
    {
        public long SLNO { get; set; }
        public string TimeStamp { get; set; }
        public string MatId { get; set; }
        public string CoilId { get; set; }
        public string HeatNo { get; set; }
        public string Grade { get; set; }
        public string Width { get; set; }
        public string Thickness { get; set; }
        public string Cust_name { get; set; }
        public string p_order { get; set; }
        public string P_Number { get; set; }
        public string AOT_Weight { get; set; }
        public string RecordId { get; set; }
        public string DataLoadDate { get; set; }
        public string DiscLine1 { get; set; }
        public string DiscLine2 { get; set; }
        public string ShellLine1 { get; set; }
        public string ShellLine2 { get; set; }
        public string ShellLine3 { get; set; }
        public string ShellLine4 { get; set; }
        public string LogoStatus { get; set; }
        public string CoilWidth { get; set; }
        public string CoilDiameter { get; set; }
    }


    public class WeightUpadteReport
    {
        public long SlNo { get; set; }
        public string CoilId { get; set; }
        public string Weight { get; set; }
        public string CreatedDate { get; set; }
    }


    public class FilterModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int moduleId { get; set; }
        public string ReportName { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 0;
    }
}
