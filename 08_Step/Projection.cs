using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;

namespace _08_Step
{
    class Projection
    {
        public static ArrayList DoParallelProjection(ArrayList L_3D)
        {
            ArrayList L_2D = new ArrayList();

            for (int i = 0; i < L_3D.Count; i++)
            {
                CPoint3D_Node ptrv = (CPoint3D_Node)L_3D[i];
                PointF pnn = new PointF(ptrv.X, ptrv.Y);
                L_2D.Add(pnn);                
            }

            return L_2D;
        }

        //public static ArrayList DoPrespectiveProjection(ArrayList L_3D, float focal)
        //{            
        //    ArrayList L_2D = new ArrayList();
        //    for (int i = 0; i < L_3D.Count; i++)
        //    {
        //        CPoint3D_Node vw = (CPoint3D_Node)L_3D[i];

        //        PointF pnn = new PointF();
        //        pnn.X = (float)(vw.X * (focal / vw.Z));
        //        pnn.Y = (float)(vw.Y * (focal / vw.Z));

        //        L_2D.Add(pnn);
        //    }
        //    return L_2D;
        //}

        public static void DoPrespectiveProjection(CPoint3D_Node e, CPoint3D_Node n, float focal)
        {
            n.X = focal * e.X / e.Z;
            n.Y = focal * e.Y / e.Z;
            n.Z = focal;            
        }

    }
}
