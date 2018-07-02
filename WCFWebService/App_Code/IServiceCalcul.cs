using AdoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService" à la fois dans le code et le fichier de configuration.
[ServiceContract]
public interface IServiceCalcul
{

	[OperationContract]
    ICollection<DataMetricView> GetMetricByDeviceType(string idTypeDevice);

    [OperationContract]
    [WebGet(UriTemplate = "devices"  )]
    ICollection<DeviceView> GetListDevicesByEmployee();

    [OperationContract]
    [WebGet(UriTemplate = "devices/device/{id}")]
    DeviceView GetDeviceById(string id);

    [OperationContract]
    [WebGet(UriTemplate = "metrics"  )]
    ICollection<MetricView> GetListMetricSimple();

    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "device/employees")]
    void SetDeviceEmployee(string idDevice, string idEmployees);

    [OperationContract]
    [WebGet(UriTemplate = "/employees"  )]
    ICollection<EmployeeView> GetListEmployees();

    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "employees/employee/{id}")]
    void SetEmployee(string id,EmployeeView employee);

    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "device/command")]
    void SetCommandeDevice(string id, string action);

    [OperationContract]
    [WebGet(UriTemplate = "/{sensorType}/{dateType}")]
    ICollection<DataMetricView> GetListMetrics(string sensorType, string dateType);

    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "{sensorType}/{dateType}")]
    void ReceptCalculatedMetrics(string sensorType, string dateType, string result);

    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/employee/connect")]
    Object ConnectionWebService(string email, string password);




}

