using CSI_PPMS.Entity;
using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using CSI_PPMS_Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CSI_PPMS.Services
{
    public class ConfigureServices : IConfigureServices
    {
        private readonly PPMSContext _context;

        public ConfigureServices(PPMSContext context)
        {
            _context = context;
        }


        public async Task<SAPLinkModel> GetSapLink(long moduleId)
        {
            var sapLink = new SAPLinkModel();
            var ips = await _context.TCPConfig.Where(x => x.ModuleId == moduleId).FirstOrDefaultAsync();
            if (moduleId != 5)
            {
                sapLink = await _context.SapCredential.Where(x => x.ModuleId == moduleId).Select(x => new SAPLinkModel
                {
                    SAPLink = x.SAPLink,
                    SapLinkId = x.Id,
                    SapPassword = x.SAPPassword,
                    SapUserName = x.SAPUserName
                }).FirstOrDefaultAsync();
            }
            else
            {
                sapLink = await _context.SapCredential.Where(x => x.ModuleId == moduleId && x.Type == 1).Select(x => new SAPLinkModel
                {
                    SAPLink = x.SAPLink,
                    SapLinkId = x.Id,
                    SapPassword = x.SAPPassword,
                    SapUserName = x.SAPUserName,
                    SapWeightId = _context.SapCredential.Where(x => x.ModuleId == moduleId && x.Type == 2).Select(x => x.Id).FirstOrDefault(),
                    SapWeightLink = _context.SapCredential.Where(x => x.ModuleId == moduleId && x.Type == 2).Select(x => x.SAPLink).FirstOrDefault()
                }).FirstOrDefaultAsync();
            }
            if (ips != null)
            {
                sapLink.TechniforIP = ips.TechniforIPAddress;
                sapLink.TechniforPort = ips.TechniforPortNo.ToString();
                sapLink.TechniforRack = ips.TechniforRack.ToString();
                sapLink.Techniforslot = ips.TechniforSlot.ToString();
                sapLink.PLCIP = ips.PLCIPAddress;
                sapLink.PLCPort = ips.PLCPortNo.ToString();
                sapLink.PLCSlot = ips.PLCSlot.ToString();
                sapLink.PLCRack = ips.PLCRack.ToString();
                sapLink.TCPId = ips.Id;
            }
            return sapLink;
        }

        public async Task<APIResponse> UpdateSapLink(UpdateSapLinkModel model)
        {
            var sapLink = await _context.SapCredential.Where(x => x.Id == model.SapLinkId).FirstOrDefaultAsync();
            sapLink.SAPLink = model.SAPLink;
            sapLink.SAPUserName = model.SAPUserName;
            sapLink.SAPPassword = model.SAPPassword;
            _context.Update(sapLink);
            await _context.SaveChangesAsync();
            return new APIResponse("updated successfully", 200);
        }

        public async Task<APIResponse> UpdateSapCredentials(UpdateSapCredentialsModel model)
        {
            var sapLink = await _context.SapCredential.Where(x => x.Id == model.SapLinkId).FirstOrDefaultAsync();
            sapLink.SAPUserName = model.SAPUserName;
            sapLink.SAPPassword = model.SAPPassword;
            _context.Update(sapLink);
            await _context.SaveChangesAsync();
            return new APIResponse("success", 200);
        }


        public async Task UpdatePLCDetails(UpdatePLCModel model)
        {
            var data = await _context.TCPConfig.Where(x => x.ModuleId == model.ModuleId && x.Id == model.tid).FirstOrDefaultAsync();
            if (data != null)
            {
                if (model.ModuleId == 1 || model.ModuleId == 3 || model.ModuleId == 5)
                {
                    if (!string.IsNullOrEmpty(model.TIP) && model.TPort != 0)
                    {
                        data.TechniforIPAddress = model.TIP;
                        data.TechniforPortNo = (int)model.TPort;
                    }
                    if (!string.IsNullOrEmpty(model.PIP) && model.PPort != 0)
                    {
                        data.PLCIPAddress = model.PIP;
                        data.PLCPortNo = (int)model.PPort;
                    }
                }
                else if (model.ModuleId == 2)
                {
                    data.TechniforIPAddress = model.TIP;
                    data.TechniforRack = model.TRack;
                    data.TechniforSlot = model.TSlot;
                }
                _context.Update(data);
                _context.SaveChanges();
            }
        }
    }
}
