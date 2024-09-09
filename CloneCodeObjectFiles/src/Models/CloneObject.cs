using System.Collections.Generic;

namespace Models
{
    public class CloneObject
    {
        public string Root { get; set; }
        public string DirectoryName { get; set; }
        public string Path => $"{Root}{DirectoryName}";
        public IEnumerable<MapObjectName> MapObjectNameList { get; set; }
        public IEnumerable<ObjectReference> ObjectReferencesList { get; set; }
        public string FilesExceptions { get; set; }

        public Dictionary<string, string> StatusFiles { get; set; }
    }
}
