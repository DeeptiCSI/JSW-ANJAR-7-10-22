using CSI_PPMS.Models;
using CSI_PPMS_Web.Models;
using System.Threading.Tasks;

namespace CSI_PPMS.IServices
{
    public interface IConfigureServices
    {
        Task<SAPLinkModel> GetSapLink(long moduleId);

        Task<APIResponse> UpdateSapLink(UpdateSapLinkModel model);

        Task<APIResponse> UpdateSapCredentials(UpdateSapCredentialsModel model);

        Task UpdatePLCDetails(UpdatePLCModel model);
    }
}
