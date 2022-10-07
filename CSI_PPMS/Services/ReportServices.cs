using CSI_PPMS.Entity;
using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSI_PPMS.Services
{
    public class ReportServices : IReportServices
    {
        private readonly PPMSContext _context;

        public ReportServices(PPMSContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<List<PunchingReportModel>>> PlatePunchingReportData(FilterModel model)
        {
            var res = new PagedResponse<List<PunchingReportModel>>();
            res.PageIndex = model.PageIndex;
            res.PageSize = model.PageSize;
            res.TotalRecords = await _context.PlatePunchingRecord.Include(x => x.PlateInfoFromSAP)
                                                               .Where(x => x.ModuleId == model.moduleId &&
                                                                           x.TimeStamp.Value.Date >= model.FromDate.Date &&
                                                                           x.TimeStamp.Value.Date <= model.ToDate.Date)
                                                               .CountAsync();
            res.ReportData = await _context.PlatePunchingRecord.Include(x => x.PlateInfoFromSAP)
                                                               .Where(x => x.ModuleId == model.moduleId &&
                                                                           x.TimeStamp.Value.Date >= model.FromDate.Date &&
                                                                           x.TimeStamp.Value.Date <= model.ToDate.Date)
                                                               .Select(x => new PunchingReportModel
                                                               {
                                                                   PunchingTime = x.TimeStamp.Value.ToString("dd-MM-yyyy : HH-mm-ss"),
                                                                   PlateNumber = x.PlateNumber,
                                                                   HeatNumber = x.PlateInfoFromSAP.HeatNumber,
                                                                   Size = $"{x.PlateInfoFromSAP.Length} X {x.PlateInfoFromSAP.Width} X {x.PlateInfoFromSAP.Thickness}",
                                                                   Weight = x.PlateInfoFromSAP.Weight,
                                                                   PurchaseOrder = x.PlateInfoFromSAP.PurchaseOrder.Value,
                                                                   PurchaseOrderNumber = x.PlateInfoFromSAP.PurchaseOrderNumber,
                                                                   MaterialDescription = x.PlateInfoFromSAP.MaterialDescription,
                                                                   CustomerName = x.PlateInfoFromSAP.CustomerName,
                                                                   CustomerReference = x.PlateInfoFromSAP.CustomerReference,
                                                                   Grade = x.PlateInfoFromSAP.Grade,
                                                                   GradeDuel = x.PlateInfoFromSAP.GradeDuel,
                                                                   Line1 = x.Line1,
                                                                   Line2 = x.Line2,
                                                                   Line3 = x.Line3,
                                                                   Line4 = x.Line4,
                                                                   Line5 = x.Line5,
                                                                   Line6 = x.Line6,
                                                               })
                                                               .Skip((model.PageIndex == 0 && model.PageSize == 0) ? 0 : model.PageSize * model.PageIndex)
                                                               .Take(model.PageSize == 0 ? res.TotalRecords : model.PageSize)
                                                               .ToListAsync();
            res.ReportData = res.ReportData.OrderByDescending(x => x.PunchingTime).ToList();

            return res;
        }

        public async Task<PagedResponse<List<MarkingReportModel>>> PlateMarkingReportData(FilterModel model)
        {
            var res = new PagedResponse<List<MarkingReportModel>>();
            res.PageIndex = model.PageIndex;
            res.PageSize = model.PageSize;
            res.TotalRecords = await _context.PlateMarkingRecord.Include(x => x.PlateInfoFromSAP)
                                                               .Where(x => x.ModuleId == model.moduleId &&
                                                                           x.TimeStamp.Value.Date >= model.FromDate.Date &&
                                                                           x.TimeStamp.Value.Date <= model.ToDate.Date)
                                                               .CountAsync();
            res.ReportData = await _context.PlateMarkingRecord.Include(x => x.PlateInfoFromSAP)
                                                               .Where(x => x.ModuleId == model.moduleId &&
                                                                           x.TimeStamp.Value.Date >= model.FromDate.Date &&
                                                                           x.TimeStamp.Value.Date <= model.ToDate.Date)
                                                               .Select(x => new MarkingReportModel
                                                               {
                                                                   MarkingTime = x.TimeStamp.Value.ToString("dd-MM-yyyy : HH-mm-ss"),
                                                                   PlateNumber = x.PlateNumber,
                                                                   HeatNumber = x.PlateInfoFromSAP.HeatNumber,
                                                                   Size = $"{x.PlateInfoFromSAP.Length} X {x.PlateInfoFromSAP.Width} X {x.PlateInfoFromSAP.Thickness}",
                                                                   Weight = x.PlateInfoFromSAP.Weight,
                                                                   PurchaseOrder = x.PlateInfoFromSAP.PurchaseOrder.Value,
                                                                   PurchaseOrderNumber = x.PlateInfoFromSAP.PurchaseOrderNumber,
                                                                   MaterialDescription = x.PlateInfoFromSAP.MaterialDescription,
                                                                   CustomerName = x.PlateInfoFromSAP.CustomerName,
                                                                   CustomerReference = x.PlateInfoFromSAP.CustomerReference,
                                                                   Grade = x.PlateInfoFromSAP.Grade,
                                                                   GradeDuel = x.PlateInfoFromSAP.GradeDuel,
                                                                   Line1 = x.Line1,
                                                                   Line2 = x.Line2,
                                                                   Line3 = x.Line3,
                                                                   Line4 = x.Line4,
                                                                   Line5 = x.Line5,
                                                                   Line6 = x.Line6,
                                                               })
                                                                .Skip((model.PageIndex == 0 && model.PageSize == 0) ? 0 : model.PageSize * model.PageIndex)
                                                               .Take(model.PageSize == 0 ? res.TotalRecords : model.PageSize)
                                                               .OrderByDescending(x => x.MarkingTime)
                                                               .ToListAsync();
            res.ReportData = res.ReportData.OrderByDescending(x => x.MarkingTime).ToList();


            return res;
        }

        public async Task<PagedResponse<List<ReportData>>> AuditReportData(FilterModel model)
        {

            var res = new PagedResponse<List<ReportData>>();
            res.PageIndex = model.PageIndex;
            res.PageSize = model.PageSize;
            res.TotalRecords = await _context.AppLogs.Include(x => x.User)
                                                               .Where(x => (x.ModuleId == model.moduleId || x.ModuleId == null) &&
                                                                           x.LogDate.Date >= model.FromDate.Date &&
                                                                           x.LogDate.Date <= model.ToDate.Date)
                                                               .CountAsync();
            var reportData = await _context.AppLogs.Include(x => x.User)
                                                               .Where(x => (x.ModuleId == model.moduleId || x.ModuleId == null) &&
                                                                           x.LogDate.Date >= model.FromDate.Date &&
                                                                           x.LogDate.Date <= model.ToDate.Date)
                                                               .Select(x => new ReportData
                                                               {
                                                                   LogTime = x.LogDate.ToString("dd-MM-yyyy : HH-mm-ss"),
                                                                   Log = x.Log,
                                                                   LogType = x.LogType,
                                                                   User = x.LogType == "exception" ? "exception" : x.User.UserName
                                                               })
                                                               .Skip((model.PageIndex == 0 && model.PageSize == 0) ? 0 : model.PageSize * model.PageIndex)
                                                               .Take(model.PageSize == 0 ? res.TotalRecords : model.PageSize)
                                                               .ToListAsync();
            res.ReportData = reportData.OrderByDescending(x => x.LogTime).ToList();


            return res;
        }

        public async Task<PagedResponse<List<ColdLevellerReport>>> ColdLevellerReportData(FilterModel model)
        {
            var res = new PagedResponse<List<ColdLevellerReport>>();
            res.PageIndex = model.PageIndex;
            res.PageSize = model.PageSize;
            res.TotalRecords = await _context.ColdLevellerRecords.Include(x => x.PlateDataFromSapColdLeveller)
                                                         .ThenInclude(x => x.YsDataRecords)
                                                         .Where(x => x.TimeStamp.Date >= model.FromDate.Date &&
                                                                     x.TimeStamp.Date <= model.ToDate.Date)
                                                         .CountAsync();
            res.ReportData = await _context.ColdLevellerRecords.Include(x => x.PlateDataFromSapColdLeveller)
                                                         .ThenInclude(x => x.YsDataRecords)
                                                         .Where(x => x.TimeStamp.Date >= model.FromDate.Date &&
                                                                     x.TimeStamp.Date <= model.ToDate.Date)
                                                         .Select(x => new ColdLevellerReport
                                                         {
                                                             SAPFetchTime = x.PlateDataFromSapColdLeveller.CreatedDate.Value.ToString("dd-MM-yyyy : HH-mm-ss"),
                                                             PlateNo = x.PlateNumber,
                                                             Grade = x.PlateDataFromSapColdLeveller.Grade,
                                                             Length = x.PlateDataFromSapColdLeveller.Length,
                                                             Thick = x.PlateDataFromSapColdLeveller.Thickness,
                                                             Width = x.PlateDataFromSapColdLeveller.Width,
                                                             Weight = x.PlateDataFromSapColdLeveller.Weight,
                                                             YST = x.PlateDataFromSapColdLeveller.YST,
                                                             PlateNo1 = x.PlateNumber,
                                                             DBYST = x.PlateDataFromSapColdLeveller.YsDataRecords.Select(x => x.YSValue).FirstOrDefault(),
                                                             LDDate = x.TimeStamp.ToString("dd-MM-yyyy : HH-mm-ss"),
                                                             LDPlateNo = x.PlateNumber,
                                                             LDSteelGrade = x.SteelGrade,
                                                             LDLength = x.Length.ToString(),
                                                             LDThick = x.Thickness.ToString(),
                                                             LDWeight = x.Weight.ToString(),
                                                             LDWidth = x.Width.ToString()
                                                         })
                                                          .Skip((model.PageIndex == 0 && model.PageSize == 0) ? 0 : model.PageSize * model.PageIndex)
                                                               .Take(model.PageSize == 0 ? res.TotalRecords : model.PageSize)
                                                         .ToListAsync();
            res.ReportData = res.ReportData.OrderByDescending(x => x.SAPFetchTime).ToList();

            return res;
        }

        public async Task<PagedResponse<List<DownCoilerReport>>> DownCoilerReportData(FilterModel model)
        {
            var res = new PagedResponse<List<DownCoilerReport>>();
            res.PageIndex = model.PageIndex;
            res.PageSize = model.PageSize;
            res.TotalRecords = await _context.DownCoilerReportsData.Include(x => x.PlateInfoFromSAP)
                                                           .Where(x => x.TimeStamp.Date >= model.FromDate.Date &&
                                                                       x.TimeStamp.Date <= model.ToDate.Date)
                                                           .CountAsync();
            res.ReportData = await _context.DownCoilerReportsData.Include(x => x.PlateInfoFromSAP)
                                                           .Where(x => x.TimeStamp.Date >= model.FromDate.Date &&
                                                                       x.TimeStamp.Date <= model.ToDate.Date)
                                                           .Select(x => new DownCoilerReport
                                                           {
                                                               TimeStamp = x.TimeStamp.ToString("dd-MM-yyyy : HH-mm-ss"),
                                                               MatId = x.MatId,
                                                               CoilId = x.PlateInfoFromSAP.PlateNumber,
                                                               HeatNo = x.PlateInfoFromSAP.HeatNumber,
                                                               Grade = x.PlateInfoFromSAP.Grade,
                                                               Width = x.PlateInfoFromSAP.Width,
                                                               Thickness = x.PlateInfoFromSAP.Thickness,
                                                               Cust_name = x.PlateInfoFromSAP.CustomerName,
                                                               p_order = x.PlateInfoFromSAP.PurchaseOrder.ToString(),
                                                               P_Number = x.PlateInfoFromSAP.PurchaseOrderNumber,
                                                               AOT_Weight = x.PlateInfoFromSAP.Weight,
                                                               RecordId = x.Id.ToString(),
                                                               DataLoadDate = x.TimeStamp.ToString("dd-MM-yyyy : HH-mm-ss"),
                                                               DiscLine1 = x.DiscLine1,
                                                               DiscLine2 = x.DiscLine2,
                                                               ShellLine1 = x.ShellLine1,
                                                               ShellLine2 = x.ShellLine2,
                                                               ShellLine3 = x.ShellLine3,
                                                               ShellLine4 = x.ShellLine4,
                                                               CoilWidth = x.CoilWidth,
                                                               CoilDiameter = x.CoilDiameter
                                                           })
                                                           .Skip((model.PageIndex == 0 && model.PageSize == 0) ? 0 : model.PageSize * model.PageIndex)
                                                           .Take(model.PageSize)
                                                           .ToListAsync();
            res.ReportData = res.ReportData.OrderByDescending(x => x.TimeStamp).ToList();
            return res;
        }


        public async Task<PagedResponse<List<WeightUpadteReport>>> WeightUpdateReportData(FilterModel model)
        {
            var res = new PagedResponse<List<WeightUpadteReport>>();
            res.PageIndex = model.PageIndex;
            res.PageSize = model.PageSize;
            res.TotalRecords = _context.CoilWeightUpdateData
                                     .Where(x => x.CreatedDate.Date >= model.FromDate.Date &&
                                                 x.CreatedDate.Date <= model.ToDate.Date).Count();
            res.ReportData = await _context.CoilWeightUpdateData
                                     .Where(x => x.CreatedDate.Date >= model.FromDate.Date &&
                                                 x.CreatedDate.Date <= model.ToDate.Date)
                                     .Select(x => new WeightUpadteReport
                                     {
                                         SlNo = x.Id,
                                         CoilId = x.CoilId,
                                         Weight = x.Weight,
                                         CreatedDate = x.CreatedDate.ToString("dd-MM-yyyy : HH-mm-ss")
                                     })
                                     .Skip((model.PageIndex == 0 && model.PageSize == 0) ? 0 : model.PageSize * model.PageIndex)
                                                           .Take(model.PageSize)
                                     .ToListAsync();
            res.ReportData = res.ReportData.OrderByDescending(x => x.CreatedDate).ToList();
            return res;
        }
    }
}
