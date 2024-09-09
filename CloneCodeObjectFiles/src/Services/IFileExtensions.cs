using System.Collections.Generic;
using System.IO;

namespace Services
{
    public interface IFileExtensions
    {
        string[] GetFiles(
            string sourceFolder
            , string filter
            , SearchOption searchOption
        );

        IEnumerable<string> GetFilesToClone(
             string path
             , string targetObjectName
             , string includeFileExceptions
         );
    }
}