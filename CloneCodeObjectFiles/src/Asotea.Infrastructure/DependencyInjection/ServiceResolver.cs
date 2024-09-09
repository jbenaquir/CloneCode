namespace Asotea.Infrastructure.DependencyInjection
{
    public static class ServiceResolver
    {
        public static T Get<T>() where T : class
        {
            return DiContainer.GetInstance().GetService<T>();
        }
    }
}
