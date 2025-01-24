﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an.PathsForPlatforms
{
    public class LinuxPlatform : IPlatformSpecifics
    {
        public string GetAppDataFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public string GetBaseDirectoryPath()
        {
            return AppContext.BaseDirectory;
        }

        public string GetFilePath(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
