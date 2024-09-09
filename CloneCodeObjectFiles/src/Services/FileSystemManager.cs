using Models;
using System.Collections.Generic;
using System.IO;

namespace Services
{
    public class FileSystemManager : IFileSystemManager
    {
        private readonly IFileCloner FileCloner;
        private readonly IFileContent FileContent;

        public FileSystemManager(
            IFileContent fileContent
            , IFileCloner fileCloner
        )
        {
            FileCloner = fileCloner;
            FileContent = fileContent;
        }

        public void ManageCloning(CloneObject cloneObject)
        {
            if (!IsValid(cloneObject))
            {
                return;
            }

            cloneObject.StatusFiles = new Dictionary<string, string>();

            if (!IsDirectoryValid(cloneObject))
            {
                return;
            }

            CloneMapObjects(cloneObject);
            CloneReferences(cloneObject);
        }

        private void CloneReferences(CloneObject cloneObject)
        {
            foreach (var objectReference in cloneObject.ObjectReferencesList)
            {
                string file = $"{cloneObject.Root}{objectReference.FileReferencesPath}";

                FileContent.CloneFileReferences(cloneObject.MapObjectNameList, file, cloneObject.StatusFiles);
            }
        }

        private void CloneMapObjects(CloneObject cloneObject)
        {
            foreach (var mapObject in cloneObject.MapObjectNameList)
            {
                FileCloner.CloneFiles(cloneObject.Path, cloneObject.MapObjectNameList, mapObject, cloneObject.FilesExceptions, cloneObject.StatusFiles);
            }
        }

        bool IsValid(CloneObject cloneObject)
        {
            if (string.IsNullOrEmpty(cloneObject.Root))
            {
                return false;
            }

            if (string.IsNullOrEmpty(cloneObject.DirectoryName))
            {
                return false;
            }

            return true;
        }

        bool IsDirectoryValid(CloneObject cloneObject)
        {
            if (!Directory.Exists(cloneObject.Path))
            {
                cloneObject.StatusFiles.Add("PathDoesNotExists", cloneObject.Path);
                return false;
            }

            return true;
        }

    }
}
