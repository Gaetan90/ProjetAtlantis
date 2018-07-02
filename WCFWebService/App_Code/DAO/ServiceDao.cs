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

    public AtlantisWindowsEntities AtlantisWindowsEntities
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
        }
    }

    public int SaveMetricsDao(Metric metrics)
    {
        _dbo.Metrics.Add(metrics);
        int result = _dbo.SaveChanges();
        return result;
    }
    public ICollection<Device> GetAllDevicesDao()
    {
        return _dbo.Devices.ToList();
    }

    public ICollection<Device> GetAllDevicesEnabled()
    {
        return _dbo.Devices.Where(o => o.disabled == false).ToList();
    }

    public ICollection<DataMetric> GetMetricByDeviceTypeDao(int value)
    {
        return _dbo.DataMetrics.Where(o => o.Metric.Device.idTypeDevice == value).ToList(); 
    }

    public Device GetDeviceByAdressMac(string adressMac)
    {
        return _dbo.Devices.Where(o => o.adressMac == adressMac && o.disabled == false).First();
    }

    public HistoriqueCommande GetCommandeByName(string commande)
    {
        return _dbo.HistoriqueCommandes.Where(o => o.commandeName == commande).First();
    }

    public Device GetDeviceById(int id)
    {
        return _dbo.Devices.Find(id);
    }

    public void SaveCommandeDevice(HistoriqueCommande commandeDevice)
    {
        _dbo.HistoriqueCommandes.Add(commandeDevice);
        _dbo.SaveChanges();
    }

    public ICollection<Employee> GetListEmployees()
    {
        return _dbo.Employees.ToList();
    }

    public TypeDevice GetTypeDeviceByName(string deviceType)
    {
        return _dbo.TypeDevices.Where(o => o.name == deviceType).First();
    }

    public void SaveNewDevice(Device device)
    {
        _dbo.Devices.Add(device);
        _dbo.SaveChanges();
    }

    public void updateEmployee(EmployeeView employee)
    {
        Employee entity = _dbo.Employees.Find(employee.id);
        ICollection<DeviceEmployee> deviceEmployees = _dbo.DeviceEmployees.Where(o => o.idEmployee.Value == employee.id).ToList();
        foreach(DeviceEmployee deviceEmployee in deviceEmployees)
        {
            _dbo.DeviceEmployees.Remove(deviceEmployee);
        }
        Employee newEmployee = _dbo.Employees.Find(employee.id);
        newEmployee.name = employee.name;
        newEmployee.lastname = employee.lastName;
        deviceEmployees = new Collection<DeviceEmployee>();
        foreach (DeviceView deviceView in employee.devices)
        {
            DeviceEmployee deviceEmployee = new DeviceEmployee();
            deviceEmployee.idDevice = deviceView.id;
            deviceEmployee.idEmployee = entity.id;
            deviceEmployees.Add(deviceEmployee);
        }
        newEmployee.DeviceEmployees = deviceEmployees;
        _dbo.Entry(entity).CurrentValues.SetValues(newEmployee);
        _dbo.SaveChanges();
        
    }

    public ICollection<DataMetric> GetDataMetricsBehindDatesByType(TypeDevice deviceType, DateTime date1, DateTime date2)
    {
        return _dbo.DataMetrics.Where(o => o.Metric.date >= date1 && o.Metric.date < date2 && o.Metric.Device.idTypeDevice == deviceType.id).ToList();
    }

    public void UpdateDevice(Device device)
    {
        Device entity = this.GetDeviceById(device.id);
        _dbo.Entry(entity).CurrentValues.SetValues(device);
        _dbo.SaveChanges();
    }

    public int GetEmployeeConnection(string email, string password)
    {
        return _dbo.Employees.Where(o => (o.email == email) && (o.password == password) && (o.isAdmin == true)).Count();
    }
}