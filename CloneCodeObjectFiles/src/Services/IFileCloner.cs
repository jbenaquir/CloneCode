using Models;
using System.Collections.Generic;

namespace Services
{
    public interface IFileCloner
    {
        void CloneFiles(
            string path
            , IEnumerable<MapObjectName> mapObjectNameList
            , MapObjectName mapObjectName
            , string includeFileExceptions
            , Dictionary<string, string> statusFiles
        );
    }
}
