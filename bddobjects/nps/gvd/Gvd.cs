using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace uk.org.hs2.npsdomainobjects.nps.gvd
{
    [XmlRoot("Gvd")]
    public class Gvd
    {
        [XmlAttribute("PreliminaryServiceDate")]
        public string PreliminaryServiceDate;

        [XmlAttribute("GvdExecutionDate")]
        public string GvdExecutionDate;
        
        [XmlAttribute("GeneralVestingPeriod")]
        public string GeneralVestingPeriod;

        [XmlAttribute("GvdNumber")]
        public string GvdNumber;
    }
}
