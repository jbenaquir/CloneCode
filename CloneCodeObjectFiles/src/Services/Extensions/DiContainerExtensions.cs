using Asotea.Infrastructure.DependencyInjection;
using Asotea.Infrastructure;

namespace Services.Extensions
{
    public static class DiContainerExtensions
    {
        public static void RegisterServices(this IDiContainer container)
        {
            container.RegisterSingleton<IFileCloner, FileCloner>();
            container.RegisterSingleton<IFileExtensions, FileExtensions>();
            container.RegisterSingleton<IFileSystemManager, FileSystemManager>();
            container.RegisterSingleton<IFileContent, FileContent>();
            container.RegisterSingleton<IStringTools, StringTools>();
        }
    }
}
