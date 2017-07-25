using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using uk.org.hs2.pageengine.services;

namespace uk.org.hs2.pageengine.xml
{
    [XmlRoot("InheritsPages")]
    public class InheritsPages
    {
        [XmlElement("InheritsPage")]
        public Page[] Page;
    }
}
