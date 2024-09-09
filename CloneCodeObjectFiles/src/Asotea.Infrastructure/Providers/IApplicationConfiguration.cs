namespace Asotea.Infrastructure.Providers
{
    public interface IApplicationConfiguration
    {
        int UserSessionExpirationTimeout { get; }
        int CutOffDay { get; }
    }
}
