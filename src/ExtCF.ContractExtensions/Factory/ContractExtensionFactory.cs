using ExtCF.ContractExtensions.Extension;
using ExtCF.ContractExtensions.Metadata;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.ServiceModel.Description;

namespace ExtCF.ContractExtensions.Factory
{
    public class ContractExtensionFactory : IContractExtensionFactory
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(ContractExtensionFactory));

        [ImportMany]
        private IEnumerable<Lazy<ContractExtension, IContractExtensionMetadata>> _extensions;
        private IEnumerable<Lazy<ContractExtension, IContractExtensionMetadata>> Extensions
        {
            get
            {
                if (_extensions == null)
                {
                    ComposeContainers();
                }
                return _extensions;
            }
        }

        private CompositionContainer _container;
        private CompositionContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        public ContractExtensionFactory()
        {            
            ComposeContainers();
        }        

        public void ApplyContractExtensions(ServiceEndpoint endpoint)
        {
            var contract = endpoint.Contract;
            foreach (var extension in Extensions)
            {
                var operationDescriptions = contract.Operations.Find(extension.Metadata.FunctionName);

                if (operationDescriptions != null)
                {
                    var serializerBehavior = operationDescriptions.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (serializerBehavior == null)
                    {
                        serializerBehavior = new DataContractSerializerOperationBehavior(operationDescriptions);
                        operationDescriptions.Behaviors.Add(serializerBehavior);
                    }
                    serializerBehavior.DataContractResolver = extension.Value.GetResolver();
                }
                else
                {
                    Logger.Warn($"No operation descriptions found for function name: {extension.Metadata.FunctionName}");
                }

            }

        }

        #region Private Functions
        private void ComposeContainers()
        {
            var catalog = new AggregateCatalog();

            LoadAssemblyExtensions(ref catalog);
            LoadDynamicExtensions(ref catalog);

            Container = new CompositionContainer(catalog);
            Container.ComposeParts(this);
        }

        private void LoadAssemblyExtensions(ref AggregateCatalog catalog)
        {
            if(catalog == null)
            {
                throw new ArgumentNullException(nameof(catalog));
            }
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ContractExtensionFactory).Assembly));
        }

        private void LoadDynamicExtensions(ref AggregateCatalog catalog)
        {
            if(catalog == null)
            {
                throw new ArgumentNullException(nameof(catalog));
            }

            DirectoryInfo newDin = new DirectoryInfo(Environment.CurrentDirectory);
            DirectoryCatalog dcat = new DirectoryCatalog(newDin.FullName);
            catalog.Catalogs.Add(dcat);
        }

        #endregion
    }
}
