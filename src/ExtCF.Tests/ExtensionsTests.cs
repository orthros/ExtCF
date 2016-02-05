using ExtCF.ContractExtensions.Factory;
using Orth.Core.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExtCF.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void ConstructorFailOnNulls()
        {
            ILog nullLog = null;
            var returnedException = Assert.Throws(typeof(ArgumentNullException), () =>
             {
                 IContractExtensionFactory factory = new ContractExtensionFactory(nullLog);
             });

            Assert.IsType(typeof(ArgumentNullException), returnedException);

            Assert.Equal("log", (returnedException as ArgumentNullException).ParamName);            
        }

    }
}
