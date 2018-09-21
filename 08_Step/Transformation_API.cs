using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace _08_Step
{
    class Transformation_API
    {
        public static void Scale(ref ArrayList L_Model, float sx, float sy, float sz)
        {
            ArrayList L_new = new ArrayList();

            for (int i = 0; i < L_Model.Count; i++)
            {
                CPoint3D_Node v = (CPoint3D_Node)L_Model[i];

                float x = v.X;
                float y = v.Y;
                float z = v.Z;

                float x_ = x * sx;
                float y_ = y * sy;
                float z_ = z * sz;

                v.X = x_;
                v.Y = y_;
                v.Z = z_;
                L_new.Add(v);
            }

            L_Model.Clear();
            L_Model = L_new;
        }
        public static void RotatX(ref ArrayList L_Model , float theta)
        {
            ArrayList L_new = new ArrayList();

            float th = (float)(Math.PI * theta / 180);
            for (int i = 0; i < L_Model.Count; i++)
            {
                CPoint3D_Node v = (CPoint3D_Node)L_Model[i];

                float x = v.X;
                float y = v.Y;
                float z = v.Z;

                float x_ = x;
                float y_ = (float)(y * Math.Cos(th) - z * Math.Sin(th));
                float z_ = (float)(y * Math.Sin(th) + z * Math.Cos(th));

                v.X = x_;
                v.Y = y_;
                v.Z = z_;
                L_new.Add(v);
            }

            L_Model.Clear();
            L_Model = L_new;

        }

        public static void RotatY(ref ArrayList L_Model, float theta)
        {
            ArrayList L_new = new ArrayList();

            float th = (float)(Math.PI * theta / 180);
            for (int i = 0; i < L_Model.Count; i++)
            {
                CPoint3D_Node v = (CPoint3D_Node)L_Model[i];

                float x = v.X;
                float y = v.Y;
                float z = v.Z;

                float x_ = (float)(z * Math.Sin(th) + x * Math.Cos(th));
                float y_ = y;
                float z_ = (float)(z * Math.Cos(th) - x * Math.Sin(th));

                v.X = x_;
                v.Y = y_;
                v.Z = z_;
                L_new.Add(v);
            }

            L_Model.Clear();
            L_Model = L_new;
        }

        
        public static void RotatZ(ref ArrayList L_Model, float theta)
        {
            ArrayList L_new = new ArrayList(); 

            float th = (float)(Math.PI * theta / 180);
            for (int i = 0; i < L_Model.Count; i++)
            {
                CPoint3D_Node v = (CPoint3D_Node)L_Model[i];
                
                float x= v.X;
                float y= v.Y;
                float z= v.Z;

                float x_ = (float)(x * Math.Cos(th) - y * Math.Sin(th));
                float y_ = (float)(x * Math.Sin(th) + y * Math.Cos(th));
                float z_ = z;

                v.X = x_;
                v.Y = y_;
                v.Z = z_;
                L_new.Add(v);
            }

            L_Model.Clear();
            L_Model = L_new;
        }

        public static void TranslateX(ref ArrayList L_Model, float tx)
        {
            ArrayList L_new = new ArrayList();
            for (int i = 0; i < L_Model.Count; i++)
            {
                CPoint3D_Node v = (CPoint3D_Node)L_Model[i];
                v.X = v.X + tx;
                L_new.Add(v);
            }
            L_Model.Clear();
            L_Model = L_new;

        }

        public static void TranslateY(ref ArrayList L_Model, float ty)
        {
            ArrayList L_new = new ArrayList();
            for (int i = 0; i < L_Model.Count; i++)
            {
                CPoint3D_Node v = (CPoint3D_Node)L_Model[i];
                v.Y = v.Y + ty;
                L_new.Add(v);
            }
            L_Model.Clear();
            L_Model = L_new;
        }

        public static void TranslateZ(ref ArrayList L_Model, float tz)
        {
            ArrayList L_new = new ArrayList();
            for (int i = 0; i < L_Model.Count; i++)
            {
                CPoint3D_Node v = (CPoint3D_Node)L_Model[i];
                v.Z = v.Z + tz;
                L_new.Add(v);
            }
            L_Model.Clear();
            L_Model = L_new;

        }

        public static void RotateArbitrary( ref ArrayList L_Model, 
                                            CPoint3D_Node v1, 
                                            CPoint3D_Node v2, 
                                            int ang)
        {
            Transformation_API.TranslateX(ref L_Model, v1.X * -1);
            Transformation_API.TranslateY(ref L_Model, v1.Y * -1);
            Transformation_API.TranslateZ(ref L_Model, v1.Z * -1);

            float dx = v2.X - v1.X;
            float dy = v2.Y - v1.Y;
            float dz = v2.Z - v1.Z;

            float theta = (float)Math.Atan2(dy, dx);
            float phi = (float)Math.Atan2(Math.Sqrt(dx * dx + dy * dy), dz);

            theta = (float)(theta * 180 / Math.PI);
            phi = (float)(phi * 180 / Math.PI);
            Transformation_API.RotatZ(ref L_Model, theta * -1);
            Transformation_API.RotatY(ref L_Model, phi * -1);

            Transformation_API.RotatZ(ref L_Model, ang);

            Transformation_API.RotatY(ref L_Model, phi * 1);
            Transformation_API.RotatZ(ref L_Model, theta * 1);
            Transformation_API.TranslateZ(ref L_Model, v1.Z * 1);
            Transformation_API.TranslateY(ref L_Model, v1.Y * 1);
            Transformation_API.TranslateX(ref L_Model, v1.X * 1);
        }
    }
}
