using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Source.INI
{
    class Subsystem
    {
        public string Loader;
        public List<string> initFiles = new List<string>();
        public List<string> initPaths = new List<string>();
        public List<string> extensions = new List<string>(); //unused
        public List<string> initFilesDebug = new List<string>();
        public List<string> excludePaths = new List<string>();
        public List<string> includePathsCinematics = new List<string>();
    }
}
