using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using uk.org.maxwell.pageengine.services;

namespace uk.org.maxwell.pageengine.xml
{
    [XmlRoot("InheritsPages")]
    public class InheritsPages
    {
        [XmlElement("InheritsPage")]
        public Page[] Page;
    }
}
