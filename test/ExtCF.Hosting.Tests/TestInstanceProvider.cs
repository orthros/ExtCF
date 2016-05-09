using ExtCF.Hosting.Tests.Stubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExtCF.Hosting.Tests
{
    public class TestInstanceProvider
    {
        [Fact]
        public void TestConstructor()
        {
            string mystring = "adsf";
            int myInt = 1;

            Func<StubHost> conFunc = () => new StubHost(mystring, myInt);

            var provider = new InjectInstanceProvider<StubHost>(conFunc);

            var testVal = provider.GetInstance(null);
            Assert.IsType(typeof(StubHost), testVal);            
        }        
    }
}
