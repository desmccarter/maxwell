using bddobjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using uk.org.hs2.genericutils;
using uk.org.hs2.npsdomainobjects.nps.gpn;
using uk.org.hs2.npsdomainobjects.nps.gvd;
using uk.org.hs2.npsdomainobjects.serialisation;
using uuk.org.hs2.npsdomainobjects.nps.notice;

namespace uk.org.hs2.npsdomainobjects.nps.laa
{
    [Serializable()]
    [XmlRoot("HEADER")]
    public class Laa : IDomainObject
    {
        // *** every time an instance of Laa is created,
        // *** this gets incremented. For caching ...

        private string batchServiceMethod = "Recorded Delivery";


        public string GetAccessMethod()
        {
            return accessMethod;
        }

        public void SetAccessMethod(string accessMethod)
        {
            this.accessMethod = accessMethod;
        }

        public const string DefaultAcquisitionMethodS2 = "S2 ACCESS";

        public const string DefaultAcquisitionMethodS16 = "S16 (Schedule 16)";

        private string acquisitionMethod = DefaultAcquisitionMethodS16;

        private string accessMethod = DefaultAcquisitionMethodS2;

        public void SetAcquisitonMethod(string acquisitionMethod)
        {
            this.acquisitionMethod = acquisitionMethod;
        }

        public string GetAcquisitonMethod()
        {
            return acquisitionMethod;
        }

        public void SetWithBatchServiceMethod(string batchServiceMethod)
        {
            this.batchServiceMethod = batchServiceMethod;
        }

        public string GetBatchServiceMethod()
        {
            return batchServiceMethod;
        }

        public void SetBatchServiceMethod(string batchServiceMethod)
        {
            this.batchServiceMethod = batchServiceMethod;
        }

        protected static int index = 0;

        public bool TestDetermineAccessMethodPage = false;
        public bool TestNoticePlansUploadPage = false;
        public bool TestRecordingPreliminaryDetailsPage = false;
        public bool TestCreateLaaPage = false;

        protected Hashtable expectationsMap = new Hashtable();

        public void SetExpectedPartiesWithNoticesRemoved(List<string> parties)
        {
            expectationsMap.Add("PARTIES_WITH_NOTICES_REMOVED", parties);
        }

        public void SetExpectedPartiesWithNotices(List<string> parties)
        {
            if (expectationsMap["PARTIES_WITH_NOTICES"] == null)
            {
                expectationsMap.Add("PARTIES_WITH_NOTICES", parties);
            }
        }

        public List<string> GetExpectedPartiesWithNotices()
        {
            return expectationsMap["PARTIES_WITH_NOTICES"] as List<string>;
        }

        public List<string> GetExpectedInterestsExcludedFromLap()
        {
            return expectationsMap["INTERESTS_WTHDRAWN_FROM_LAPS"] as List<string>;
        }

        public List<string> GetExpectedInterestsIncludedInLap()
        {
            return expectationsMap["INTERESTS_INCLUDED_IN_LAPS"] as List<string>;
        }

        public void SetExpectedInterestsExcludedFromLap(List<string> interests)
        {
            expectationsMap.Add("INTERESTS_WTHDRAWN_FROM_LAPS", interests);
        }

        public void SetExpectedInterestsIncludedInLap(List<string> interests)
        {
            expectationsMap.Add("INTERESTS_INCLUDED_IN_LAPS", interests);
        }

        public List<string> GetExpectedPartiesWithNoticesRemoved()
        {
            return expectationsMap["PARTIES_WITH_NOTICES_REMOVED"] as List<string>;
        }

        [XmlElement("Gvd")]
        public Gvd Gvd;

        [XmlElement("Gpn")]
        public Gpn Gpn;

        [XmlElement("LaaNotice")]
        public LaaNotice[] LaaNotice;

        [XmlElement("Index")]
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        [XmlAttribute("Code")]
        public string Code { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Description")]
        public string Description { get; set; }

        [XmlAttribute("ClrNumber")]
        public string CrNumber { get; set; }

        [XmlAttribute("Flab")]
        public string Flab { get; set; }

        [XmlAttribute("ConstructionSector")]
        public string ConstructionSector { get; set; }

        [XmlAttribute("RequiredDateOfEntry")]
        public string RequiredDateOfEntry { get; set; }

        [XmlAttribute("TransactionStep")]
        public LaaWorkflowStageEnum TransactionStep { get; set; }

        [XmlAttribute("LandAcquisitionStatus")]
        public LaaStageLandAcquisitionSearchPageEnum LandAcquisitionStatus { get; set; }

        [XmlAttribute("LaaType")]
        public LaaTypeEnum LaaType = LaaTypeEnum.S2;

        [XmlAttribute("laaId")]
        public string laaId { get; set; }

        [XmlAttribute("RequestIdAssignPsc")]
        public string RequestIdAssignPsc { get; set; }

        [XmlAttribute("psc")]
        public string psc { get; set; }

        [XmlAttribute("NoticeBatchDriverId")]
        public string NoticeBatchDriverId { get; set; }

        [XmlAttribute("NoticeBatchId")]
        public string NoticeBatchId { get; set; }

        protected const string cacheFolder = @"c:\temp\npscache";

        public static List<Laa> GetCachedLaasByWorkflowStage(LaaWorkflowStageEnum stage)
        {
            List<Laa> laaList = new List<Laa>();

            string[] files = null;

            if (Directory.Exists(cacheFolder))
            {
                files = Directory.GetFiles(cacheFolder);

                foreach (string file in files)
                {
                    try
                    {
                        Laa laa = Deserialize(file);

                        if (laa.TransactionStep.Equals(stage))
                        {
                            laaList.Add(laa);
                        }
                    }
                    catch (Exception e)
                    {
                        // ... *** ignore
                    }
                }
            }

            return laaList;
        }

        public void Serialize()
        {
            if (!Directory.Exists(cacheFolder))
            {
                Directory.CreateDirectory(cacheFolder);
            }

            Serialize(cacheFolder + @"\LAA_" + LaaType + "_" + Code + ".xml");
        }

        public void UpdateLaaWorkflowStatus(LaaWorkflowStageEnum nextLaaWorkflowStage)
        {
            TransactionStep = nextLaaWorkflowStage;
            Serialize();
        }

        public static Laa Deserialize(string path)
        {
            return (Laa)GenericData.DeserializedBddObject(path, typeof(Laa));
        }

        public void Serialize(string path)
        {
            GenericData.SerializeBddObject(this, path);
        }

        public Laa()
        {
            // *** set all the properties in
            // *** this LAA object with random/default
            // *** values ...

            string random = GenericUtils.GetRandomString(8);

            Description = "LAADescription" + random;
            Name = "LAAName" + random;
            Code = "LAACode" + random;
            ConstructionSector = "S3";
            CrNumber = CRDefaultValue;

            DateTime now = DateTime.Now.AddDays(30);

            string day = now.Day.ToString().PadLeft(0, '0');
            string month = now.Month.ToString().PadLeft(0, '0');
            string year = now.Year.ToString().PadLeft(0, '0');

            RequiredDateOfEntry = year + "-" + month + "-" + day;
        }

        public const string CRDefaultValue = "CR98765";

        public static int CurrentIndex
        {
            get { return index; }
        }
    }
}
