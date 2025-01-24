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



        static PlatformPaths()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                CurrentPlatform = new WindowsPlatform();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                CurrentPlatform = new LinuxPlatform();
            }
            else
            {
                CurrentPlatform = new ProbablyAndroidPlatform();
            }
        }

    }
}
