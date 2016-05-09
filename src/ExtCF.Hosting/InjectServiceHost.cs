using log4net;
using System;
using System.ServiceModel;

namespace ExtCF.Hosting
{
    public class InjectionServiceHost<T> : ServiceHost
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(InjectionServiceHost<T>));

        private Func<T> ConstructorFunc { get; set; }

        public InjectionServiceHost(Func<T> constructor, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if (constructor == null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            this.ConstructorFunc = constructor;

            ApplyConstructor();
        }

        private void ApplyConstructor()
        {
            foreach (var cd in this.ImplementedContracts.Values)
            {
                Log.Debug("Applying a custom instance provider to " + cd.Name);
                cd.ContractBehaviors.Add(new InjectInstanceProvider<T>(ConstructorFunc));
            }
        }
    }
}
