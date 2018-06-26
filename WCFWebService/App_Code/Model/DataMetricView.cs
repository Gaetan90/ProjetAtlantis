using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Description résumée de DataMetricView
/// </summary>
/// 
[DataContract]
public class DataMetricView
{
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public string value { get; set; }
    [DataMember]
    public MetricView metric { get; set; }
}