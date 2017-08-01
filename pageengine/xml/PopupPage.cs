using logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using uk.org.maxwell.pageengine.browserdrivers;
using uk.org.maxwell.pageengine.browserdrivers.interfaces;
using uk.org.maxwell.pageengine.services;

namespace uk.org.maxwell.pageengine.xml
{
    public class PopupPage : Page
    {
        public PopupPage() : base()
        {}

        public new void Open()
        {
            OpenAsPopup();
        }
    }
}
