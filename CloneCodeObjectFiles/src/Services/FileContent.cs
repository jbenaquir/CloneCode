using Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Services
{
    public class FileContent : IFileContent
    {
        private readonly IStringTools StringTools;

        public FileContent(
            IStringTools stringTools
        )
        {
            StringTools = stringTools;
        }

        public void ReplaceFileContent(
            IEnumerable<MapObjectName> mapObjectNameList
            , string file
            , Dictionary<string, string> statusFiles
        )
        {
            if (statusFiles.ContainsKey($"ReplaceFileContent: {file}"))
            {
                return;
            }

            statusFiles.Add($"ReplaceFileContent: {file}", file);

            string content = File.ReadAllText(file);

            content = StringTools.ReplaceContent(mapObjectNameList, content);

            File.WriteAllText(file, content);
        }

        public void CloneFileReferences(
            IEnumerable<MapObjectName> mapObjectNameList
            , string file
            , Dictionary<string, string> statusFiles
        )
        {
            if (statusFiles.ContainsKey($"ObjectReference: {file}"))
            {
                return;
            }

            statusFiles.Add($"ObjectReference: {file}", file);

            List<string> fileLines = File.ReadAllLines(file).ToList();

            Dictionary<int, string> objectSourceLines = GetSourceLines(mapObjectNameList, fileLines);
            InsertTargetObjectLines(mapObjectNameList, fileLines, objectSourceLines);

            File.WriteAllLines(file, fileLines);
        }

        private void InsertTargetObjectLines(IEnumerable<MapObjectName> mapObjectNameList, List<string> fileLines, Dictionary<int, string> linesFound)
        {
            foreach (var objectSourceLine in linesFound)
            {
                string clonedLine = StringTools.ReplaceContent(mapObjectNameList, objectSourceLine.Value);
                InsertTargetObjectLine(fileLines, objectSourceLine, clonedLine);
            }
        }

        private static void InsertTargetObjectLine(List<string> fileLines, KeyValuePair<int, string> objectSourceLine, string clonedLine)
        {
            if (!fileLines.Contains(clonedLine))
            {
                fileLines.Insert(objectSourceLine.Key, clonedLine);
            }
        }

        private Dictionary<int, string> GetSourceLines(IEnumerable<MapObjectName> mapObjectNameList, List<string> fileLines)
        {
            Dictionary<int, string> linesFound = new Dictionary<int, string>();

            foreach (var mapObjectName in mapObjectNameList)
            {
                FindSourceLines(fileLines, linesFound, mapObjectName);
            }

            return linesFound;
        }

        private void FindSourceLines(List<string> fileLines, Dictionary<int, string> linesFound, MapObjectName mapObjectName)
        {
            foreach (var lineFound in fileLines.Where(l => l.Contains(mapObjectName.SourceObjectName)))
            {
                AddSourceLine(fileLines, linesFound, lineFound);
            }
        }

        private void AddSourceLine(List<string> fileLines, Dictionary<int, string> linesFound, string lineFound)
        {
            int indexLine = fileLines.IndexOf(lineFound);

            if (!linesFound.Keys.Contains(indexLine))
            {
                linesFound.Add(indexLine, lineFound);
            }
        }
    }
}