using CSI_PPMS.DowncoilerModels;
using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using CSI_PPMS_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSI_PPMS.Controllers
{
    public class PlateController : Controller
    {
        private readonly DateTime date = new(2022, 09, 30);

        private readonly IUnitOfWork _unitOfWork;

        public PlateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(LoginResponse model)
        {
                return View(model);
        }


        public async Task<APIResponse> SendToPunching(FL1PunchingModel model)
        {
            var res = await _unitOfWork.PlateServices.SendToPunching(model);
            return res;
        }

        public async Task<APIResponse> SendToMarking(FL1MarkingModel model)
        {
            var res = await _unitOfWork.PlateServices.SendToMarking(model);
            return res;
        }

        public void SaveCheckMark(int id, bool status)
        {
            _unitOfWork.PlateServices.SaveCheckMark(id, status);
        }

        public void SaveCheckPunch(int id, bool status)
        {
            _unitOfWork.PlateServices.SaveCheckPunch(id, status);
        }

        public void RefreshChecks()
        {
            _unitOfWork.PlateServices.RefreshChecks();
        }

        public async Task<APIResponse> LoadDataColdLeveller(ColdLevellerMarkingModel model)
        {
            var res = await _unitOfWork.PlateServices.LoadDataColdLeveller(model);
            return res;
        }


        public CheckAutoModeResponse CheckAutoMode()
        {
            var res = _unitOfWork.PlateServices.CheckAutoMode();
            return res;
        }

        public CheckAutoModeResponse CheckMarkerReady()
        {
            var res = _unitOfWork.PlateServices.CheckMarkerReady();
            return res;
        }


        public async Task<ServiceResponse<List<NewPlateDataModel>>> GetSapDataDownCoiler(DownCoilerModel model)
        {
            var res = await _unitOfWork.PlateServices.GetSapDataDownCoiler(model);
            return res;
        }

        public string GetWeighingData()
        {
            var res = _unitOfWork.PlateServices.GetWeighingData();
            return res;
        }





        public APIResponse SendToPunchingDownCoiler(DownCoilerMarkingModel model)
        {
            var res = _unitOfWork.PlateServices.SendToPunchingDownCoiler(model);
            return res;
        }

        public async Task<List<TemplateDropDown>> GetTemplateDropDown(long ModuleId)
        {
            var res = await _unitOfWork.PlateServices.GetTemplateDropDown(ModuleId);
            return res;
        }

        public async Task<List<TableRows>> GetTemplateRows(long TemplateId)
        {
            var res = await _unitOfWork.PlateServices.GetTemplateRows(TemplateId);
            return res;
        }

        public async Task AddTemplate(AddTemplateModel model)
        {
            await _unitOfWork.PlateServices.AddTemplate(model);
        }


        public async Task MakeTemplateDefault(DefaultTemplateModel model)
        {
            await _unitOfWork.PlateServices.MakeTemplateDefault(model);
        }


        public MechineModeResponse GetMarkerSequence()
        {
            var res = _unitOfWork.PlateServices.GetMarkerSequence();
            return res;
        }

        public void DeleteTemplate(int templateId)
        {
            _unitOfWork.PlateServices.DeleteTemplate(templateId);
        }

        public async Task<ServiceResponse<DCWeightResponse>> UpdateWeightInOracleAndSAP(string Weight)
        {
            var res = await _unitOfWork.PlateServices.UpdateWeightInOracleAndSAP(Weight);
            return res;
        }


        public void SaveLogs(string userId, string moduleId, string log, string type)
        {
            _unitOfWork.PlateServices.SaveLogs(userId, moduleId, log, type);
        }

        public async Task<ServiceResponse<string>> UpdateWeightdata(string weight)
        {
            return await _unitOfWork.PlateServices.UpdateWeightdata(weight);
        }

        public void SetMarkingBit()
        {
            _unitOfWork.PlateServices.SetMarkingBit();
        }

        public OracleWeightDataResponse GetCoilPositionFromOracle()
        {
            return _unitOfWork.PlateServices.GetCoilPositionFromOracle();
        }

        public DownCoilerAutoModeModel DCAutoModeData()
        {
            return _unitOfWork.PlateServices.DCAutoModeData();
        }

        public async Task<ServiceResponse<string>> ManualWeightUpdate(ManualWeightUpdate model)
        {
            return await _unitOfWork.PlateServices.ManualWeightUpdate(model);
        }

        public async Task AddDCTemplate(AddDCTemplateModel model)
        {
            await _unitOfWork.PlateServices.AddDCTemplate(model); 
        }

    }
}
