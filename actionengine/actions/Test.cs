using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace actionengine.actions
{
    [XmlRoot("Test")]
    public class Test
    {
        [XmlAttribute("Name")]
        public string Name;

        [XmlElement("Parameter")]
        public string[] Parameter;
    }
}
