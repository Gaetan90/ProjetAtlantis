using AdoModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Description résumée de Device
/// </summary>
/// 
[DataContract]
public class DeviceView
{
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public string addressMac { get; set; }
    [DataMember]
    public ICollection<EmployeeView> employees { get; set; }
    [DataMember]
    public string nameDeviceType { get; set; }
    [DataMember]
    public TypeDeviceView typeDevices { get; set; }
    [DataMember]
    public bool disabled { get; set; }


    public void EmployeeToEmployeeView(ICollection<AdoModel.DeviceEmployee> deviceEmployees)
    {
        ICollection<EmployeeView> employees = new Collection<EmployeeView>();
        foreach (DeviceEmployee employee in deviceEmployees)
        {
            EmployeeView employeeView = new EmployeeView();
            employeeView.id = employee.Employee.id;
            employeeView.name = employee.Employee.name;
            employeeView.lastName = employee.Employee.lastname;
            employeeView.email = employee.Employee.email;
            employeeView.password = employee.Employee.password;
            employeeView.isAdmin = employee.Employee.isAdmin.Value;
            employees.Add(employeeView);
        }
        this.employees = employees;
    }

   
}