using System;
using System.Diagnostics;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;

namespace Covid19.Infrastructure.Networking.Ioc
{
    public sealed class Ioc
    {
        private Ioc() { }

        private static readonly Lazy<Ioc> Lazy = new Lazy<Ioc>(() => new Ioc());

        public static Ioc Current => Lazy.Value;

        private IContainerProvider _containerProvider;
        private IContainerProvider ContainerProvider => _containerProvider ?? (_containerProvider = Prism.PrismApplicationBase.Current.Container);
        private IContainerExtension ContainerExtension => ContainerProvider as IContainerExtension;

        public void SetContainer(IContainerProvider containerProvider)
            => _containerProvider = containerProvider;

        public INavigationService CreateNavigationService(Page forPage) => ContainerExtension.CreateNavigationService(forPage);

        public T Resolve<T>() where T : class
        {
            try
            {
                return ContainerProvider?.Resolve<T>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return null;
        }

        public T Resolve<T>(string name) where T : class
        {
            try
            {
                return ContainerProvider?.Resolve<T>(name);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return null;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry, params IIocRegistry[] initializers)
        {
            foreach (var initializer in initializers)
                initializer.Register(containerRegistry);
        }
    }
}
