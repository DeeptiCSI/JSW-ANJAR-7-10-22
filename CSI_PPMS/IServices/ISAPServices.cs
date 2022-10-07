using CSI_PPMS.Models;
using CSI_PPMS_Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSI_PPMS.IServices
{
    public interface ISAPServices
    {
        Task<ServiceResponse<item>> GetPlateDataFromSAP(SAPInputModel model);

        Task<ServiceResponse<item>> GetSamplePlateData(SAPInputModel model);

        List<NewPlateDataModel> GetLineWiseData(item item, long moduleId, int templateId=0);

        List<NewPlateDataModel> GetLineWiseData(ColdLevellerItem item, long moduleId);

        Task<ServiceResponse<ColdLevellerItem>> GetCLSamplePlateData(SAPInputModel model);

        Task<ServiceResponse<ColdLevellerItem>> GetCLPlateDataFromSAP(SAPInputModel model);

        Task<APIResponse> SavePlateDetail(PlateResponse plateResponse, long moduleId, long userid);

        Task<YSValueModel> GetYSValueFromOracleDb(string plateNo);

        Task<string> GetGrade(string plateNo);

        List<item> GetItemList();

        Task<APIResponse> UpdateYsValue(YSUpdateModel model);
    }
}
