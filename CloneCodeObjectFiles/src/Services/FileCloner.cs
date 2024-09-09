using Models;
using System.Collections.Generic;
using System.IO;

namespace Services
{
    public class FileCloner : IFileCloner
    {
        private readonly IFileExtensions FileExtensions;
        private readonly IFileContent FileReplacer;

        public FileCloner(
            IFileExtensions fileExtensions
            , IFileContent fileReplacer
        )
        {
            FileExtensions = fileExtensions;
            FileReplacer = fileReplacer;
        }

        public void CloneFiles(
            string path
            , IEnumerable<MapObjectName> mapObjectNameList
            , MapObjectName mapObjectName
            , string includeFileExceptions
            , Dictionary<string, string> statusFiles
        )
        {
            foreach (string sourceFileName in FileExtensions.GetFilesToClone(path, mapObjectName.SourceObjectName, includeFileExceptions))
            {
                string targetFileName = GetTargetFileName(mapObjectNameList, sourceFileName);

                CloneFile(sourceFileName, targetFileName, statusFiles);
                FileReplacer.ReplaceFileContent(mapObjectNameList, targetFileName, statusFiles);
            }
        }

        private void CloneFile
        (
            string sourceFileName
            , string targetFileName
            , Dictionary<string, string> statusFiles
        )
        {
            var directoryName = Path.GetDirectoryName(targetFileName);

            if (!Directory.Exists(directoryName))
            {
                statusFiles.Add($"CreatedDirectory:{targetFileName}", directoryName);
                Directory.CreateDirectory(directoryName);
            }

            if (!File.Exists(targetFileName))
            {
                statusFiles.Add($"ClonedFile:{targetFileName}", targetFileName);
                File.Copy(sourceFileName, targetFileName);
            }
        }

        private string GetTargetFileName(
            IEnumerable<MapObjectName> mapObjectNameList
            , string sourceFileName
        )
        {
            string targetFileName = sourceFileName;

            foreach (var mapObjectName in mapObjectNameList)
            {
                targetFileName = ReplaceFileName(targetFileName, mapObjectName);
            }

            return targetFileName;
        }

        private string ReplaceFileName(string targetFileName, MapObjectName mapObjectName)
        {
            if (targetFileName.Contains(mapObjectName.SourceObjectName))
            {
                targetFileName = targetFileName.Replace(mapObjectName.SourceObjectName, mapObjectName.TargetObjectName);
            }

            return targetFileName;
        }
    }
}
