using System.IO;

namespace projectX.services
{
    public class InitFolders
    {
        public InitFolders()
        {
            if (!Directory.Exists("cases"))
            {
                Directory.CreateDirectory("cases");
            }
        }
    }
}
