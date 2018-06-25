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
    public ICollection<DeviceView> GetAllDevice()
    {
        ICollection<DeviceView> devices = new Collection<DeviceView>();

        AtlantisWindowsEntities _dbo = new AtlantisWindowsEntities();
        ICollection<Devices> listDevices = _dbo.Devices.ToList();
        foreach(Devices device in listDevices)
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

    public string GetCalcul(int value)
    {
        throw new NotImplementedException();
    }

    public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}

    public void ReceptMetric(MetricView metric)
    {
        throw new NotImplementedException();
    }

    public void saveMetrics(string idDevice, string value)
    {
        var json = new JavaScriptSerializer().Deserialize<MetricsJson>(value);
        Metrics metrics = new Metrics();
        metrics.idDevice = Int32.Parse(idDevice);
        metrics.date = json.metricDate;
        int nbrValue = 0;
        ICollection<DataMetrics> dataMetrics = new Collection<DataMetrics>();
        foreach (Object o in json.metricValue){
            DataMetrics data = new DataMetrics();
            data.value = o.ToString();
            dataMetrics.Add(data);
            nbrValue++;
        }
        metrics.nbrValues = nbrValue;
        metrics.DataMetrics = dataMetrics;

        AtlantisWindowsEntities _dbo = new AtlantisWindowsEntities();
        _dbo.Metrics.Add(metrics);
        _dbo.SaveChanges();
    }
}
