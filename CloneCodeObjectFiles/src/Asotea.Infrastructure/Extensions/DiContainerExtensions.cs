using Asotea.Infrastructure.DependencyInjection;
using Asotea.Infrastructure.Mapper;

namespace Asotea.Infrastructure.Extensions
{
    public static class DiContainerExtensions
    {
        public static void RegisterInfrastructure(this IDiContainer container)
        {
            container.RegisterSingleton<IMapper, Mapper.Mapper>();
        }
    }
}
