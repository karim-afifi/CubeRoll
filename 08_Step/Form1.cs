using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace _08_Step
{
    public partial class Form1 : Form
    {
        int XB = 100;
        int YB = 50;
        int cx = 0;
        int cy = 0;


        Bitmap off;
        ArrayList Ground = new ArrayList();
        ArrayList Spikes = new ArrayList();
        _3D_Model Movingcube = new _3D_Model();
        Camera cam = new Camera();
        _3D_Model Cube = new _3D_Model();
        Timer t = new Timer();
        public Point x;
        public Point y;
        bool firstclick = false;
        bool whichground = false;
        int chanceornot = 0;
        int hole = 0;
        bool whichtoadd = false;
        int dropground = 0;
        int rowmovement = 41;
        int rowmovementcheckspike = 41;
        bool clickedrotate = false;
        bool rotsided = false;
        bool rotsidea = false;
        bool falling = false;
        bool crushed = false;
        int rotatecounter = 0;
        int fallingcubecounter = 0;
        bool fallsound = false;
        int spikemove = 0;
        bool checkdeath = false;
        System.Media.SoundPlayer bow = new System.Media.SoundPlayer(@"Bow.wav");
        System.Media.SoundPlayer fall = new System.Media.SoundPlayer(@"Drop.wav");
        System.Media.SoundPlayer breaking = new System.Media.SoundPlayer(@"Break.wav");

        public void addnewrow()
        {
            if (whichtoadd == false)
            {
                int startfgroundx = -75;
                float startgroundz;
                _3D_Model tempgr1 = (_3D_Model)Ground[Ground.Count - 3];
                CPoint3D_Node tempgr2 = (CPoint3D_Node)tempgr1.L_3D_Pts[1];
                startgroundz = tempgr2.Z;
                startgroundz += 45;

                _3D_Model cube3 = new _3D_Model();
                cube3.cam = cam;
                cube3.LoadFromFile("Cubewall.txt");
                cube3.typeofcube = 3;
                cube3.upordown = false;
                Transformation_API.Scale(ref cube3.L_3D_Pts, 0.2f, 0.2f, 0.2f);
                Transformation_API.TranslateZ(ref cube3.L_3D_Pts, (startgroundz - 10) - 50);
                Transformation_API.TranslateY(ref cube3.L_3D_Pts, -108);
                Transformation_API.TranslateX(ref cube3.L_3D_Pts, -163);
                cube3.Rotat_Aroun_Edge(3, 0, 90);
                cube3.Rotat_Aroun_Edge(8, 1, 146);
                cube3.Rotat_Aroun_Edge(7, 0, 12);

                cube3.clr = Color.DarkRed;
                Ground.Add(cube3);


                Random rnd1 = new Random();
                chanceornot = rnd1.Next(3);
                Random rnd2 = new Random();
                hole = rnd2.Next(4);


                for (int i = 0; i < 4; i++)
                {
                    _3D_Model cube2 = new _3D_Model();
                    cube2.cam = cam;
                    cube2.LoadFromFile("Cube2.txt");
                    cube2.upordown = false;
                    Transformation_API.Scale(ref cube2.L_3D_Pts, 0.2f, 0.005f, 0.2f);
                    Transformation_API.TranslateZ(ref cube2.L_3D_Pts, startgroundz);
                    Transformation_API.TranslateY(ref cube2.L_3D_Pts, -100);
                    Transformation_API.TranslateX(ref cube2.L_3D_Pts, startfgroundx);
                    cube2.Rotat_Aroun_Edge(1, 0, 45);
                    bool whichcolor = false;

                    if (chanceornot == 0)
                    {
                        cube2.typeofcube = 0;
                    }
                    else if (chanceornot == 1)
                    {
                        if (hole == i)
                        {
                            cube2.typeofcube = 1;
                        }
                    }
                    else if (chanceornot == 2)
                    {
                        if (hole == i)
                        {
                            cube2.typeofcube = 2;
                            whichcolor = true;
                            CPoint3D_Node forspikess = (CPoint3D_Node)cube2.L_3D_Pts[0];
                            addspikes(forspikess);
                        }
                    }

                    if(whichcolor == false)
                    {
                        cube2.clr = Color.DarkGray;
                    }
                    else if(whichcolor == true)
                    {
                        cube2.clr = Color.Purple;
                    }
                    startfgroundx += 50;
                    Ground.Add(cube2);
                }

                cube3 = new _3D_Model();
                cube3.cam = cam;
                cube3.LoadFromFile("Cubewall.txt");
                cube3.typeofcube = 3;
                cube3.upordown = false;
                Transformation_API.Scale(ref cube3.L_3D_Pts, 0.2f, 0.2f, 0.2f);
                Transformation_API.TranslateZ(ref cube3.L_3D_Pts, startgroundz - 10);
                Transformation_API.TranslateY(ref cube3.L_3D_Pts, -108);
                Transformation_API.TranslateX(ref cube3.L_3D_Pts, 102);
                cube3.Rotat_Aroun_Edge(3, 0, 90);
                cube3.Rotat_Aroun_Edge(8, 0, 45);

                cube3.clr = Color.DarkRed;
                Ground.Add(cube3);
                whichtoadd = true;
            }
            else if (whichtoadd == true)
            {
                int startfgroundx = -100;
                float startgroundz;
                _3D_Model tempgr1 = (_3D_Model)Ground[Ground.Count - 2];
                CPoint3D_Node tempgr2 = (CPoint3D_Node)tempgr1.L_3D_Pts[1];
                startgroundz = tempgr2.Z;
                startgroundz += 50;

                Random rnd1 = new Random();
                chanceornot = rnd1.Next(3);
                Random rnd2 = new Random();
                hole = rnd2.Next(5);

                for (int i = 0; i < 5; i++)
                {
                    _3D_Model cube2 = new _3D_Model();
                    cube2.cam = cam;
                    cube2.upordown = false;
                    cube2.LoadFromFile("Cube2.txt");
                    Transformation_API.Scale(ref cube2.L_3D_Pts, 0.2f, 0.005f, 0.2f);
                    Transformation_API.TranslateZ(ref cube2.L_3D_Pts, startgroundz);
                    Transformation_API.TranslateY(ref cube2.L_3D_Pts, -100);
                    Transformation_API.TranslateX(ref cube2.L_3D_Pts, startfgroundx);
                    cube2.Rotat_Aroun_Edge(1, 0, 45);
                    bool whichcolor = false;

                    if (chanceornot == 0)
                    {
                        cube2.typeofcube = 0;
                    }
                    else if (chanceornot == 1)
                    {
                        if (hole == i)
                        {
                            cube2.typeofcube = 1;
                        }
                    }
                    else if (chanceornot == 2)
                    {
                        if (hole == i)
                        {
                            cube2.typeofcube = 2;
                            whichcolor = true;
                            CPoint3D_Node forspikess = (CPoint3D_Node)cube2.L_3D_Pts[0];
                            addspikes(forspikess);
                        }
                    }

                    if (whichcolor == false)
                    {
                        cube2.clr = Color.DarkRed;
                    }
                    else if (whichcolor == true)
                    {
                        cube2.clr = Color.Purple;
                    }
                    startfgroundx += 50;
                    Ground.Add(cube2);
                }

                whichtoadd = false;
            }
        }
         
        public void translatebackrow()
        {
            for (int i = 0; i < Ground.Count; i++)
            {
                _3D_Model temp1 = (_3D_Model)Ground[i];
                Transformation_API.TranslateZ(ref temp1.L_3D_Pts, -28);
            }

            for (int i = 0; i < Spikes.Count; i++)
            {
                _3D_Model temp1 = (_3D_Model)Spikes[i];
                Transformation_API.TranslateZ(ref temp1.L_3D_Pts, -28);
            }
        }

        public void removeoldrow()
        {
            if (dropground < 8)
            {
                if (dropground < 7)
                {
                    _3D_Model temp1 = (_3D_Model)Ground[5];
                    _3D_Model temp2 = (_3D_Model)Ground[4];
                    _3D_Model temp3 = (_3D_Model)Ground[3];
                    _3D_Model temp4 = (_3D_Model)Ground[2];
                    _3D_Model temp5 = (_3D_Model)Ground[1];
                    _3D_Model temp6 = (_3D_Model)Ground[0];

                    for (int i = 0; i < temp1.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp1.L_3D_Pts[i];
                        temppoint.Y--;
                    }

                    for (int i = 0; i < temp2.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp2.L_3D_Pts[i];
                        temppoint.Y -= 2;
                    }
                    for (int i = 0; i < temp3.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp3.L_3D_Pts[i];
                        temppoint.Y--;
                    }
                    for (int i = 0; i < temp4.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp4.L_3D_Pts[i];
                        temppoint.Y -= 3;
                    }
                    for (int i = 0; i < temp5.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp5.L_3D_Pts[i];
                        temppoint.Y -= 4;
                    }
                    for (int i = 0; i < temp6.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp6.L_3D_Pts[i];
                        temppoint.Y -= 2;
                    }
                }
                else
                {
                    Ground.RemoveAt(5);
                    Ground.RemoveAt(4);
                    Ground.RemoveAt(3);
                    Ground.RemoveAt(2);
                    Ground.RemoveAt(1);
                    Ground.RemoveAt(0);
                    rowmovement -= 6;
                }
                dropground++;
            }
            else if (dropground >= 8 && dropground < 13)
            {
                if (dropground < 12)
                {
                    _3D_Model temp1 = (_3D_Model)Ground[4];
                    _3D_Model temp2 = (_3D_Model)Ground[3];
                    _3D_Model temp3 = (_3D_Model)Ground[2];
                    _3D_Model temp4 = (_3D_Model)Ground[1];
                    _3D_Model temp5 = (_3D_Model)Ground[0];

                    for (int i = 0; i < temp1.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp1.L_3D_Pts[i];
                        temppoint.Y--;
                    }
                    for (int i = 0; i < temp2.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp2.L_3D_Pts[i];
                        temppoint.Y -= 2;
                    }
                    for (int i = 0; i < temp3.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp3.L_3D_Pts[i];
                        temppoint.Y--;
                    }
                    for (int i = 0; i < temp4.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp4.L_3D_Pts[i];
                        temppoint.Y -= 3;
                    }
                    for (int i = 0; i < temp5.L_3D_Pts.Count; i++)
                    {
                        CPoint3D_Node temppoint = (CPoint3D_Node)temp5.L_3D_Pts[i];
                        temppoint.Y -= 4;
                    }
                }
                else
                {
                    Ground.RemoveAt(4);
                    Ground.RemoveAt(3);
                    Ground.RemoveAt(2);
                    Ground.RemoveAt(1);
                    Ground.RemoveAt(0);

                    rowmovement -= 5;
                }
                dropground++;
            }
            else if (dropground >= 13)
            {
                dropground = 0;
            }
        }

        public void buildandreset()
        {
            XB = 100;
            YB = 50;
            cx = 0;
            cy = 0;
            Ground.Clear();
            Spikes.Clear();
            Movingcube = new _3D_Model();
            cam = new Camera();
            Cube = new _3D_Model();
            x.X = 0;
            x.Y = 0;
            y.X = 0;
            y.Y = 0;
            chanceornot = 0;
            hole = 0;
            firstclick = false;
            whichground = false;
            whichtoadd = false;
            dropground = 0;
            rowmovement = 41;
            rowmovementcheckspike = 41;
            clickedrotate = false;
            rotsided = false;
            rotsidea = false;
            falling = false;
            crushed = false;
            checkdeath = false;
            rotatecounter = 0;
            fallingcubecounter = 0;
            fallsound = false;
            spikemove = 0;

            cx = (this.Width - 185);
            cy = (this.Height - 150);

            cam.ceneterX = XB + (cx / 2);
            cam.ceneterY = YB + (cy / 2);
            cam.cxScreen = cx;
            cam.cyScreen = cy;
            cam.cop.Y += 715;
            cam.cop.Z -= 360;

            Cube.cam = cam;

            Cube.LoadFromFile("Cube.txt");
            Cube.clr = Color.White;
            Transformation_API.Scale(ref Cube.L_3D_Pts, 1.31f, 1f, 4f);

            float startgroundz = -350;

            for (int j = 0; j < 30; j++)
            {
                float startfgroundx = -75;

                _3D_Model cube3 = new _3D_Model();
                cube3.cam = cam;
                cube3.LoadFromFile("Cubewall.txt");
                cube3.upordown = false;
                Transformation_API.Scale(ref cube3.L_3D_Pts, 0.2f, 0.2f, 0.2f);
                cube3.typeofcube = 3;
                Transformation_API.TranslateZ(ref cube3.L_3D_Pts, startgroundz - (10 + 50));
                Transformation_API.TranslateY(ref cube3.L_3D_Pts, -108);
                Transformation_API.TranslateX(ref cube3.L_3D_Pts, -163);
                cube3.Rotat_Aroun_Edge(3, 0, 90);
                cube3.Rotat_Aroun_Edge(8, 1, 146);
                cube3.Rotat_Aroun_Edge(7, 0, 12);

                cube3.clr = Color.DarkRed;
                Ground.Add(cube3);

                if (j > 10)
                {
                    Random rnd1 = new Random();
                    chanceornot = rnd1.Next(3);
                    Random rnd2 = new Random();
                    hole = rnd2.Next(4);
                }

                for (int i = 0; i < 4; i++)
                {
                    _3D_Model cube2 = new _3D_Model();
                    cube2.cam = cam;
                    cube2.upordown = false;
                    cube2.LoadFromFile("Cube2.txt");
                    Transformation_API.Scale(ref cube2.L_3D_Pts, 0.2f, 0.005f, 0.2f);
                    Transformation_API.TranslateZ(ref cube2.L_3D_Pts, startgroundz);
                    Transformation_API.TranslateY(ref cube2.L_3D_Pts, -100);
                    Transformation_API.TranslateX(ref cube2.L_3D_Pts, startfgroundx);
                    cube2.Rotat_Aroun_Edge(1, 0, 45);
                    bool changecolor = false;

                    if (j <= 10)
                    {
                        cube2.typeofcube = 0;
                    }
                    else if (j > 10 && chanceornot == 0)
                    {
                        cube2.typeofcube = 0;
                    }
                    else if (j > 10 && hole == i && chanceornot == 1)
                    {
                        cube2.typeofcube = 1;
                    }
                    else if (j > 10 && hole == i && chanceornot == 2)
                    {
                        cube2.typeofcube = 2;
                        changecolor = true;
                        CPoint3D_Node forspikess = (CPoint3D_Node)cube2.L_3D_Pts[0];
                        addspikes(forspikess);
                    }

                    if(changecolor == false)
                    {
                        cube2.clr = Color.DarkGray;
                    }
                    else
                    {
                        cube2.clr = Color.Purple;
                    }

                    startfgroundx += 50;
                    Ground.Add(cube2);
                }

                cube3 = new _3D_Model();
                cube3.cam = cam;
                cube3.LoadFromFile("Cubewall.txt");
                Transformation_API.Scale(ref cube3.L_3D_Pts, 0.2f, 0.2f, 0.2f);
                cube3.upordown = false;
                cube3.typeofcube = 3;
                Transformation_API.TranslateZ(ref cube3.L_3D_Pts, startgroundz - 10);
                Transformation_API.TranslateY(ref cube3.L_3D_Pts, -108);
                Transformation_API.TranslateX(ref cube3.L_3D_Pts, 102);
                cube3.Rotat_Aroun_Edge(3, 0, 90);
                cube3.Rotat_Aroun_Edge(8, 0, 45);

                cube3.clr = Color.DarkRed;
                Ground.Add(cube3);

                startgroundz += 27;
                startfgroundx = -100;

                if (j > 10)
                {
                    Random rnd1 = new Random();
                    chanceornot = rnd1.Next(3);
                    Random rnd2 = new Random();
                    hole = rnd2.Next(5);
                }

                for (int i = 0; i < 5; i++)
                {
                    _3D_Model cube2 = new _3D_Model();
                    cube2.cam = cam;
                    cube2.LoadFromFile("Cube2.txt");
                    cube2.upordown = false;
                    Transformation_API.Scale(ref cube2.L_3D_Pts, 0.2f, 0.005f, 0.2f);
                    Transformation_API.TranslateZ(ref cube2.L_3D_Pts, startgroundz);
                    Transformation_API.TranslateY(ref cube2.L_3D_Pts, -100);
                    Transformation_API.TranslateX(ref cube2.L_3D_Pts, startfgroundx);
                    cube2.Rotat_Aroun_Edge(1, 0, 45);

                    bool changecolor = false;

                    if (j <= 10)
                    {
                        cube2.typeofcube = 0;
                    }
                    else if (j > 10 && chanceornot == 0)
                    {
                        cube2.typeofcube = 0;
                    }
                    else if (j > 10 && hole == i && chanceornot == 1)
                    {
                        cube2.typeofcube = 1;
                    }
                    else if (j > 10 && hole == i && chanceornot == 2)
                    {
                        cube2.typeofcube = 2;
                        changecolor = true;
                        CPoint3D_Node forspikess = (CPoint3D_Node)cube2.L_3D_Pts[0];
                        addspikes(forspikess);
                    }

                    if (changecolor == false)
                    {
                        cube2.clr = cube2.clr = Color.DarkRed;
                    }
                    else
                    {
                        cube2.clr = Color.Purple;
                    }

                    startfgroundx += 50;
                    Ground.Add(cube2);
                }

                startgroundz += 30;
            }

            Movingcube.cam = cam;
            Movingcube.LoadFromFile("Cube2.txt");
            Movingcube.upordown = false;
            Transformation_API.Scale(ref Movingcube.L_3D_Pts, 0.2f, 0.2f, 0.2f);
            _3D_Model tempground = (_3D_Model)Ground[41];
            for (int i = 0; i < Movingcube.L_3D_Pts.Count; i++)
            {
                CPoint3D_Node temp = (CPoint3D_Node)Movingcube.L_3D_Pts[i];
                CPoint3D_Node tempground2 = (CPoint3D_Node)tempground.L_3D_Pts[i];
                temp.X = tempground2.X;
                temp.Z = tempground2.Z;
            }
            Transformation_API.TranslateY(ref Movingcube.L_3D_Pts, -70);

            Movingcube.clr = Color.Blue;

            cam.BuildNewSystem();

            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        }

        public void rot(int amount)
        {
            addnewrow();
            translatebackrow();
            rotatecounter = 0;


            rowmovement += amount;
            rowmovementcheckspike += amount;
            bow.Play();
            Movingcube.cam = cam;
            Movingcube.LoadFromFile("Cube2.txt");
            Transformation_API.Scale(ref Movingcube.L_3D_Pts, 0.2f, 0.2f, 0.2f);
            _3D_Model tempground2 = (_3D_Model)Ground[rowmovement];
            for (int i = 0; i < Movingcube.L_3D_Pts.Count; i++)
            {
                CPoint3D_Node temp = (CPoint3D_Node)Movingcube.L_3D_Pts[i];
                CPoint3D_Node tempground3 = (CPoint3D_Node)tempground2.L_3D_Pts[i];
                temp.X = tempground3.X;
                temp.Z = tempground3.Z;
            }
            Transformation_API.TranslateY(ref Movingcube.L_3D_Pts, -70);

            Movingcube.clr = Color.Blue;
            whichground = false;
        }

        void addspikes(CPoint3D_Node copy)
        {
            //////////////////////
            int theta = 0;
            CPoint3D_Node forspikes = copy;
            _3D_Model circle = new _3D_Model();
            circle.cam = cam;
            circle.clr = Color.Gold;
            circle.L_3D_Pts = new ArrayList();
            circle.L_Edges = new ArrayList();
            circle.upordown = false;

            CPoint3D_Node pnnn = new CPoint3D_Node((forspikes.X), (forspikes.Y - 10), (float)(forspikes.Z + 2 + 20));

            circle.L_3D_Pts.Add(pnnn);

            for (int c = 0; c < 36; c++)
            {
                float xcircle = (float)((forspikes.X) + (4 * Math.Cos(theta * Math.PI / 180)));
                float ycircle = (float)((forspikes.Y - 30) + (4 * Math.Sin(theta * Math.PI / 180)));
                float zcircle = forspikes.Z + 20;
    

                CPoint3D_Node pnn = new CPoint3D_Node(xcircle, ycircle, zcircle);


                circle.L_3D_Pts.Add(pnn);


                if (c > 0)
                {
                    Edge pn = new Edge(c, c - 1);
                    circle.L_Edges.Add(pn);
                }

                Edge pn2 = new Edge(0, c);
                circle.L_Edges.Add(pn2);

                theta += 10;
            }
            Spikes.Add(circle);
        }

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.Load += new EventHandler(Form1_Load);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            t.Tick += T_Tick;
            t.Interval = 50;
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < Spikes.Count; i++)
            {
                _3D_Model tempspike = (_3D_Model)Spikes[i];
                CPoint3D_Node tempspikepoint = (CPoint3D_Node)tempspike.L_3D_Pts[3];
                if(tempspikepoint.Z < - 350)
                {
                    Spikes.RemoveAt(i);
                }
            }

            CPoint3D_Node checkcube = (CPoint3D_Node)Movingcube.L_3D_Pts[3];
            _3D_Model checkcube2 = (_3D_Model)Ground[1];
            CPoint3D_Node checkcube22 = (CPoint3D_Node)checkcube2.L_3D_Pts[3];
            if (checkcube.Z < checkcube22.Z)
            {
                falling = true;
            }

            if (spikemove == 15 || spikemove == 30)
            {
                for (int i = 0; i < Spikes.Count; i++)
                {
                    _3D_Model tempspike = (_3D_Model)Spikes[i];
                    if (tempspike.upordown == false)
                    {
                        Transformation_API.TranslateY(ref tempspike.L_3D_Pts, 30);
                        tempspike.upordown = true;
                        
                    }
                    else if (tempspike.upordown == true)
                    {
                        Transformation_API.TranslateY(ref tempspike.L_3D_Pts, -30);
                        tempspike.upordown = false;
                    }
                }

                for (int i = 0; i < Ground.Count; i++)
                {
                    _3D_Model tempspike = (_3D_Model)Ground[i];
                    if (tempspike.typeofcube == 2)
                    {
                        if (tempspike.upordown == false)
                        {
                            tempspike.upordown = true;
                            _3D_Model checkhole = (_3D_Model)Ground[rowmovement];     
                             if (checkhole.typeofcube == 2 && checkhole.upordown == true)
                            {
                                crushed = true;
                            }

                        }
                        else if (tempspike.upordown == true)
                        {
                            tempspike.upordown = false;
                        }
                    }
                }
            }

            if (firstclick == true)
            {
                if (falling == false && crushed == false)
                {             
                     removeoldrow();
                    if (clickedrotate == true)
                    {
                        if (rotsided == true)
                        {
                            if (rotatecounter < 5)
                            {
                                Movingcube.Rotat_Aroun_Edge(10, 1, 15);
                            }
                            else
                            {
                                rot(6);
                                rotsided = false;
                                rotsidea = false;
                                clickedrotate = false;
                                rotatecounter = 0;
                                _3D_Model checkhole = (_3D_Model)Ground[rowmovement];
                                if (checkhole.typeofcube == 1)
                                {
                                    falling = true;
                                }
                                else if(checkhole.typeofcube == 2 && checkhole.upordown == true)
                                {
                                    crushed = true;
                                }
                            
                            }
                            rotatecounter++;
                        }
                        else if (rotsidea == true)
                        {
                            if (rotatecounter < 5)
                            {
                                Movingcube.Rotat_Aroun_Edge(6, 1, 15);
                            }
                            else
                            {
                                rot(5);
                                rotsided = false;
                                rotsidea = false;
                                clickedrotate = false;
                                rotatecounter = 0;
                                _3D_Model checkhole = (_3D_Model)Ground[rowmovement];
                                if (checkhole.typeofcube == 1)
                                {
                                    falling = true;
                                }
                                else if (checkhole.typeofcube == 2 && checkhole.upordown == true)
                                {
                                    crushed = true;
                                }

                            }
                            rotatecounter++;
                        }
                    }
                }
                else if (falling == true)
                {
                    if (fallingcubecounter < 20)
                    {
                        if(fallsound == false)
                        {
                            fall.Play();
                            fallsound = true;
                        }
                        Transformation_API.TranslateZ(ref Movingcube.L_3D_Pts, -20);
                    }
                    else
                    {
                        fall.Stop();
                        breaking.Play();
                        buildandreset();
                    }
                    fallingcubecounter++;
                }
                else if(crushed == true)
                {
                    if (fallingcubecounter < 20)
                    {
                        if (fallsound == false)
                        {
                            breaking.Play();
                            fallsound = true;
                        }
                        Transformation_API.Scale(ref Movingcube.L_3D_Pts, (float)0.7, (float)0.7, (float)0.7);
                    }
                    else
                    {
                        breaking.Stop();
                        buildandreset();
                    }
                    fallingcubecounter++;
                }
            }

            spikemove++;
            if(spikemove == 31)
            {
                spikemove = 0;
            }
            cam.BuildNewSystem();
            DrawScene(this.CreateGraphics());
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (falling == false && crushed == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        cam.cop.Z++;
                        break;
                    case Keys.Down:
                        cam.cop.Z--;
                        break;

                    case Keys.Right:
                        cam.cop.X++;
                        break;
                    case Keys.Left:
                        cam.cop.X--;
                        break;

                    case Keys.Y:
                        cam.cop.Y++;
                        break;
                    case Keys.H:
                        cam.cop.Y--;
                        break;
                    case Keys.A:
                        if (firstclick == false)
                        {
                            firstclick = true;
                        }

                        if (clickedrotate == false)
                        {
                            _3D_Model checkwall = (_3D_Model)Ground[rowmovement + 5];
                            if (checkwall.typeofcube != 3)
                            {
                                clickedrotate = true;
                                rotsidea = true;
                            }
                        }
                        break;

                    case Keys.D:

                        if (firstclick == false)
                        {
                            firstclick = true;
                        }

                        if (clickedrotate == false)
                        {
                            _3D_Model checkwall = (_3D_Model)Ground[rowmovement + 6];
                            if (checkwall.typeofcube != 3)
                            {
                                clickedrotate = true;
                                rotsided = true;
                            }
                        }
                        break;
                }
            }
        }

        void Form1_Load(object sender, EventArgs e)
        {
            buildandreset();
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawScene(e.Graphics);
        }

        void DrawScene(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
          
            g2.Clear(Color.Black);
            

            //Cube.DrawYourSelf(g2, 0);
            for(int i = 0; i < Ground.Count; i++)
            {
                _3D_Model temp = (_3D_Model)Ground[i];
                temp.DrawYourSelf(g2, 0);
            }


            for (int i = 0; i < Spikes.Count; i++)
            {
                _3D_Model temp = (_3D_Model)Spikes[i];
                temp.DrawYourSelf(g2, 0);
            }

            Movingcube.DrawYourSelf(g2, 1);

            g.DrawImage(off, 0, 0);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
