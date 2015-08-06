using System.Reflection;
using System.IO;

namespace SageCS.Core
{
    class Resource
    {
        private static string ShaderResource = "SageCS.Shader.";

        public static Stream GetShader(string name)
        {
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            Stream s = curAssembly.GetManifestResourceStream(ShaderResource + name);
            s.Position = 0;
            return s;
        }
    }
}
