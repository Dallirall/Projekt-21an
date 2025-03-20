using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an.PathsForPlatforms
{
    public class MacOSPlatform : IPlatformSpecifics
    {
        public string GetFilePath(string fileName)
        {
            throw new NotImplementedException();
        }

        public string GetBaseDirectoryPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public string GetAppDataFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }
    }
}
