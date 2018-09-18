using AdoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Description résumée de Metric
/// </summary>
/// 
[DataContract]
public class MetricView
{
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public ICollection<Object> values { get; set; }
    [DataMember]
    public DateTime date { get; set; }
    [DataMember]
    public int nbrValues { get; set; }
    [DataMember]
    public string nameTypeDivice { get; set; }

    [DataMember]
    public DeviceView device { get; set; }


    public MetricView getMetricsToMetricsView(Metric metric)
    {
        MetricView metricView = new MetricView();
        metricView.id = metric.id;
        metricView.nbrValues = metric.nbrValues.Value;
        metricView.nameTypeDivice = metric.Device.TypeDevice.name;
        return metricView;
    }
}