using System;
using System.Collections.Generic;
using AdoModel;

public interface IServiceDao
{
    ICollection<Devices> GetAllDevicesDao();
    int SaveMetricsDao(Metrics metrics);
    Devices GetDeviceByAdressMac(string adressMac);
    Devices GetDeviceById(int id);
    ICollection<DataMetrics> GetMetricByDeviceTypeDao(int value);
    HistoriqueCommandes GetCommandeByName(string commande);
    void SaveCommandeDevice(HistoriqueCommandes commandeDevice);
    ICollection<Employees> GetListEmployees();
    TypeDevices GetTypeDeviceByName(string deviceType);
    void SaveNewDevice(Devices device);
    void updateEmployee(EmployeeView employee);
    ICollection<DataMetrics> GetDataMetricsBehindDatesByType(TypeDevices deviceType, DateTime monday, DateTime sunday);
    ICollection<Devices> GetAllDevicesEnabled();
    void UpdateDevice(Devices device);
}