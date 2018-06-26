using AdoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de ServiceDao
/// </summary>
public class ServiceDao : IServiceDao
{
    private AtlantisWindowsEntities _dbo;
    public ServiceDao()
    {
        _dbo = new AtlantisWindowsEntities();
    }

    public int SaveMetricsDao(Metrics metrics)
    {
        _dbo.Metrics.Add(metrics);
        int result = _dbo.SaveChanges();
        return result;
    }
    public ICollection<Devices> GetAllDevicesDao()
    {
        return _dbo.Devices.ToList();
    }

    public ICollection<DataMetrics> GetMetricByDeviceTypeDao(int value)
    {
        return _dbo.DataMetrics.Where(o => o.Metrics.Devices.idTypeDevice == value).ToList(); 
    }

    public Devices GetDeviceByAdressMac(string adressMac)
    {
        return _dbo.Devices.Where(o => o.adressMac == adressMac).First();
    }
}