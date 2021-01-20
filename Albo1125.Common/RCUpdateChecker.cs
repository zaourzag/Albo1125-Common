using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using mscoree;
using Rage;

namespace Albo1125.Common
{
    public class RCUpdateChecker
    {
        private const string updateApi = "https://raw.githubusercontent.com/RelaperCrystal/RCSOnlineService/master/";
        string url;
        InitializationFile downloadedFile;
        WebClient client = new WebClient();

        public RCUpdateChecker(string name)
        {
            Name = name;
            url = $"{updateApi}{name}";
        }

        public string Name { get; private set; }

        public void CheckForUpdates(int versionIndex)
        {
            string temponaryPath = Path.GetTempFileName();
            client.DownloadFile(url, temponaryPath);
            downloadedFile = new InitializationFile(temponaryPath);
            if (downloadedFile.ReadBoolean("Main", "AcquireUpdates", false) || downloadedFile.ReadBoolean("Main", "ReleasedAVersion", false))
            {
                int index = downloadedFile.ReadInt32("Main", "Version", versionIndex);
                if (index != versionIndex)
                {
                    Game.DisplayNotification("Update available for " + Name);
                }
            }
            else
            {
                Game.LogTrivial("The update file is not released.");
            }
            
        }
    }
}
