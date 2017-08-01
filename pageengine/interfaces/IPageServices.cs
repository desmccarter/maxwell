using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.org.maxwell.pageengine.pageinterface
{
	public interface IPageServices
	{
        void SetDropdownUsingXPath(string xpath, string value);
        void DoubleClickElementUsingXPath(string xpath);
		void ClickElementUsingXPath(string xpath);
		List<string> GetElementTextListUsingXPath(string xpath);
		string GetElementValueUsingXPath(string xpath);
		void SetElementTextUsingXPath(string xpath, string value);
		string GetElementAttributeUsingXPath(string xpath, string attrName);
	}
}
