using System.ServiceModel.Description;

namespace ExtCF.ContractExtensions.Factory
{
    public interface IContractExtensionFactory
    {
        void ApplyContractExtensions(ServiceEndpoint endpoint);
    }
}
