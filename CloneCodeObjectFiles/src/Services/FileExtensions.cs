using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Services
{
    public class FileExtensions : IFileExtensions
    {
        /// <summary>
        /// Returns file names from given folder that comply to given filters
        /// </summary>
        /// <param name="sourceFolder">Folder with files to retrieve</param>
        /// <param name="filter">Multiple file filters separated by | character</param>
        /// <param name="searchOption">File.IO.SearchOption, 
        /// could be AllDirectories or TopDirectoryOnly</param>
        /// <returns>Array of FileInfo objects that presents collection of file names that 
        /// meet given filter</returns>
        public string[] GetFiles(
            string sourceFolder
            , string filter
            , SearchOption searchOption)
        {
            // ArrayList will hold all file names
            ArrayList alFiles = new ArrayList();

            // Create an array of filter string
            string[] MultipleFilters = filter.Split('|');

            // for each filter find mathing file names
            foreach (string FileFilter in MultipleFilters)
            {
                // add found file names to array list
                alFiles.AddRange(Directory.GetFiles(sourceFolder, FileFilter, searchOption));
            }

            // returns string array of relevant file names
            return (string[])alFiles.ToArray(typeof(string));
        }

        public IEnumerable<string> GetFilesToClone(
             string path
             , string targetObjectName
             , string includeFileExceptions
         )
        {
            string searchPattern = $"*{targetObjectName}*";

            if (!string.IsNullOrEmpty(includeFileExceptions))
            {
                searchPattern += $"|{includeFileExceptions}";
            }

            return GetFiles(path, searchPattern, SearchOption.AllDirectories);
        }
    }
}
