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
public interface IServiceDevice
{

    [OperationContract]
    void ReceptMetric(MetricView metric);

    [OperationContract]
    [WebGet(UriTemplate = "/devices", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    ICollection<DeviceView> GetAllDevice();

    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/device/{idDevice}/telemetry", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    void saveMetrics(string idDevice, string value);

    // TODO: ajoutez vos opérations de service ici
}

