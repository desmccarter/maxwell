using bddobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.org.hs2.npsdomainobjects.simplebddobjects
{
    public class StringBddObject : IDomainObject
    {
        public StringBddObject(string value)
        {
            Value = value;
        }

        public string Value { get; set;  }
    }
}
