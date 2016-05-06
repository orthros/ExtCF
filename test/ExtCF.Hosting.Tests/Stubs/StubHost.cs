using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtCF.Hosting.Tests.Stubs
{
    internal class StubHost
    {
        public string MyString { get; private set; }
        public int MyInt { get; private set; }

        public StubHost(string myString, int myInt)
        {
            MyString = myString;
            MyInt = myInt;
        }
    }
}
