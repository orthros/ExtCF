using log4net;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ExtCF.Hosting
{
    public class InjectInstanceProvider<T> : IInstanceProvider, IContractBehavior
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(InjectInstanceProvider<T>));

        private Func<T> ConstructorMethod { get; set; }

        public InjectInstanceProvider(Func<T> constructorMethod)
        {
            if(constructorMethod == null)
            {
                throw new ArgumentNullException("constructorMethod");
            }

            this.ConstructorMethod = constructorMethod;
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return ConstructorMethod.Invoke();
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.GetInstance(instanceContext);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            var disposeable = instance as IDisposable;
            if (disposeable != null)
            {
                disposeable.Dispose();
            }
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {

        }
    }
}
