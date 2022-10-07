using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSI_PPMS.Controllers
{
    public class ReportsController : Controller
    {
        private readonly DateTime date = new(2022, 09, 30);

        private readonly IUnitOfWork _unitOfWork;

        public ReportsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Reports(LoginResponse model)
        {
                return View(model);
        }

        public async Task<PagedResponse<List<PunchingReportModel>>> PlatePunchingReportData(FilterModel model)
        {
            var result = await _unitOfWork.ReportServices.PlatePunchingReportData(model);
            return result;
        }

        public async Task<PagedResponse<List<MarkingReportModel>>> PlateMarkingReportData(FilterModel model)
        {
            var result = await _unitOfWork.ReportServices.PlateMarkingReportData(model);
            return result;
        }

        public async Task<ServiceResponse<string>> PlateMarkingReportDataDownload(FilterModel model)
        {
            var data = await _unitOfWork.ExcellServices.GenerateMarkingReport(model);
            var file = File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MarkingReport.xlsx");
            file.FileDownloadName = "MarkingReport.xlsx";
            var convertToBase64 = Convert.ToBase64String(file.FileContents);
            return new ServiceResponse<string>("base 64", 200, convertToBase64);
        }

        public async Task<ServiceResponse<string>> PlatePunchingReportDataDownload(FilterModel model)
        {
            var data = await _unitOfWork.ExcellServices.GeneratePunchingingReport(model);
            var file = File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MarkingReport.xlsx");
            file.FileDownloadName = "MarkingReport.xlsx";
            var convertToBase64 = Convert.ToBase64String(file.FileContents);
            return new ServiceResponse<string>("base 64", 200, convertToBase64);
        }

        public async Task<PagedResponse<List<ColdLevellerReport>>> ColdLevellerReportData(FilterModel model)
        {
            var res = await _unitOfWork.ReportServices.ColdLevellerReportData(model);
            return res;
        }

        public async Task<ServiceResponse<string>> ColdLevellerReportDataDownload(FilterModel model)
        {
            var data = await _unitOfWork.ExcellServices.GenerateColdLevellerReport(model);
            var file = File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MarkingReport.xlsx");
            file.FileDownloadName = "ColdLevellerreport.xlsx";
            var convertToBase64 = Convert.ToBase64String(file.FileContents);
            return new ServiceResponse<string>("base 64", 200, convertToBase64);
        }



        public async Task<PagedResponse<List<DownCoilerReport>>> DownCoilerReportData(FilterModel model)
        {
            var res = await _unitOfWork.ReportServices.DownCoilerReportData(model);
            return res;
        }

        public async Task<ServiceResponse<string>> DowncoilerReportDataDownload(FilterModel model)
        {
            var data = await _unitOfWork.ExcellServices.GenerateDowncoilerReport(model);
            var file = File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MarkingReport.xlsx");
            file.FileDownloadName = "DowncoilerReport.xlsx";
            var convertToBase64 = Convert.ToBase64String(file.FileContents);
            return new ServiceResponse<string>("base 64", 200, convertToBase64);
        }


        public async Task<PagedResponse<List<ReportData>>> AuditReportData(FilterModel model)
        {
            var res = await _unitOfWork.ReportServices.AuditReportData(model);
            return res;
        }

        public async Task<ServiceResponse<string>> AuditReportDataDownload(FilterModel model)
        {
            var data = await _unitOfWork.ExcellServices.GenerateAuditReport(model);
            var file = File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AuditReport.xlsx");
            file.FileDownloadName = "AuditReport.xlsx";
            var convertToBase64 = Convert.ToBase64String(file.FileContents);
            return new ServiceResponse<string>("base 64", 200, convertToBase64);
        }

        public async Task<PagedResponse<List<WeightUpadteReport>>> WeightUpdateReportData(FilterModel model)
        {
            return await _unitOfWork.ReportServices.WeightUpdateReportData(model);
        }

        public async Task<ServiceResponse<string>> WeightUpdateReportReportDataDownload(FilterModel model)
        {
            var data = await _unitOfWork.ExcellServices.GenerateWeightDataUpdate(model);
            var file = File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ManualWeightUpdateReport.xlsx");
            file.FileDownloadName = "ManualWeightUpdateReport.xlsx";
            var convertToBase64 = Convert.ToBase64String(file.FileContents);
            return new ServiceResponse<string>("base 64", 200, convertToBase64);
        }
    }
}
