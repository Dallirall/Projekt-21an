using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an.PathsForPlatforms
{
    public static class PlatformPaths
    {
        public static IPlatformSpecifics CurrentPlatform { get; private set; }

        //public static string AppFolderPath { get; private set; }

        static PlatformPaths()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                CurrentPlatform = new WindowsPlatform();
                //AppFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //Lägg till AppFolderPath
                CurrentPlatform = new LinuxPlatform();
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }


    }
}
