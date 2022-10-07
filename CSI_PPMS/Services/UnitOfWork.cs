using CSI_PPMS.ColdLevellerDb;
using CSI_PPMS.DownCoilerDb;
using CSI_PPMS.Entity;
using CSI_PPMS.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CSI_PPMS.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PPMSContext _context;
        private readonly IConfiguration _configuration;
        private readonly ITCPServices _tCPServices;
        private readonly ColdLevellerContext _coldLevellerContext;
        private readonly IWebHostEnvironment _environment;
        private readonly DownCoilerContext _downCoilerDb;
        private readonly ISAPServices _sapServices;

        public UnitOfWork(PPMSContext context,
                          IConfiguration configuration,
                          ITCPServices tCPServices,
                          ColdLevellerContext coldLevellerContext,
                          IWebHostEnvironment environment,
                          DownCoilerContext downCoilerDb,
                          ISAPServices sapServices)
        {
            _context = context;
            _configuration = configuration;
            _tCPServices = tCPServices;
            _coldLevellerContext = coldLevellerContext;
            _environment = environment;
            _downCoilerDb = downCoilerDb;
            _sapServices = sapServices;
        }

        public IAccountServices AccountServices =>
            new AccountServices(_context);

        public IPlateServices PlateServices =>
            new PlateServices(_tCPServices, _context, _coldLevellerContext, _downCoilerDb, _sapServices);

        public ISAPServices SAPServices =>
            new SAPServices(_configuration, _context, _coldLevellerContext);

        public IReportServices ReportServices =>
            new ReportServices(_context);

        public IConfigureServices ConfigureServices =>
            new ConfigureServices(_context);

        public IExcellServices ExcellServices =>
            new ExcellServices(_environment, ReportServices);
    }
}
