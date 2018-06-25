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
public class Metric
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
    public TypeDevice type { get; set; }
    [DataMember]
    public Device device { get; set; }
}