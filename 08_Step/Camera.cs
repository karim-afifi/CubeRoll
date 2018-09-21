using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _08_Step
{
    class Camera
    {
        public CPoint3D_Node cop;
        public CPoint3D_Node lookAt;
        public CPoint3D_Node up;
        public double  front, back;

        public float focal = 3;


        // vectors
        public CPoint3D_Node basisa, lookDir, basisc;

        public int ceneterX, ceneterY;
        public int cxScreen, cyScreen;

        public Camera()
        {
            cop = new CPoint3D_Node(0, 0, -350); // new Point3D(0, -50, 0);
            lookAt = new CPoint3D_Node(0, 0, 50);     //new Point3D(0, 50, 0);
            up = new CPoint3D_Node(0, 1, 0);
            front = 10; // 70.0;
            back = 300.0;
        }

        public void BuildNewSystem()
        {
            lookDir = new CPoint3D_Node(0, 0, 0);
            basisa = new CPoint3D_Node(0, 0, 0);
            basisc = new CPoint3D_Node(0, 0, 0);

            lookDir.X = lookAt.X - cop.X;
            lookDir.Y = lookAt.Y - cop.Y;
            lookDir.Z = lookAt.Z - cop.Z;
            Matrix.Normalise(lookDir);

            basisa = Matrix.CrossProduct(up, lookDir);
            Matrix.Normalise(basisa);

            basisc = Matrix.CrossProduct(lookDir, basisa);
            Matrix.Normalise(basisc);
        }

        public void TransformToOrigin_And_Rotate(CPoint3D_Node a, CPoint3D_Node e)
        {
            CPoint3D_Node w = new CPoint3D_Node(a.X , a.Y , a.Z);
            w.X -= cop.X;
            w.Y -= cop.Y;
            w.Z -= cop.Z;

            e.X = w.X * basisa.X + w.Y * basisa.Y + w.Z * basisa.Z;
            e.Y = w.X * basisc.X + w.Y * basisc.Y + w.Z * basisc.Z;
            e.Z = w.X * lookDir.X + w.Y * lookDir.Y + w.Z * lookDir.Z;            
        }
       
        public PointF TransformToOrigin_And_Rotate_And_Project(CPoint3D_Node w1)
        {
            CPoint3D_Node e1, n1;
            e1 = new CPoint3D_Node(0, 0, 0);
            n1 = new CPoint3D_Node(0, 0, 0);

            TransformToOrigin_And_Rotate(w1, e1);
            Projection.DoPrespectiveProjection(e1, n1, focal);

            // view mapping
            n1.X = (int)(ceneterX + cxScreen * n1.X / 2);
            n1.Y = (int)(ceneterY - cyScreen * n1.Y / 2);

            return new PointF(n1.X, n1.Y);
        }
    }
}
