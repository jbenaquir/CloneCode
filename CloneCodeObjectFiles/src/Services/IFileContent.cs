using Models;
using System.Collections.Generic;

namespace Services
{
    public interface IFileContent
    {
        void ReplaceFileContent(
            IEnumerable<MapObjectName> mapObjectNameList
            , string file
            , Dictionary<string, string> statusFiles
        );

        void CloneFileReferences(
            IEnumerable<MapObjectName> mapObjectNameList
            , string file
            , Dictionary<string, string> statusFiles
        );
    }
}