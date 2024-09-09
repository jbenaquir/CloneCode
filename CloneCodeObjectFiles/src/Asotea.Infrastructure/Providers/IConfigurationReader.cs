namespace Asotea.Infrastructure.Providers
{
    public interface IConfigurationReader
    {
        string GetStringSetting(string settingKey);
        int GetIntSetting(string settingKey);
        bool GetBoolSetting(string settingKey);
    }
}
