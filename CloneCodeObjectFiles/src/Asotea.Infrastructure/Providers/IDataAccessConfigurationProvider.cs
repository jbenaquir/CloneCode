namespace Asotea.Infrastructure.Providers
{
    public interface IDataAccessConfigurationProvider
    {
        string ConnectionString { get; }
        int DefaultTimeout { get; }
    }
}
