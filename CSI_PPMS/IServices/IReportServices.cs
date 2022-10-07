using CSI_PPMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSI_PPMS.IServices
{
    public interface IReportServices
    {
        Task<PagedResponse<List<PunchingReportModel>>> PlatePunchingReportData(FilterModel model);

        Task<PagedResponse<List<MarkingReportModel>>> PlateMarkingReportData(FilterModel model);

        Task<PagedResponse<List<ColdLevellerReport>>> ColdLevellerReportData(FilterModel model);

        Task<PagedResponse<List<DownCoilerReport>>> DownCoilerReportData(FilterModel model);


        Task<PagedResponse<List<ReportData>>> AuditReportData(FilterModel model);

        Task<PagedResponse<List<WeightUpadteReport>>> WeightUpdateReportData(FilterModel model);


    }
}
