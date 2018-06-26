using System.Collections.Generic;
using AdoModel;

public interface IServiceDao
{
    ICollection<Devices> GetAllDevicesDao();
    int SaveMetricsDao(Metrics metrics);

    Devices GetDeviceByAdressMac(string adressMac);

    ICollection<DataMetrics> GetMetricByDeviceTypeDao(int value);
}