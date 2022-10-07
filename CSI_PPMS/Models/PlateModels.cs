using System.Collections.Generic;

namespace CSI_PPMS.Models
{
    public class PlateModels
    {
    }



    public class FL1PunchingModel
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public string Line5 { get; set; }
        public string Line6 { get; set; }
        public string plateNo { get; set; }

    }

    public class FL1MarkingModel
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public string Line5 { get; set; }
        public string Line6 { get; set; }
        public string plateNo { get; set; }

    }


    public class DownCoilerMarkingModel
    {
        public string discLine1 { get; set; }
        public string discLine2 { get; set; }
        public bool isDisc { get; set; }
        public bool isShell { get; set; }
        public string shellLine1 { get; set; }
        public string shellLine2 { get; set; }
        public string shellLine3 { get; set; }
        public string shellLine4 { get; set; }
        public string plateNo { get; set; }

    }

    public class DownCoilerModel
    {
        public string ModuleId { get; set; }
        public string RoleId { get; set; }

        public string matid { get; set; }

        public bool AutoMode { get; set; }
    }



    public class DownCoilerAutoModeModel
    {
        public long MatId { get; set; }
        public long Position { get; set; }
        public long LiveWeight { get; set; }
        public bool MachineMode { get; set; }
        public bool MarkerHomePosition { get; set; }
        public bool MarkerActive { get; set; }
        public bool MarkerReady { get; set; }
        public bool MarkerFault { get; set; }
        public bool MarkingCycleStatus { get; set; }
        public bool MarkingAbortStatus { get; set; }

    }




    public class CheckAutoModeResponse
    {
        public bool ConnectionStatus { get; set; }
        public bool DataReadStatus { get; set; }
        public bool AutoModeStatus { get; set; }
        public bool MarkerReadyStatus { get; set; }
        public bool MarkerInHomePosition { get; set; }
        public bool MarkerFault { get; set; }
        public bool MarkerActive { get; set; }
        public bool MarkerCycleFinished { get; set; }
        public bool MarkerCycleAbourted { get; set; }
        public bool PrepareToMarking { get; set; }
        public bool StartMarking { get; set; }
        public bool StopMarking { get; set; }
    }



    public class UpdateWeightDataModel
    {
        public string CHARG { get; set; }
        public string WEIGHT { get; set; }
    }






    public class TemplateDropDown
    {
        public long Id { get; set; }
        public string TemplateName { get; set; }

        public bool IsDefault { get; set; }
    }


    public class TableRows
    {
        public long Id { get; set; }

        public string Row { get; set; }

        public bool? IsDisc { get; set; }

        public bool? IsShell { get; set; }
    }


    public class AddTemplateModel
    {
        public string templateName { get; set; }
        public List<string> Lines { get; set; }
        public long moduleId { get; set; }

        public bool isDefault { get; set; }

        public int templateId { get; set; }
    }

    public class AddDCTemplateModel
    {
        public string templateName { get; set; }
        public List<TemplateRowsDC> Lines { get; set; }
        public long moduleId { get; set; }

        public bool isDefault { get; set; }

        public int templateId { get; set; }
    }


    public class TemplateRowsDC
    {
        public string row { get; set; }
        public bool isDisc { get; set; }
        public bool isShell { get; set; }
    }



    public class DefaultTemplateModel
    {
        public long TemplateId { get; set; }
        public long ModeuleId { get; set; }
    }


    public class ColdLevellerMarkingModel
    {
        public string PlateNumber { get; set; }
        public string SteelGrade { get; set; }
        public string Thickness { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Weight { get; set; }
        public string YTS { get; set; }
        public string plateNo { get; set; }

    }


    public class MechineModeResponse
    {
        public string Mode { get; set; }
        public string Sequence { get; set; }
    }


    public class ManualWeightUpdate
    {
        public string CoilId { get; set; }
        public string Weight { get; set; }
    }


    public class WeightUpdateModel
    {
        public string CHARG { get; set; }
        public string WEIGHT { get; set; }
    }




    public class OracleWeightDataResponse
    {
        public long  MatId { get; set; }
        public string CoilId { get; set; }
        public long Position { get; set; }
        public int? Weight { get; set; }
    }
}
