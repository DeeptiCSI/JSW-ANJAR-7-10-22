using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using CSI_PPMS_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CSI_PPMS.Controllers
{
    public class ConfigureController : Controller
    {
        private readonly DateTime date = new(2022, 09, 30);

        private readonly IUnitOfWork _unitOfWork;

        public ConfigureController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Configure(LoginResponse model)
        {
                return View(model);
        }

        public async Task<SAPLinkModel> GetSapLink(long moduleId)
        {
            var res = await _unitOfWork.ConfigureServices.GetSapLink(moduleId);
            return res;
        }

        public async Task<APIResponse> UpdateSapLink(UpdateSapLinkModel model)
        {
            var res = await _unitOfWork.ConfigureServices.UpdateSapLink(model);
            return res;
        }

        public async Task<APIResponse> UpdateSapCredentials(UpdateSapCredentialsModel model)
        {
            var res = await _unitOfWork.ConfigureServices.UpdateSapCredentials(model);
            return res;
        }


        public async Task UpdatePLCDetails(UpdatePLCModel model)
        {
            await _unitOfWork.ConfigureServices.UpdatePLCDetails(model);
        }
    }
}
