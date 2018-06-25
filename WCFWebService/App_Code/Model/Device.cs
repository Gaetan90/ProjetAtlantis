using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Description résumée de Device
/// </summary>
/// 
[DataContract]
public class Device
{
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public string nom { get; set; }
    [DataMember]
    public string adressMac { get; set; }
    [DataMember]
    public ICollection<Employee> employees { get; set; }
}