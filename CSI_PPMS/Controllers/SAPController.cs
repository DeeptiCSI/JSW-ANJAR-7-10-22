using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using CSI_PPMS_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSI_PPMS.Controllers
{
    public class SAPController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public SAPController(IUnitOfWork unitOfWork,
                             IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<List<NewPlateDataModel>>> GetPlateData(SAPInputModel model)
        {
            var res = new ServiceResponse<item>();
            var CLres = new ServiceResponse<ColdLevellerItem>();
            bool plantState = false;
            if (model.plateNo == "PA31980B1")
            {
                plantState = false;
            }
            else
            {
                plantState = _configuration.GetValue<bool>("DevelopmentState:IsLive");
            }
            if (plantState)
            {
                if (model.moduleId == 4)
                {
                    CLres = await _unitOfWork.SAPServices.GetCLPlateDataFromSAP(model);
                    var result = _unitOfWork.SAPServices.GetLineWiseData(CLres.Data, model.moduleId);
                    if (result == null)
                        return new ServiceResponse<List<NewPlateDataModel>>("error", 400, null);
                    return new ServiceResponse<List<NewPlateDataModel>>(CLres.Messege, 200, result);
                }
                else
                {
                    res = await _unitOfWork.SAPServices.GetPlateDataFromSAP(model);
                    var result = _unitOfWork.SAPServices.GetLineWiseData(res.Data, model.moduleId, model.templateId);
                    return new ServiceResponse<List<NewPlateDataModel>>(res.Messege, 200, result);
                }
            }
            else
            {
                if (model.moduleId == 4)
                {
                    CLres = await _unitOfWork.SAPServices.GetCLSamplePlateData(model);
                    if (CLres.StatusCode != 200)
                        return new ServiceResponse<List<NewPlateDataModel>>(CLres.Messege, 200, null);
                    var result = _unitOfWork.SAPServices.GetLineWiseData(CLres.Data, model.moduleId);
                    return new ServiceResponse<List<NewPlateDataModel>>(CLres.Messege, 200, result);
                }
                else
                {
                    res = await _unitOfWork.SAPServices.GetSamplePlateData(model);
                    if (res.StatusCode != 200)
                        return new ServiceResponse<List<NewPlateDataModel>>(res.Messege, 200, null);
                    var result = _unitOfWork.SAPServices.GetLineWiseData(res.Data, model.moduleId,model.templateId);
                    return new ServiceResponse<List<NewPlateDataModel>>(res.Messege, 200, result);
                }
            }
        }

        public async Task<YSValueModel> GetYSValueFromOracleDb(string plateNo)
        {
            var res = await _unitOfWork.SAPServices.GetYSValueFromOracleDb(plateNo);
            var res1 = new YSValueModel();
            res1.YS = 20;
            res1.Grade = "123";
            return res;
        }

        public async Task<string> GetGrade(string plateNo)
        {
            var res = await _unitOfWork.SAPServices.GetGrade(plateNo);
            return res;
        }

        public async Task<APIResponse> UpdateYsValue(YSUpdateModel model)
        {
            if (!ModelState.IsValid)
                return new APIResponse("invalid input", 400);
            var res = await _unitOfWork.SAPServices.UpdateYsValue(model);
            return res;
        }


    }
}
