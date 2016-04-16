using System.Runtime.Serialization;

namespace ExtCF.ContractExtensions.Extension
{
    public abstract class ContractExtension
    {
        public abstract DataContractResolver GetResolver();
    }
}
