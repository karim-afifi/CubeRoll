using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _08_Step
{
    class Matrix
    {
        static public void Normalise(CPoint3D_Node v)
        {
            float length;

            length = (float)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            v.X /= length;
            v.Y /= length;
            v.Z /= length;
        }

        static public CPoint3D_Node CrossProduct(CPoint3D_Node p1, CPoint3D_Node p2)
        {
            CPoint3D_Node p3;
            p3 = new CPoint3D_Node(0, 0, 0);
            p3.X = p1.Y * p2.Z - p1.Z * p2.Y;
            p3.Y = p1.Z * p2.X - p1.X * p2.Z;
            p3.Z = p1.X * p2.Y - p1.Y * p2.X;
            return p3;
        }
    }
}
