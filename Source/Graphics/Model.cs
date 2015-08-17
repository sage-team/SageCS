using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Graphics
{
    class Model
    {
        public static Dictionary<string, Model> models = new Dictionary<string, Model>();

        public struct Box
        {
            public Vector3 center;
            public Vector3 extend;
        }

        //add the instances here? 
        public List<W3DMesh> meshes = new List<W3DMesh>();
        public Box box;

        public Model()
        {

        }
    }
}
