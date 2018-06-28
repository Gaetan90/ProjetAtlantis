using AdoModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de Employee
/// </summary>
public class EmployeeView
{
    public int id { get; set; }
    public string name { get; set; }
    public string lastName { get; set; }

    public ICollection<DeviceView> devices { get; set; }


    public void DeviceToDeviceView(ICollection<DeviceEmployees> deviceEmployees)
    {
        ICollection<DeviceView> listdeviceView = new Collection<DeviceView>();
        foreach (DeviceEmployees device in deviceEmployees)
        {
            DeviceView deviceView = new DeviceView();
            deviceView.id = device.Devices.id;
            deviceView.name = device.Devices.name;
            deviceView.addressMac = device.Devices.adressMac;
            deviceView.nameDeviceType = device.Devices.TypeDevices.name;
            listdeviceView.Add(deviceView);
        }
        this.devices = listdeviceView;
    }
}