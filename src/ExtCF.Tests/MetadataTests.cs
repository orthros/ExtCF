using ExtCF.ContractExtensions.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace ExtCF.Tests
{
    public class MetadataTests
    {
        [Fact]
        public void TestEmptyStringParameter()
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
             {
                 ContractExtensionMetadataAttribute attribute = new ContractExtensionMetadataAttribute("");
             });
        }

        [Fact]
        public void TestNullStringParameter()
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
             {
                 ContractExtensionMetadataAttribute attribute = new ContractExtensionMetadataAttribute(null);
             });
        }

        [Theory, MemberData("FunctionNames")]        
        public void TestFunctionNames(string name)
        {
            var attribute = new ContractExtensionMetadataAttribute(name);
            Assert.Equal(name, attribute.FunctionName);
        }

        /// <summary>
        /// The names of the function to test for metadata
        /// TODO: Make this read from a file
        /// </summary>
        public static IEnumerable<object[]> FunctionNames
        {
            get
            {
                return new[]
                {
                    new object[] { "asdf" },
                    new object[] { "qwerty" }
                };
            }
        }
    }
}
