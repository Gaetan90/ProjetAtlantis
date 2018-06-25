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
    public string nom { get; set; }
    [DataMember]
    public string adressMac { get; set; }
    [DataMember]
    public ICollection<EmployeeView> employees { get; set; }
    [DataMember]
    public TypeDeviceView typeDevices { get; set; }

    public void EmployeeToEmployeeView(ICollection<AdoModel.DeviceEmployees> deviceEmployees)
    {
        ICollection<EmployeeView> employees = new Collection<EmployeeView>();
        foreach (DeviceEmployees employee in deviceEmployees)
        {
            EmployeeView employeeView = new EmployeeView();
            employeeView.id = employee.Employees.id;
            employeeView.nom = employee.Employees.name;
            employeeView.prenom = employee.Employees.lastname;
            employees.Add(employeeView);
        }
        this.employees = employees;
    }
}