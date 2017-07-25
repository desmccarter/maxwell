using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace actionengine.actions
{
    [XmlRoot("Dependencies")]
    public class Dependencies
    {
        [XmlElement("DependsOn")]
        public string[] DependsOn;
    }
}
