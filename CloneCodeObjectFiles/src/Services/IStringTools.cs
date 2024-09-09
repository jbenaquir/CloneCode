using Models;
using System.Collections.Generic;

namespace Services
{
    public interface IStringTools
    {
        string ReplaceContent(IEnumerable<MapObjectName> mapObjectNameList, string content);
    }
}