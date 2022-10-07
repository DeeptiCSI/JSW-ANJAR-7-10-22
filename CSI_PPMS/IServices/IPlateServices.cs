using CSI_PPMS.DowncoilerModels;
using CSI_PPMS.Models;
using CSI_PPMS_Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSI_PPMS.IServices
{
    public interface IPlateServices
    {
        Task<APIResponse> SendToPunching(FL1PunchingModel model);

        Task<APIResponse> SendToMarking(FL1MarkingModel model);

        bool SaveCheckMark(long id, bool status);

        bool SaveCheckPunch(long id, bool status);

        Task AddTemplate(AddTemplateModel model);

        void RefreshChecks();

        public string GetWeighingData();

        Task<List<TableRows>> GetTemplateRows(long TemplateId);

        Task<APIResponse> LoadDataColdLeveller(ColdLevellerMarkingModel model);

        APIResponse SendToPunchingDownCoiler(DownCoilerMarkingModel model);

        Task<ServiceResponse<List<NewPlateDataModel>>> GetSapDataDownCoiler(DownCoilerModel model);

        CheckAutoModeResponse CheckAutoMode();

        CheckAutoModeResponse CheckMarkerReady();


        Task<List<TemplateDropDown>> GetTemplateDropDown(long ModuleId);

        Task MakeTemplateDefault(DefaultTemplateModel model);

        MechineModeResponse GetMarkerSequence();

        void DeleteTemplate(int templateId);

        Task<ServiceResponse<DCWeightResponse>> UpdateWeightInOracleAndSAP(string Weight);

        void SaveLogs(string userId, string moduleId, string log, string type);

        Task<ServiceResponse<string>> UpdateWeightdata(string weight);

        void SetMarkingBit();

        OracleWeightDataResponse GetCoilPositionFromOracle();

        DownCoilerAutoModeModel DCAutoModeData();

        Task<ServiceResponse<string>> ManualWeightUpdate(ManualWeightUpdate model);

        Task AddDCTemplate(AddDCTemplateModel model);
    }
}
