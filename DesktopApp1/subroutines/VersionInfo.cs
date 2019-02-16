using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp1
{
    public class VersionInfo
    {
        private string url;
        private string releaseDate;
        private string version;
        public VersionInfo()
        {

        }
        public VersionInfo(string versionNumber, string Date, string link)
        {
            version = versionNumber;
            releaseDate = Date;
            url = link;
        }
        public void setLink(string link)
        {
            url = link;
            return;
        }
        public void setReleaseDate(string Date)
        {
            releaseDate = Date;
            return;
        }
        public void setVersion(string versionNumber)
        {
            version = versionNumber;
            return;
        }
        public string getVersion()
        {
            return version;
        }
        public string getReleaseDate()
        {
            return releaseDate;
        }
        public string getLink()
        {
            return url;
        }
    }
}
