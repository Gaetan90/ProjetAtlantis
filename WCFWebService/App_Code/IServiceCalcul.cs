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
    [WebGet(UriTemplate = "/devices", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    ICollection<DeviceView> GetListDevicesByEmployee();

    [OperationContract]
    [WebGet(UriTemplate = "/metrics", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    ICollection<MetricView> GetListMetricSimple();

    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/device/{idDevice}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    void SetDeviceEmployee(string idDevice, string value);


}

