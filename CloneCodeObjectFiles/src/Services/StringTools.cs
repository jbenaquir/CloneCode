using Models;
using System.Collections.Generic;

namespace Services
{
    public class StringTools : IStringTools
    {
        public string ReplaceContent(IEnumerable<MapObjectName> mapObjectNameList, string content)
        {
            foreach (var mapObjectName in mapObjectNameList)
            {
                content = ReplaceMappingValue(content, mapObjectName);
            }

            return content;
        }

        private string ReplaceMappingValue(string content, MapObjectName mapObjectName)
        {
            if (content.Contains(mapObjectName.SourceObjectName))
            {
                content = content.Replace(mapObjectName.SourceObjectName, mapObjectName.TargetObjectName);
            }

            return content;
        }
    }
}
