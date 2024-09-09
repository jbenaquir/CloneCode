using Mapster;

namespace Asotea.Infrastructure.Mapper
{
    internal class Mapper : IMapper
    {
        public TResult MapTo<TResult>(object source)
        {
            return source.Adapt<TResult>();
        }
    }
}
