using System;

namespace ExtCF.ContractExtensions.Metadata
{
    public interface IContractExtensionMetadata
    {
        /// <summary>
        /// The Name of the OperationContract to apply the Extension to
        /// </summary>
        string FunctionName { get; }
    }
}
