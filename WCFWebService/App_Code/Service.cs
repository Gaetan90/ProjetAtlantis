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
            deviceView.nom = device.name;
            deviceView.adressMac = device.adressMac;
            deviceView.EmployeeToEmployeeView(device.DeviceEmployees);
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

    public int SaveMetrics(string adressMac, string value)
    {
        var json = new JavaScriptSerializer().Deserialize<MetricsJson>(value);
        Metrics metrics = new Metrics();
        metrics.date = json.metricDate;
        int nbrValue = 0;
        ICollection<DataMetrics> dataMetrics = new Collection<DataMetrics>();
        //foreach (Object o in json.metricValue){
        DataMetrics data = new DataMetrics();
        data.value = json.metricValue;
        dataMetrics.Add(data);
        nbrValue++;
        //}
        Devices devices = serviceDao.GetDeviceByAdressMac(adressMac);
        metrics.nbrValues = nbrValue;
        metrics.DataMetrics = dataMetrics;
        metrics.Devices = devices;
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

    public void SetCommandDevice(string idDevice, string value)
    {
        throw new NotImplementedException();
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
            deviceView.nom = device.name;
            deviceView.adressMac = device.adressMac;
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

    public ICollection<MetricView> GetListMetricSimple()
    {
        throw new NotImplementedException();
    }

    public void SetDeviceEmployee(string idDevice, string value)
    {
        throw new NotImplementedException();
    }

    public void ReceptMetric(MetricView metric)
    {
        throw new NotImplementedException();
    }

  
}
