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
using System.Globalization;
using System.Net;
using Newtonsoft.Json;

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
        ICollection<Devices> listDevices = serviceDao.GetAllDevicesEnabled();
        foreach (Devices device in listDevices)
        {
            DeviceView deviceView = new DeviceView();
            deviceView.id = device.id;
            deviceView.name = device.name;
            deviceView.addressMac = device.adressMac;
            deviceView.disabled = device.disabled.Value;
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
            deviceView.disabled = device.disabled.Value;
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
        Devices device = serviceDao.GetDeviceById(Int32.Parse(id_device));
        switch (action)
        {
            case "activate":
                device.disabled = false;
                break;
            case "disabled":
                device.disabled = true;
                break;
        }
        serviceDao.UpdateDevice(device);
        HistoriqueCommandes historiqueCommande = new HistoriqueCommandes();
        historiqueCommande.idDevice = Int32.Parse(id_device);
        historiqueCommande.commandeName = action;
        historiqueCommande.dateTime = DateTime.Now;
        serviceDao.SaveCommandeDevice(historiqueCommande);
    }

    public ICollection<DataMetricView> GetListMetrics(string sensorType, string dateType)
    {
        ICollection<DataMetrics> dataMetrics = null;
        ICollection<DataMetricView> dataMetricsViews = new Collection<DataMetricView>();
        TypeDevices deviceType = serviceDao.GetTypeDeviceByName(sensorType);
        DateTime now = DateTime.Now;
        DateTime  date1 = new DateTime();
        DateTime date2 = new DateTime();
        switch (dateType)
        {
            case "day":
                date1 = DateTime.Parse(now.ToString("d"));
                date2 = date1.AddDays(1);
                break;
            case "week":
                int delta = DayOfWeek.Monday - now.DayOfWeek;
                date1 = DateTime.Parse(now.AddDays(delta).ToString("d"));
                delta = now.DayOfWeek - DayOfWeek.Sunday;
                date2 = DateTime.Parse(now.AddDays(delta).ToString("d"));
                break;
            case "month":
                int month = now.Month;
                int year = now.Year;
                date1 = DateTime.Parse("01/" + month + "/" + year);
                date2 = DateTime.Parse(DateTime.DaysInMonth(year,month ) +"/" + month + "/" + year);
                break;
        }
        dataMetrics = serviceDao.GetDataMetricsBehindDatesByType(deviceType, date1, date2);
        foreach(DataMetrics dataMetric in dataMetrics)
        {
            DataMetricView dataMetricView = new DataMetricView();
            dataMetricView.value = dataMetric.value;
            dataMetricsViews.Add(dataMetricView);
        }
        return dataMetricsViews;
    }

    public DeviceView GetDeviceById(string id)
    {
        DeviceView deviceView = new DeviceView();
        Devices device = serviceDao.GetDeviceById(Int32.Parse(id));
        deviceView.name = device.name;
        deviceView.disabled = device.disabled.Value;
        deviceView.nameDeviceType = device.TypeDevices.name;
        deviceView.id = device.id;
        deviceView.addressMac = device.adressMac;
        deviceView.EmployeeToEmployeeView(device.DeviceEmployees);
        return deviceView;
    }

    public void ReceptCalculatedMetrics(string sensorType, string dateType,string value)
    {
        DataMetricsJson dataMetricsJson = new DataMetricsJson();
        dataMetricsJson.deviceType = sensorType;
        dataMetricsJson.value = Double.Parse(value);
        dataMetricsJson.dateType = dateType.ToUpper();
        var json = new JavaScriptSerializer().Serialize(dataMetricsJson);
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.167.128.145:8080/transaction/data-calculated");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
        }
        
    }
}
