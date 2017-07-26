using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace uk.org.hs2.shareddomainobjects.serialisation
{
    public class GenericData
    {
        public static IDomainObject DeserializedBddObject(string path, Type bddObjectType)
        {
            XmlSerializer serializer = new XmlSerializer(bddObjectType);

            StreamReader reader = new StreamReader(path);
            object bddobject = serializer.Deserialize(reader);
            reader.Close();

            return (IDomainObject)bddobject;
        }

        public static void SerializeBddObject(IDomainObject bddobject, string path)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream,
                Encoding.Unicode);

            XmlSerializer writer = new XmlSerializer(bddobject.GetType());

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            ns.Add("", "");

            writer.Serialize(streamWriter, bddobject, ns);

            byte[] utf8EncodedXml = memoryStream.ToArray();

            File.WriteAllBytes(path, memoryStream.ToArray());
        }
    }

}
