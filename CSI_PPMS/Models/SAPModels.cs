namespace CSI_PPMS_Web.Models
{
    public class SAPModels
    {
    }

    public class PlateRequest
    {
        public string CHARG { get; set; }
    }

    public class DownCoilerPlateRequest
    {
        public string CHARG { get; set; }
        public decimal WEIGHT { get; set; }
    }

    public class PlateResponse
    {
        public T_ZPLATE_MARKING T_ZPLATE_MARKING { get; set; }
    }

    public class T_ZPLATE_MARKING
    {
        public item item { get; set; }
    }

    public class item
    {
        public string CHARG { get; set; }
        public string HEAT_NO { get; set; }
        public string LENGTH { get; set; }
        public string WIDTH { get; set; }
        public string THICK { get; set; }
        public string WEIGHT { get; set; }
        public long? P_ORDER { get; set; }
        public string PO_NUMBER { get; set; }
        public string ARKTX { get; set; }
        public string CUST_NAME { get; set; }
        public string PROJ_NAME { get; set; }
        public string GRADE { get; set; }
        public decimal ACT_WEIGHT { get; set; }
        public string GRADE_D { get; set; }
        public string CUSTOMER_REF { get; set; }
    }


    public class ColdLevellerItem
    {
        public string CHARG { get; set; }
        public string GRADE { get; set; }
        public string LNTH { get; set; }
        public string THK { get; set; }
        public string WDTH { get; set; }
        public string WEIGHT { get; set; }
        public string YS_T { get; set; }
        public string RETURN1 { get; set; }
    }

    public class ColdLevellerItemResponse
    {
        public string GRADE { get; set; }
        public string LNTH { get; set; }
        public string THK { get; set; }
        public string WDTH { get; set; }
        public string WEIGHT { get; set; }
        public string YS_T { get; set; }
        public string RETURN1 { get; set; }
    }



    public class ItemPreview
    {
        public string JSW { get; set; }
        public string PLATE_NO { get; set; }
        public string HEAT_NO { get; set; }
        public string GRADE_NO_1 { get; set; }
        public string GRADE_NO_2 { get; set; }
        public string SIZE { get; set; }
        public string CUSTOMER_REF { get; set; }
        public string PO_ORDER { get; set; }
        public string PO_NUMBER { get; set; }
        public string WEIGHT { get; set; }
        public string CUSTOMER_NAME { get; set; }
    }



    public class ItemDownCoilerPreview
    {
        public string JSW { get; set; }
        public string COIL_NO { get; set; }
        public string HEAT_NO { get; set; }
        public string WIDTH { get; set; }
        public string THICK { get; set; }
        public string P_ORDER { get; set; }
        public string PO_NUMBER { get; set; }
        public string CUST_NAME { get; set; }
        public string GRADE { get; set; }
        public string ACT_WEIGHT { get; set; }
    }


    public class ColdLevellerItemPreview
    {
        public string YTS { get; set; }
        public string LENGTH { get; set; }
        public string THICKNESS { get; set; }
        public string WIDTH { get; set; }
        public string WEIGHT { get; set; }
    }

    public class NewPlateDataModel
    {
        public int LineNo { get; set; }
        public bool Punching { get; set; }
        public bool Marking { get; set; }
        public string Prefix_Text { get; set; }
    }



    public class SAPInputModel
    {
        public int roleId { get; set; }

        public string plateNo { get; set; }

        public int moduleId { get; set; }

        public int templateId { get; set; }
    }


    public class UpdateSapLinkModel
    {
        public string SAPLink { get; set; }
        public long SapLinkId { get; set; }
        public string SAPUserName { get; set; }
        public string SAPPassword { get; set; }

        public long typeid { get; set; }
    }

    public class UpdateSapCredentialsModel
    {
        public string SAPUserName { get; set; }
        public string SAPPassword { get; set; }
        public long SapLinkId { get; set; }
    }


    public class UpdatePLCModel
    {
        public long ModuleId { get; set; }
        public string TIP { get; set; }
        public int TSlot { get; set; }
        public int TRack { get; set; }
        public long TPort { get; set; }
        public string PIP { get; set; }
        public long PPort { get; set; }
        public long tid { get; set; }
    }


    public class SAPLinkModel
    {
        public long SapLinkId { get; set; }
        public string SAPLink { get; set; }

        public string SapUserName { get; set; }

        public string SapPassword { get; set; }

        public long TCPId { get; set; }
        public string TechniforIP { get; set; }
        public string TechniforPort { get; set; }
        public string Techniforslot { get; set; }
        public string TechniforRack { get; set; }
        public string PLCIP { get; set; }
        public string PLCSlot { get; set; }
        public string PLCRack { get; set; }
        public string PLCPort { get; set; }
        public long? SapWeightId { get; set; }
        public string SapWeightLink { get; set; }
    }


    public class YSUpdateModel
    {
        public string PlateNo { get; set; }
        public string Grade { get; set; }
        public string MininumThickness { get; set; }
        public string MaximumThickness { get; set; }
        public string YSValue { get; set; }
    }

    public class YSValueModel
    {
        public string Grade { get; set; }
        public int YS { get; set; }
    }
}
