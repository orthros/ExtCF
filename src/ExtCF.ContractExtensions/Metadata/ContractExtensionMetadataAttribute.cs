using ExtCF.ContractExtensions.Extension;
using System;
using System.ComponentModel.Composition;

namespace ExtCF.ContractExtensions.Metadata
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ContractExtensionMetadataAttribute : ExportAttribute, IContractExtensionMetadata
    {
        public string FunctionName
        {
            get;
            protected set;
        }

        public ContractExtensionMetadataAttribute(string functionName)
            : base(typeof(ContractExtension))
        {
            if(string.IsNullOrWhiteSpace(functionName))
            {
                throw new ArgumentNullException("functionName");
            }

            this.FunctionName = functionName;
        }
    }
}
