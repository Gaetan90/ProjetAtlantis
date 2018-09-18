using AdoModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Description résumée de Employee
/// </summary>
/// 
[DataContract]
public class EmployeeView
{
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public string lastName { get; set; }
    [DataMember]
    public string email { get; set; }
    [DataMember]
    public string password { get; set; }
    [DataMember]
    public bool isAdmin { get; set; }
    [DataMember]
    public ICollection<DeviceView> devices { get; set; }

    public void DeviceToDeviceView(ICollection<DeviceEmployee> deviceEmployees)
    {
        ICollection<DeviceView> listdeviceView = new Collection<DeviceView>();
        foreach (DeviceEmployee device in deviceEmployees)
        {
            DeviceView deviceView = new DeviceView();
            deviceView.id = device.Device.id;
            deviceView.name = device.Device.name;
            deviceView.addressMac = device.Device.adressMac;
            deviceView.disabled = device.Device.disabled.Value;
            deviceView.nameDeviceType = device.Device.TypeDevice.name;
            listdeviceView.Add(deviceView);
        }
        this.devices = listdeviceView;
    }
}