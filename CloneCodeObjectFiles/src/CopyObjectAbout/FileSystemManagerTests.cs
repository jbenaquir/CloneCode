using Asotea.Infrastructure.DependencyInjection;
using Models;
using Services;
using Services.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace CopyObjectAbout
{
    public class FileSystemManagerTests : IDisposable
    {
        private readonly IFileSystemManager FileSystemManager;

        public FileSystemManagerTests()
        {
            IDiContainer diContainer = DiContainer.GetInstance();
            diContainer.RegisterServices();

            FileSystemManager = ServiceResolver.Get<IFileSystemManager>();
        }

        [Theory]
        [InlineData(@"\components\", "index.js")]   //  Separated per |
        [InlineData(@"\store\modules\")]
        public void CopyDirectoryObjectTest(string directoryName, string includeFileExceptions = null)
        {
            var mapList = new List<MapObjectName>();

            mapList.Add(new MapObjectName { SourceObjectName = "Objects", TargetObjectName = "Perros" });
            mapList.Add(new MapObjectName { SourceObjectName = "objects", TargetObjectName = "perros" });
            mapList.Add(new MapObjectName { SourceObjectName = "Object", TargetObjectName = "Perro" });
            mapList.Add(new MapObjectName { SourceObjectName = "object", TargetObjectName = "perro" });

            CloneObject cloneObject = new CloneObject()
            {
                Root = @"D:\Temp\SolutionTest",
                StatusFiles = new Dictionary<string, string>(),
                MapObjectNameList = mapList,
                DirectoryName = directoryName,
                FilesExceptions = includeFileExceptions
            };

            FileSystemManager.ManageCloning(cloneObject);

            Assert.True(true);
        }


        //WORKING PERFECTLY
        //After this check routes and router.
        [Theory]
        [InlineData(@"\components\")]
        [InlineData(@"\components\brands\", "index.js")]   // NOTE: Origin to copy. Allow multiple files separated per |
        [InlineData(@"\store\modules\")]
        public void CopyDirectoryVueProjectTest(string directoryName, string includeFileExceptions = null)
        {
            var mapObjectNameList = new List<MapObjectName>();

            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "Brands", TargetObjectName = "ChannelSessions" });
            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "brands", TargetObjectName = "channelSessions" });
            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "BRANDS", TargetObjectName = "CHANNEL_SESSIONS" });
            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "Brand", TargetObjectName = "ChannelSession" });
            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "brand", TargetObjectName = "channelSession" });
            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "BRAND", TargetObjectName = "CHANNEL_SESSION" });

            CloneObject cloneObject = new CloneObject()
            {
                Root = @"D:\Repository\asotea\AsoteaCRMClient\src",
                MapObjectNameList = mapObjectNameList,
                ObjectReferencesList = GetFrontVueTemplate_ObjectReferencesList(),
                DirectoryName = directoryName,
                FilesExceptions = includeFileExceptions
            };

            FileSystemManager.ManageCloning(cloneObject);

            Assert.True(true);
        }

        private IEnumerable<ObjectReference> GetFrontVueTemplate_ObjectReferencesList()
        {
            var objectReferencesList = new List<ObjectReference>();

            objectReferencesList.Add(new ObjectReference()
            {
                FileReferencesPath = @"\store\index.js"
            });

            objectReferencesList.Add(new ObjectReference()
            {
                FileReferencesPath = @"\store\modules\index.js"
            });

            objectReferencesList.Add(new ObjectReference()
            {
                FileReferencesPath = @"\lib\constants\api.js"
            });

            objectReferencesList.Add(new ObjectReference()
            {
                FileReferencesPath = @"\router\router.js"
            });

            /*
            objectReferencesList.Add(new ObjectReference()
            {
                FileReferencesPath = @"\lib\constants\routes.js",
                Template =
@"
export const businessesRoute = {
  name: '${SourceObjectName}',
  path: '/management/${SourceObjectName}',
  url: '/management/${SourceObjectName}',
  needsAuth: true,
  layout: 'CrmLayout'
}"
            });
            */

            return objectReferencesList;
        }

        //WORKING PERFECTLY
        //After this add files to projects
        [Theory]
        [InlineData(@"\Asotea.Api\Controllers\")]
        [InlineData(@"\Asotea.Services\Chat\")]
        [InlineData(@"\Asotea.DataAccess\Criteria\")]
        [InlineData(@"\Asotea.DataObjects\")]
        [InlineData(@"\Asotea.Models\")]
        ///SQL Database optional
        //  [InlineData(@"\Asotea.Database\dbo\Tables\")]
        //  [InlineData(@"\Asotea.Database\dbo\Stored Procedures\")]
        public void CopyDirectoryApiProjectTest(string directoryName, string includeFileExceptions = null)
        {
            bool sqlServer = false;

            var mapObjectNameList = new List<MapObjectName>();

            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "Messages", TargetObjectName = "ChannelSessions" });
            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "messages", TargetObjectName = "channelSessions" });
            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "MESSAGES", TargetObjectName = "CHANNEL_SESSIONS" });
            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "Message", TargetObjectName = "ChannelSession" });
            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "message", TargetObjectName = "channelSession" });
            mapObjectNameList.Add(new MapObjectName { SourceObjectName = "MESSAGE", TargetObjectName = "CHANNEL_SESSION" });

            CloneObject cloneObject = new CloneObject()
            {
                Root = @"D:\Repository\asotea\AsoteaCRMServer\src",
                MapObjectNameList = mapObjectNameList,
                ObjectReferencesList = GetBackApiTemplate_ObjectReferencesList(sqlServer),
                DirectoryName = directoryName,
                FilesExceptions = includeFileExceptions
            };

            FileSystemManager.ManageCloning(cloneObject);

            Assert.True(true);
        }


        private IEnumerable<ObjectReference> GetBackApiTemplate_ObjectReferencesList(bool sqlServer)
        {
            var objectReferencesList = new List<ObjectReference>();

            objectReferencesList.Add(new ObjectReference()
            {
                FileReferencesPath = @"\Asotea.DataAccess\Extensions\DiContainerExtensions.cs"
            });

            objectReferencesList.Add(new ObjectReference()
            {
                FileReferencesPath = @"\Asotea.Services\Extensions\DiContainerExtensions.cs"
            });
            objectReferencesList.Add(new ObjectReference()
            {
                FileReferencesPath = @"\Asotea.Api\Asotea.Api.csproj"
            });

            if (sqlServer)
            {
                //Optional for SqlServer entity
                objectReferencesList.Add(new ObjectReference()
                {
                    FileReferencesPath = @"\Asotea.Database\Asotea.Database.sqlproj"
                });
            }

            return objectReferencesList;
        }


        public void Dispose()
        {
            DiContainer.Dispose();
        }
    }
}
