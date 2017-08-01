using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace uk.org.maxwell.pageengine.xml
{
    [XmlRoot("Element")]
    public class Element
    {
        [XmlAttribute("Name")]
        public string Name;

        [XmlAttribute("XPath")]
        public string XPath;
    }
}
