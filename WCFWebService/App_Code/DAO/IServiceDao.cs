using System;
using System.Collections.Generic;
using AdoModel;

public interface IServiceDao
{
    ICollection<Device> GetAllDevicesDao();
    int SaveMetricsDao(Metric metrics);
    Device GetDeviceByAdressMac(string adressMac);
    Device GetDeviceById(int id);
    ICollection<DataMetric> GetMetricByDeviceTypeDao(int value);
    HistoriqueCommande GetCommandeByName(string commande);
    void SaveCommandeDevice(HistoriqueCommande commandeDevice);
    ICollection<Employee> GetListEmployees();
    TypeDevice GetTypeDeviceByName(string deviceType);
    void SaveNewDevice(Device device);
    void updateEmployee(EmployeeView employee);
    ICollection<DataMetric> GetDataMetricsBehindDatesByType(TypeDevice deviceType, DateTime date1, DateTime date2);
    ICollection<Device> GetAllDevicesEnabled();
    void UpdateDevice(Device device);
    int GetEmployeeConnection(string email, string password);
    ICollection<Metric> GetMetricsByDevices(string idDevice);
}