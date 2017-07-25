using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace uk.org.hs2.npsdomainobjects.nps.gpn
{
    [XmlRoot("Gpn")]
    public class Gpn
    {
        [XmlAttribute("LgnPublicationDate")]
        public string LgnPublicationDate;
    }
}
