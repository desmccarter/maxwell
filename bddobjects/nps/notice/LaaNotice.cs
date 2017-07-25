using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using bddobjects;

namespace uuk.org.hs2.npsdomainobjects.nps.notice
{
    [Serializable()]
    [XmlRoot("LaaNotice")]
    public class LaaNotice : IDomainObject
    {
        [XmlAttribute("NoticeId")]
        public string NoticeId { get; set; }

        [XmlAttribute("NoticeGenenerationDate")]
        public string NoticeGenenerationDate { get; set; }

        [XmlAttribute("SurveysDateFrom")]
        public string SurveysDateFrom { get; set; }

        [XmlAttribute("SurveysDateTo")]
        public string SurveysDateTo { get; set; }

        [XmlAttribute("LrPeriod")]
        public string LrPeriod { get; set; }

        [XmlAttribute("Reason")]
        public string Reason { get; set; }

        public LaaNotice()
        {

        }

        public LaaNotice(string noticeId)
        {
            NoticeId = noticeId;
        }
    }
}
