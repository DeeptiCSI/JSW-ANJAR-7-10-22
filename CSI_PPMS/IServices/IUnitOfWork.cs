namespace CSI_PPMS.IServices
{
    public interface IUnitOfWork
    {
        IAccountServices AccountServices { get; }

        IPlateServices PlateServices { get; }

        ISAPServices SAPServices { get; }

        IReportServices ReportServices { get; }

        IConfigureServices ConfigureServices { get; }

        IExcellServices ExcellServices { get; }
    }
}
