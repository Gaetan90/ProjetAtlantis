using AdoModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public ICollection<Devices> GetAllDevicesEnabled()
    {
        return _dbo.Devices.Where(o => o.disabled == false).ToList();
    }

    public ICollection<DataMetrics> GetMetricByDeviceTypeDao(int value)
    {
        return _dbo.DataMetrics.Where(o => o.Metrics.Devices.idTypeDevice == value).ToList(); 
    }

    public Devices GetDeviceByAdressMac(string adressMac)
    {
        return _dbo.Devices.Where(o => o.adressMac == adressMac && o.disabled == false).First();
    }

    public HistoriqueCommandes GetCommandeByName(string commande)
    {
        return _dbo.HistoriqueCommandes.Where(o => o.commandeName == commande).First();
    }

    public Devices GetDeviceById(int id)
    {
        return _dbo.Devices.Find(id);
    }

    public void SaveCommandeDevice(HistoriqueCommandes commandeDevice)
    {
        _dbo.HistoriqueCommandes.Add(commandeDevice);
        _dbo.SaveChanges();
    }

    public ICollection<Employees> GetListEmployees()
    {
        return _dbo.Employees.ToList();
    }

    public TypeDevices GetTypeDeviceByName(string deviceType)
    {
        return _dbo.TypeDevices.Where(o => o.name == deviceType).First();
    }

    public void SaveNewDevice(Devices device)
    {
        _dbo.Devices.Add(device);
        _dbo.SaveChanges();
    }

    public void updateEmployee(EmployeeView employee)
    {
        Employees entity = _dbo.Employees.Find(employee.id);
        ICollection<DeviceEmployees> deviceEmployees = _dbo.DeviceEmployees.Where(o => o.idEmployee.Value == employee.id).ToList();
        foreach(DeviceEmployees deviceEmployee in deviceEmployees)
        {
            _dbo.DeviceEmployees.Remove(deviceEmployee);
        }
        Employees newEmployee = _dbo.Employees.Find(employee.id);
        newEmployee.name = employee.name;
        newEmployee.lastname = employee.lastName;
        deviceEmployees = new Collection<DeviceEmployees>();
        foreach (DeviceView deviceView in employee.devices)
        {
            DeviceEmployees deviceEmployee = new DeviceEmployees();
            deviceEmployee.idDevice = deviceView.id;
            deviceEmployee.idEmployee = entity.id;
            deviceEmployees.Add(deviceEmployee);
        }
        newEmployee.DeviceEmployees = deviceEmployees;
        _dbo.Entry(entity).CurrentValues.SetValues(newEmployee);
        _dbo.SaveChanges();
        
    }

    public ICollection<DataMetrics> GetDataMetricsBehindDatesByType(TypeDevices deviceType, DateTime date1, DateTime date2)
    {
        return _dbo.DataMetrics.Where(o => o.Metrics.date >= date1 && o.Metrics.date < date2 && o.Metrics.Devices.idTypeDevice == deviceType.id).ToList();
    }

   
}