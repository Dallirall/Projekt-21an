using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an.PathsForPlatforms
{
    public class OtherPlatform : IPlatformSpecifics
    {
        public string GetAppDataFolderPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }

        public string GetBaseDirectoryPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }

        public string GetFilePath(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
