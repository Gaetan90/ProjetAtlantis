using AdoModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService" à la fois dans le code et le fichier de configuration.
[ServiceContract]
public interface IServiceDevice
{
    Service Service { get; set; }

    [OperationContract]
    [WebGet(UriTemplate = "devices")]
    ICollection<DeviceView> GetAllDevice();

    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "device")]
    void CreateNewDevice(string id, string name,string deviceType);

    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "device/{id}/telemetry")]
    int SaveMetrics(string id, string metricDate,string deviceType, string metricValue);

    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "device/{idDevice}/command")]
    void SetCommandDevice(string idDevice, string commande);


}

