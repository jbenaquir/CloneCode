using System;
using System.Reflection;
using System.Web.Http.Dependencies;

namespace Asotea.Infrastructure.DependencyInjection
{
    public interface IDiContainer
    {
        void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        void RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        T GetService<T>() where T : class;
        IDependencyResolver GetDependencyResolverForWebApi();
        void RegisterGenericsSingleton(Type openGenericServiceType, Assembly assemblies);
        void RegisterSingletonWithFactory<TService>(Func<TService> instanceCreator) where TService : class;
        void RegisterSingletonMakeGenericType<TService>(Type genericImplementationType, Type defaultImplementationType);
    }
}