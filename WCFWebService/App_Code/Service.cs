using AdoModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service" dans le code, le fichier svc et le fichier de configuration.
public class Service : IServiceDevice, IServiceCalcul
{
    IServiceDao serviceDao;

    public Service()
    {
        this.serviceDao = new ServiceDao();
    }

    //*******************************//
    //*********DEVICEINTERFACE********//
    //*******************************//
    public ICollection<DeviceView> GetAllDevice()
    {
        ICollection<DeviceView> devices = new Collection<DeviceView>();
        ICollection<Devices> listDevices = serviceDao.GetAllDevicesDao();
        foreach (Devices device in listDevices)
        {
            DeviceView deviceView = new DeviceView();
            deviceView.id = device.id;
            deviceView.name = device.name;
            deviceView.addressMac = device.adressMac;
            //deviceView.EmployeeToEmployeeView(device.DeviceEmployees);
            TypeDevices typeDevice = device.TypeDevices;
            if (typeDevice != null)
            {
                TypeDeviceView typeDeviceView = new TypeDeviceView();
                typeDeviceView.id = typeDevice.id;
                typeDeviceView.name = typeDevice.name;
                deviceView.typeDevices = typeDeviceView;
            }
            
            devices.Add(deviceView);
        }

        return devices;
    }

    public int SaveMetrics(string adressMac, string metricDate, string deviceType, string metricValue)
    {
        DateTime datetime = Convert.ToDateTime(metricDate);
        int nbrValues = 0;
        Metrics metrics = new Metrics();
        string[] values = metricValue.Split(';');
        metrics.date = datetime;
        ICollection<DataMetrics> dataMetrics = new Collection<DataMetrics>();
        foreach (string value in values)
        {
            DataMetrics data = new DataMetrics();
            data.value = value;
            dataMetrics.Add(data);
            nbrValues++;
        }
        Devices devices = serviceDao.GetDeviceByAdressMac(adressMac);
        metrics.nbrValues = nbrValues;
        metrics.Devices = devices;
        metrics.DataMetrics = dataMetrics;
        return serviceDao.SaveMetricsDao(metrics);
    }

    public ICollection<DataMetricView> GetMetricByDeviceType(string value)
    {
        ICollection<DataMetrics> dataMetrics = serviceDao.GetMetricByDeviceTypeDao(Int32.Parse(value));
        ICollection<DataMetricView> dataMetricsView = new Collection<DataMetricView>();
        foreach(DataMetrics dataMetric in dataMetrics)
        {
            DataMetricView dataMetricView = new DataMetricView();
            dataMetricView.id = dataMetric.id;
            dataMetricView.value = dataMetric.value;
            dataMetricView.metric = new MetricView().getMetricsToMetricsView(dataMetric.Metrics);
            dataMetricsView.Add(dataMetricView);
        }

        return dataMetricsView;

    }

    public void SetCommandDevice(string idDevice, string commandeName)
    {
        Devices device = serviceDao.GetDeviceById(Int32.Parse(idDevice));
        HistoriqueCommandes commandeDevice = new HistoriqueCommandes();
        commandeDevice.Devices = device;
        commandeDevice.commandeName = commandeName;
        commandeDevice.dateTime = DateTime.Now;
        serviceDao.SaveCommandeDevice(commandeDevice);
    }

    public void CreateNewDevice(string id, string name, string deviceType)
    {
        TypeDevices typeDevice = serviceDao.GetTypeDeviceByName(deviceType);
        Devices device = new Devices();
        device.adressMac = id;
        device.name = name;
        device.TypeDevices = typeDevice;
        serviceDao.SaveNewDevice(device);
    }

    //*******************************//
    //*********CALULINTERFACE********//
    //*******************************//

    public ICollection<DeviceView> GetListDevicesByEmployee()
    {
        ICollection<DeviceView> devices = new Collection<DeviceView>();
        ICollection<Devices> listDevices = serviceDao.GetAllDevicesDao();
        foreach (Devices device in listDevices)
        {
            DeviceView deviceView = new DeviceView();
            deviceView.id = device.id;
            deviceView.name = device.name;
            deviceView.addressMac = device.adressMac;
            deviceView.nameDeviceType = device.TypeDevices.name;
            deviceView.EmployeeToEmployeeView(device.DeviceEmployees);
            devices.Add(deviceView);
        }

        return devices;
    }

    public ICollection<MetricView> GetListMetricSimple()
    {
        throw new NotImplementedException();
    }

    public void SetDeviceEmployee(string idDevice, string idEmployee)
    {
        throw new NotImplementedException();
    }


    public ICollection<EmployeeView> GetListEmployees()
    {
        ICollection<Employees> employees = serviceDao.GetListEmployees();
        ICollection<EmployeeView> listEmployeeView = new Collection<EmployeeView>();
        foreach(Employees employee in employees)
        {
            EmployeeView employeeView = new EmployeeView();
            employeeView.id = employee.id;
            employeeView.name = employee.name;
            employeeView.lastName = employee.lastname;
            employeeView.DeviceToDeviceView(employee.DeviceEmployees);
            listEmployeeView.Add(employeeView);
        }
        return listEmployeeView;
    }

    public void SetEmployee(string id,EmployeeView employee)
    {
        serviceDao.updateEmployee(employee);
    }

    public void SetCommandeDevice(string id_device, string action)
    {
        HistoriqueCommandes historiqueCommande = new HistoriqueCommandes();
        historiqueCommande.idDevice = Int32.Parse(id_device);
        historiqueCommande.commandeName = action;
        historiqueCommande.dateTime = DateTime.Now;
        serviceDao.SaveCommandeDevice(historiqueCommande);
    }
}
