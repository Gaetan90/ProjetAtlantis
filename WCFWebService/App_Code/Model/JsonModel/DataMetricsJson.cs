using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Description résumée de DataMetricsJson
/// </summary>
/// 
[DataContract]
public class DataMetricsJson
{
  [DataMember]
    public string deviceType { get; set; }
    [DataMember]
    public string dateType { get; set; }
    [DataMember]
    public double value { get; set; }

   
}