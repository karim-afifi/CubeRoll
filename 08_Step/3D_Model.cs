using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.IO;
using System.Drawing;

namespace _08_Step
{
    class _3D_Model
    {
        public ArrayList L_3D_Pts;
        public ArrayList L_Edges;
        public int typeofcube;
        public bool upordown;

        public float focal = 3;
        public int XB = 0;
        public int YB = 0;

        public Camera cam;

        public Color clr = Color.Black;

        public void LoadFromFile(string strF)
        {

            L_3D_Pts = new ArrayList();
            L_Edges = new ArrayList();
            StreamReader sr = new StreamReader( strF );

            string strLine;
            int Flag = 0;
            while ( (strLine = sr.ReadLine())   != null)
            {
                if (strLine[0] == 'L')
                {
                    Flag = 1;
                    continue;
                }

                if (Flag == 0)
                {
                    string []ss = strLine.Split(',');
                    CPoint3D_Node pnn = new CPoint3D_Node( 
                                float.Parse (ss[0].Trim()),
                                float.Parse(ss[1].Trim()),
                                float.Parse(ss[2].Trim())
                                );

                    L_3D_Pts.Add(pnn);
                }

                if (Flag == 1)
                {
                    string[] ss = strLine.Split(',');
                    Edge pnn = new Edge(
                                int.Parse(ss[0].Trim()),
                                int.Parse(ss[1].Trim())
                                );

                    L_Edges.Add(pnn);
                }

            }

            sr.Close();
        }

        public void DrawYourSelf(Graphics g, int type)
        {
            if (typeofcube == 0 || typeofcube == 2 || typeofcube == 3)
            {
                Pen PP = new Pen(clr, 2);

                /*   if (type == 1)
               {
                    CPoint3D_Node first = (CPoint3D_Node)L_3D_Pts[0];
                    CPoint3D_Node second = (CPoint3D_Node)L_3D_Pts[1];
                    CPoint3D_Node third = (CPoint3D_Node)L_3D_Pts[3];
                    CPoint3D_Node fourth = (CPoint3D_Node)L_3D_Pts[4];
                   firstpoint.X = (int)first.X + XB ;
                   firstpoint.Y = (int)first.Y + YB ;
                   secondpoint.X = (int)second.X + XB ;
                   secondpoint.Y = (int)second.Y + YB ;
                   thirdpoint.X = (int)fourth.X + XB ;
                   thirdpoint.Y = (int)fourth.Y + YB ;
                   fourthpoint.X = (int)third.X + XB ;
                   fourthpoint.Y = (int)third.Y + YB ;
                   points = new Point[4];
                   points[0] = firstpoint;
                   points[1] = secondpoint;
                   points[2] = thirdpoint;
                   points[3] = fourthpoint;
                   g.FillPolygon(Brushes.Blue, points);
               }*/

                for (int i = 0; i < L_Edges.Count; i++)
                {
                    Edge ptrv = (Edge)L_Edges[i];

                    PointF s = cam.TransformToOrigin_And_Rotate_And_Project((CPoint3D_Node)L_3D_Pts[ptrv.E0]);
                    PointF e = cam.TransformToOrigin_And_Rotate_And_Project((CPoint3D_Node)L_3D_Pts[ptrv.E1]);

                    g.DrawLine(PP, s.X + XB, s.Y + YB, e.X + XB, e.Y + YB);

                    //Font FF = new Font("System", 10);
                    //for (int i = 0; i < L_2D.Count; i++)
                    //{
                    //    PointF v = (PointF)L_2D[i];
                    //    g.FillEllipse(Brushes.Red,
                    //                    XB + v.X - 5,
                    //                    YB + v.Y - 5,
                    //                    10, 10);
                    //    g.DrawString("P" + (i), FF, Brushes.Green, XB + v.X, YB + v.Y + 10);
                    //}
                }
            }
        }

        
        public void Rotat_Aroun_Edge(int iEdge, int checkside, int amount)
        {
            if (iEdge < 0 || iEdge >= L_Edges.Count)
                return;

                Edge e = (Edge)L_Edges[iEdge];
                CPoint3D_Node pointer = (CPoint3D_Node )L_3D_Pts[e.E0];
                CPoint3D_Node pointer2 = (CPoint3D_Node)L_3D_Pts[e.E1];

                CPoint3D_Node v1 = new CPoint3D_Node(pointer.X,pointer .Y, pointer .Z) ;
                CPoint3D_Node v2 = new CPoint3D_Node(pointer2.X, pointer2.Y, pointer2.Z);

            if (checkside == 0)
            {

                Transformation_API.RotateArbitrary(
                                            ref L_3D_Pts,
                                            v1, //(CPoint3D_Node)L_3D_Pts[0],
                                            v2, //(CPoint3D_Node)L_3D_Pts[1],
                                            amount);
            }
            else if(checkside == 1)
            {
                Transformation_API.RotateArbitrary(
                                            ref L_3D_Pts,
                                            v1, //(CPoint3D_Node)L_3D_Pts[0],
                                            v2, //(CPoint3D_Node)L_3D_Pts[1],
                                            -amount);
            }
        }
    
    }
}
