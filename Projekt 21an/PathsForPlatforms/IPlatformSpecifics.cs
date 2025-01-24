using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an.PathsForPlatforms
{
    public interface IPlatformSpecifics
    {
        public string GetFilePath(string fileName);

        public string GetBaseDirectoryPath();

        public string GetAppDataFolderPath();
    }
}
