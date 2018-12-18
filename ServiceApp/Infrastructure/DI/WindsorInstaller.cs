using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ServiceApp.Core.Charge;
using ServiceApp.Infrastructure.Stores;
using Stripe;

namespace ServiceApp.Infrastructure.DI
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICheckStatus, AppChargeService>().LifestyleTransient(),
                Component.For<ICalculateUnits, AppChargeService>().LifestyleTransient(),
                Component.For<IRoundAmount, AppChargeService>().LifestyleTransient(),
                Component.For<IUnitConverter, UnitConverter>().LifestyleTransient(),
                
                Component.For<ICurrenciesStore, CurrenciesStore>().LifestyleScoped()
            );
        }
    }
}