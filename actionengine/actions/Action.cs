using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace actionengine.actions
{
    [XmlRoot("Action")]
    public class Action
    {
        [XmlAttribute("Name")]
        public string Name;

        [XmlAttribute("Method")]
        public string Method;

        [XmlElement("Test")]
        public Test[] Test;

        [XmlElement("Match")]
        public string[] Match;

        //[XmlElement("Description")]
        //public string Description;

        [XmlElement("Dependencies")]
        public Dependencies Dependencies;
    }
}
