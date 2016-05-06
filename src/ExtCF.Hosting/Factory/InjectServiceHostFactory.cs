using log4net;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace ExtCF.Hosting.Factory
{
    public class InjectServiceHostFactory<T> : ServiceHostFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(InjectServiceHostFactory<T>));

        Func<T> ConstructorFunction { get; set; }

        public InjectServiceHostFactory(Func<T> constructorFunction)
            : base()
        {
            Log.Debug("Constructing a new Factory");

            if (constructorFunction == null)
            {
                throw new ArgumentNullException(nameof(constructorFunction));
            }

            this.ConstructorFunction = constructorFunction;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            Log.Debug("Creating a new ServiceHost");
            return new InjectionServiceHost<T>(ConstructorFunction, serviceType, baseAddresses);
        }
    }
}
