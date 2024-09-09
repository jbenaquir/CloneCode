using System;
using System.Reflection;
using System.Web.Http.Dependencies;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Asotea.Infrastructure.DependencyInjection
{
    public class DiContainer : IDiContainer
    {
        private static IDiContainer Instance;

        private readonly Container Container;

        private DiContainer()
        {
            Container = new Container();
        }

        public static IDiContainer GetInstance()
        {
            return Instance ?? (Instance = new DiContainer());
        }

        public static void Dispose()
        {
            Instance = null;
        }

        public void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            Container.Register<TService, TImplementation>(Lifestyle.Singleton);
        }
        public void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            Container.Register<TService, TImplementation>(Lifestyle.Transient);
        }
        public void RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            Container.Register<TService, TImplementation>(Lifestyle.Scoped);
        }

        public void RegisterGenericsSingleton(Type openGenericServiceType, Assembly assemblies)
        {
            Container.Register(openGenericServiceType, assemblies, Lifestyle.Singleton);
        }

        public void RegisterSingletonWithFactory<TService>(Func<TService> instanceCreator) where TService : class
        {
            Container.Register<TService>(instanceCreator, Lifestyle.Singleton);
        }

        // Use for generic implementations. No extra parameters needed and no classes needed
        public void RegisterSingletonMakeGenericType<TService>(Type genericImplementationType, Type defaultImplementationType)
        {
            Container.RegisterConditional(typeof(TService),
                c =>
                {
                    Type implementationType = c.Consumer?.ImplementationType ?? defaultImplementationType;
                    return genericImplementationType.MakeGenericType(implementationType);
                },
                Lifestyle.Singleton,
                c => true);
        }

        public T GetService<T>() where T : class
        {
            return Container.GetInstance<T>();
        }

        public IDependencyResolver GetDependencyResolverForWebApi()
        {
            return new SimpleInjectorWebApiDependencyResolver(Container);
        }
    }
}
