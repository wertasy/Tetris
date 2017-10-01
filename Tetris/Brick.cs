using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class Brick
    {
        public int[,] Mod;
        public Point Pos;
        public int[,] tMod;
        public Point tPos;
        public Color col;
        public bool canChange = true;
        public Brick()
        {
            col = Color.FromArgb(255, Game.random.Next(255), 255 - Game.random.Next(255), 255 - Game.random.Next(255));
            Mod = new int[5, 5];
            Pos = new Point();
            tMod = new int[5, 5];
            tPos = new Point();
            MoveTestMod();
        }
        public Brick(int i = 0)
        {
            col = Color.FromArgb(255, Game.random.Next(255), 255 - Game.random.Next(255), 255 - Game.random.Next(255));
            switch (i)
            {
                case 0:
                    Mod = new int[5, 5]
            {
                   { 0,0,0,0,0},
                   { 0,0,0,0,0},
                   { 1,1,1,1,0},
                   { 0,0,0,0,0},
                   { 0,0,0,0,0},
            }; break;
                case 1:
                    Mod = new int[5, 5]
           {
                   { 0,0,0,0,0},
                   { 0,0,1,1,0},
                   { 0,0,1,1,0},
                   { 0,0,0,0,0},
                   { 0,0,0,0,0},
           }; break;
                case 2:
                    Mod = new int[5, 5]
           {
                   { 0,0,0,0,0},
                   { 0,0,1,0,0},
                   { 0,1,1,1,0},
                   { 0,0,0,0,0},
                   { 0,0,0,0,0},
           }; break;
                case 3:
                    Mod = new int[5, 5]
           {
                   { 0,0,0,0,0},
                   { 0,0,0,0,0},
                   { 0,1,1,0,0},
                   { 0,0,1,1,0},
                   { 0,0,0,0,0},
           }; break;
                case 4:
                    Mod = new int[5, 5]
           {
                   { 0,0,0,0,0},
                   { 0,0,1,1,0},
                   { 0,1,1,0,0},
                   { 0,0,0,0,0},
                   { 0,0,0,0,0},
           }; break;
                case 5:
                    Mod = new int[5, 5]
           {
                   { 0,0,0,0,0},
                   { 0,1,1,0,0},
                   { 0,0,1,0,0},
                   { 0,0,1,0,0},
                   { 0,0,0,0,0},
           }; break;
                case 6:
                    Mod = new int[5, 5]
           {
                   { 0,0,0,0,0},
                   { 0,0,1,1,0},
                   { 0,0,1,0,0},
                   { 0,0,1,0,0},
                   { 0,0,0,0,0},

           }; break;
                default://不可能出现的，除非有Bug
                    Mod = new int[5, 5]
      {
                   { 0,0,0,0,0},
                   { 0,0,1,0,0},
                   { 0,1,1,1,0},
                   { 0,0,1,0,0},
                   { 0,0,0,0,0},
       };break;
            }
            Pos = new Point();
            tMod = new int[5, 5];
            tPos = new Point();
        }
        public void RotateTestMod()//Mod旋转后输出到tMod
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    tMod[j, 5 - i - 1] = Mod[i, j];
        }
        public void MoveTestMod()//Mod,Pos输出到tMod,tPos
        {
            InitialtPos();
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    tMod[i, j] = Mod[i, j];
        }
        public void ChangePos()
        {
            Pos.X = tPos.X;
            Pos.Y = tPos.Y;
        }
        public void InitialtPos()//Pos输出到tPos
        {
            tPos.X = Pos.X;
            tPos.Y = Pos.Y;
        }
        public void ChangeMod()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    Mod[i, j] = tMod[i, j];
        }
        public void MoveTestLeft()
        {
            tPos.Offset(-1, 0);
        }
        public void MoveTestRight()
        {
            tPos.Offset(1, 0);
        }
        public void MoveTestUp()
        {
            tPos.Offset(0, -1);
        }
        public void MoveTestDown()
        {
            tPos.Offset(0, 1);
        }
    }
}
