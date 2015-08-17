using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Graphics
{
    class Hierarchy
    {
        public static Dictionary<string, Hierarchy> hierarchies = new Dictionary<string, Hierarchy>();

        public struct Pivot
        {
            public string name;
            public long parentID;
            public Vector3 position;
            public Vector3 eulerAngles;
            public Quaternion rotation;
        }

        public long pivotCount;
        public Vector3 centerPos;
        public List<Pivot> pivots = new List<Pivot>();

        public Hierarchy(long pivotCount, Vector3 centerPos)
        {
            this.pivotCount = pivotCount;
            this.centerPos = centerPos;
        }

        public void addPivot(string name, long parentID, Vector3 position, Vector3 eulerAngles, Quaternion rotation)
        {
            Pivot p = new Pivot();
            p.name = name;
            p.parentID = parentID;
            p.position = position;
            p.eulerAngles = eulerAngles;
            p.rotation = rotation;
            pivots.Add(p);
        }
    }
}
