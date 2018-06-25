using AdoModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
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
}
