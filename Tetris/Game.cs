using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    //游戏的核心算法
    //进行游戏的逻辑更新
    public enum Towards { UP = 0, RIGHT, DOWN, LETF }; //方向
    class Game
    {
        public Map map;
        public Brick curBrick;
        public Brick nextBrick;
        public Timer speed; //更新速度
        public static Random random = new Random(); //整个游戏只用random来产生随机数，避免出现同时调用多个随机数生成器由相同或相近的时间种子获得相同的随机数的情况
        public int score = 0;
        public Game()
        {
            map = new Map(10,15);
            CreateBrick();
            nextBrick = new Brick(random.Next(7));

            speed = new Timer();
            speed.Tick += new EventHandler(speed_Tick);
            speed.Interval = 1000;
            speed.Enabled = true;

            System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
            sp.SoundLocation = @".\Sound.wav";
            if (System.IO.File.Exists(@".\Sound.wav")) sp.PlayLooping();
        }
        private void speed_Tick(object sender, EventArgs e)
        {
            if (!Drop())
            {
                PutBrick2Map();
                ClearLine();
                if (!NewFall())
                {
                    speed.Enabled = false;
                    MessageBox.Show("GAME OVER", "提示");
                    //Application.Exit();
                }
            }
        }
        public void Pause()
        {
            if (speed.Enabled == true)
                speed.Enabled = false;
            else
                speed.Enabled = true;
        }
        public bool Intersect() //相交测试
        {
            bool flag = false;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (curBrick.tMod[i, j] + map.Mod[curBrick.tPos.Y + i, curBrick.tPos.X + j].p == 2)
                        flag = true;
            return flag;
        }
        public void PutBrick2Map()//将当前方块放入地图
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (curBrick.Mod[i, j] == 1)
                    {
                        map.Mod[i + curBrick.Pos.Y, j + curBrick.Pos.X].p = curBrick.Mod[i, j];
                        map.Mod[i + curBrick.Pos.Y, j + curBrick.Pos.X].c = curBrick.col;
                    }
        }
        public void Rotate()//旋转
        {
            curBrick.RotateTestMod();//旋转测试模式
            if(!Intersect())
            {
                curBrick.ChangeMod();
            }
        }
        public void CreateBrick()
        {
            curBrick = new Brick(random.Next(7));
            curBrick.Pos.X = (map.Width + 8 / 2) - 8;
            curBrick.Pos.Y = 2;
        }
        public bool NewFall()//新的方块下落
        {
            curBrick = nextBrick;
            curBrick.Pos.X = (map.Width + 8 / 2) - 8;
            curBrick.Pos.Y = 1;
            nextBrick = new Brick(random.Next(7));
            curBrick.MoveTestMod();//移动测试模式
            return !Intersect();
        }
        public bool Drop()
        {
            bool flag = false;
            curBrick.MoveTestMod();
            curBrick.MoveTestDown();
            if (!Intersect())
            {
                flag = true;
                curBrick.ChangePos();//改变位置
            }
            else
            {
                curBrick.InitialtPos();
            }
            return flag;
        }
        public void Move(Towards dir)//移动方块,返回值
        {
            curBrick.MoveTestMod();
            if (dir == Towards.LETF)
                curBrick.MoveTestLeft();
            if (dir == Towards.RIGHT)
                curBrick.MoveTestRight();
            if (dir == Towards.UP)//呵呵
                curBrick.MoveTestUp();
            if (dir == Towards.DOWN)
                curBrick.MoveTestDown();
            if (!Intersect())
            {
                curBrick.ChangePos();//改变位置
            }
            else
            {
                curBrick.InitialtPos();
            }
        }
        public void ClearLine()
        {
            //清除掉满行
            //这个函数实际上效率也很低的,为了简便它从头到尾作了测试
            //具体的算法为:
            //从游戏区域第0行开始到最后一行,测试地图点阵是否为满,如果是的话
            //从当前行算起,之上的地图向下掉一行
            int i, dx, dy;
            bool fullflag;
            for (i = 4; i < map.Height + 4; i++)//最后一行保留行
            {
                fullflag = true;//假设为满
                for (int j = 4; j < map.Width + 4; j++)//保留列
                { 
                    if (map.Mod[i,j].p == 0)
                    {
                        fullflag = false;
                        break;
                    }
                }//找出第i行为满
                if (fullflag)
                { 
                    score += 10;
                    for (dy = i; dy > 0; dy--)
                        for (dx = 4; dx < map.Width + 4; dx++)
                            map.Mod[dy, dx] = map.Mod[dy - 1, dx];//向下移动一行
                    for (dx = 4; dx < map.Width + 4; dx++)
                        map.Mod[0, dx].p = 0;//并清除掉第一行
                }
            }
        }
    }
}
