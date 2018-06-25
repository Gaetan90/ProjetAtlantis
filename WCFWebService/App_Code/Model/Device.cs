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
    public int id { get; set; }
    public string nom { get; set; }
    public string adressMac { get; set; }
    public int MyProperty { get; set; }
}