namespace Asotea.Infrastructure.Mapper
{
    public interface IMapper
    {
        TResult MapTo<TResult>(object source);
    }
}