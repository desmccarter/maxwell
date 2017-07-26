using logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using uk.org.hs2.pageengine.browserdrivers;
using uk.org.hs2.pageengine.browserdrivers.interfaces;
using uk.org.hs2.pageengine.services;

namespace uk.org.hs2.pageengine.xml
{
    [XmlRoot("Page")]
    public class Page : SeleniumPageServices
    {
        [XmlElement("InheritsPages", IsNullable = true)]
        public InheritsPages InheritsPages;

        [XmlAttribute("Name")]
        public string Name;

        [XmlAttribute("Url")]
        public string Url;

        [XmlElement("Element")]
        public Element[] Element;

        protected Element GetElementByName(string name)
        {
            if (Element == null)
            {
                throw new Exception("[ERR] Element array is null");
            }

            Element e =
                Element.Where(item => item.Name.Equals(name)).ToArray().Length > 0 ?
                Element.Where(item => item.Name.Equals(name)).ToArray()[0] : null;

            if (e == null)
            {
                throw new Exception("[ERR] Element array is NOT null but first item is null");
            }

            return e;
        }

        public void Open()
        {
            Open(Url);
        }

        public List<string> GetTextList(string elementName)
        {
            Element e = GetElementByName(elementName);

            List<string> value = null;

            try
            {
                value = GetElementTextListUsingXPath(e.XPath);

                Log.Debug("[INFO] Received " + e.Name + ". Value is '" + value + "'");
            }
            catch (Exception ex)
            {
                Log.ErrAndFail("[INFO] Failed to received element " + e.Name + ". Exception is '" + ex.Message + "'");
            }

            return value;
        }

        public string GetText(string elementName)
        {
            Element e = GetElementByName(elementName);

            string value = null;

            try
            {
                value = GetElementValueUsingXPath(e.XPath);

                Log.Debug("[INFO] Received " + e.Name + ". Value is '" + value + "'");
            }
            catch (Exception ex)
            {
                Log.ErrAndFail("[INFO] Failed to received element " + e.Name + ". Exception is '" + ex.Message + "'");
            }

            return value;
        }

        public void AssertElementAndTextAreEqual(string elementName, string expectedValue)
        {
            string actualValue = GetText(elementName);

            AssertAreEqual(expectedValue, actualValue, null);
        }

        public void AssertAreEqual(string expectedValue, string actualValue, string errorMessage)
        {

            //Assert.AreEqual(expectedValue, actualValue, "[ERR] Element text does not match expected value");

            // *** assert above throws an exception for no reason what so ever (it seems) so
            // *** doing a manual IF ...

            if ( expectedValue == null )
            {
                if( actualValue != null )
                {
                    throw new Exception("[ASSERT-ERR] Expected null actual value - " + errorMessage);
                }
            }
            else
            if( actualValue == null )
            {
                throw new Exception("[ASSERT-ERR] Actual value is null - " + errorMessage);
            }
            else
            if( !expectedValue.Equals(actualValue) )
            {
                throw new Exception("[ASSERT-ERR] Expected value '" + expectedValue+"' does not match actual value '"+actualValue+"' " + errorMessage);
            }
        }

        public void SetText(string elementName, string text)
        {
            Element e = GetElementByName(elementName);

            try
            {
                SetElementTextUsingXPath(e.XPath, text);

                Log.Debug("[INFO] Set element " + e.Name + ". Value is now '" + text + "'");
            }
            catch (Exception ex)
            {
                Log.ErrAndFail("[INFO] Failed to set element " + e.Name + ". Exception is '" + ex.Message + "'");
            }
        }

        public void SetDropdown(string elementName, string text)
        {
            Element e = GetElementByName(elementName);

            try
            {
                SetDropdownUsingXPath(e.XPath, text);

                Log.Debug("[INFO] Set element " + e.Name + ". Value is now '" + text + "'");
            }
            catch (Exception ex)
            {
                Log.ErrAndFail("[INFO] Failed to set element " + e.Name + ". Exception is '" + ex.Message + "'");
            }
        }

        public void DoubleClick(string elementName)
        {
            Element e = GetElementByName(elementName);

            try
            {
                ClickElementUsingJavaScript(e.XPath);

                Log.Debug("[INFO] Double click element " + e.Name + " successful.");
            }
            catch (Exception ex)
            {
                Log.ErrAndFail("[INFO] Failed to double click element " + e.Name + ". Exception is '" + ex.Message + "'");
            }
        }

        public void Click(string elementName)
        {
            Element e = GetElementByName(elementName);

            try
            {
                ClickElementUsingXPath(e.XPath);

                Log.Debug("[INFO] Click element " + e.Name + " successful.");
            }
            catch (Exception ex)
            {
                Log.ErrAndFail("[INFO] Failed to click element " + e.Name + ". Exception is '" + ex.Message + "'");
            }
        }
    }
}
