using logging;
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
