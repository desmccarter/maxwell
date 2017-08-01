﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.org.maxwell.shareddomainobjects.simplebddobjects
{
    public class StringDomainObject : IDomainObject
    {
        public StringDomainObject(string value)
        {
            Value = value;
        }

        public string Value { get; set;  }
    }
}
