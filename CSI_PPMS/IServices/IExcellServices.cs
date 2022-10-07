using CSI_PPMS.Models;
using System.Threading.Tasks;

namespace CSI_PPMS.IServices
{
    public interface IExcellServices
    {
        Task<byte[]> GenerateMarkingReport(FilterModel model);

        Task<byte[]> GeneratePunchingingReport(FilterModel model);

        Task<byte[]> GenerateColdLevellerReport(FilterModel model);

        Task<byte[]> GenerateDowncoilerReport(FilterModel model);

        Task<byte[]> GenerateAuditReport(FilterModel model);

        Task<byte[]> GenerateWeightDataUpdate(FilterModel model);
    }
}
