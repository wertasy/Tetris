using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    //界面、游戏画面绘制和游戏操作输入
    public partial class Form1 : Form
    {
        Timer fresh; //画面刷新计时
        Game game;
        //Graphics gra;

        int map_cell_width;
        int map_cell_height;

        public Form1()
        {
            InitializeComponent();
            Text = "俄罗斯方块";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.BackColor = Color.Black;
            //gra = pictureBox1.CreateGraphics();
            game = new Game();

            map_cell_width = 30;
            map_cell_height = 30;

            KeyDown += new KeyEventHandler(Form1_KeyDown);
            fresh = new Timer();
            fresh.Tick += new System.EventHandler(fresh_Tick);

            fresh.Interval = 50 / 3;//60帧
            fresh.Enabled = true;
        }
        public void NewGame()
        {
            game = new Game();
        }
        private void fresh_Tick(object sender, EventArgs e)
        {
            Draw();
        }
        
        private void Draw() //游戏画面绘制
        {
            //初始化画布
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            SolidBrush s;

            g.DrawRectangle(Pens.White, map_cell_width * 4 - 3 - 80, map_cell_height * 4 - 3 - 80, map_cell_width * game.map.Width + 3, map_cell_height * game.map.Height + 3);
            //画地图
            for (int i = 4; i < game.map.Height + 4; i++) //(int i = 0; i < game.map.Height + 8; i++)
            {
                for (int j = 4; j < game.map.Width + 4; j++)
                {
                    if (game.map.Mod[i, j].p == 1)
                    {
                        s = new SolidBrush(game.map.Mod[i, j].c);
                        g.FillRectangle(s, map_cell_width * j - 80, map_cell_height * i - 80, map_cell_width - 2, map_cell_height - 2);
                    }
                }
            }
            s = new SolidBrush(game.curBrick.col);
            //画方块
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (game.curBrick.Mod[i, j] == 1)
                    {
                        if(i + game.curBrick.Pos.Y > 3)
                        {
                            //(i,j)方块内的点阵位置 Pos是方块左上角点的地图位置
                            g.FillRectangle(s, map_cell_width * (j + game.curBrick.Pos.X) - 80, map_cell_height * (i + game.curBrick.Pos.Y) - 80, map_cell_width - 2, map_cell_height - 2);
                        }
                        
                    }
                }
            }
            //画下一个
            s = new SolidBrush(game.nextBrick.col);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (game.nextBrick.Mod[i, j] == 1)
                    {
                        g.FillRectangle(s, map_cell_width * j + 380, map_cell_height * i + 50, map_cell_width - 2, map_cell_height - 2);
                    }
                }
            }
            //绘制文字
            g.DrawString("NEXT：", Font, Brushes.White, 380, 40);
            g.DrawString("游戏说明：\n\n   通过方向键控制砖块。\n   向上键：变形\n   向左键：左移\n   向下键：下移\n   向右键：右移\n\n当前分数：", Font, Brushes.White, 380, 200);
            Font f = new Font("微软雅黑", 32, Font.Style | FontStyle.Bold);
            g.DrawString(game.score.ToString(), f, Brushes.Snow, 420, 340);
            pictureBox1.BackgroundImage = bmp;
            pictureBox1.Refresh();
        }
        //按键部分
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (game.speed.Enabled == true)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            if (key == Keys.Up)
            {
                pictureBox1.Refresh();
                //game.Move(Towards.UP);
                game.Rotate();
                Draw();
            }
            if (key == Keys.Left)
            {
                pictureBox1.Refresh();
                game.Move(Towards.LETF);
                Draw();
            }
            if (key == Keys.Right)
            {
                pictureBox1.Refresh();
                game.Move(Towards.RIGHT);
                Draw();
            }
            if (key == Keys.Down)
            {
                pictureBox1.Refresh();
                game.Move(Towards.DOWN);
                Draw();
            }
            if(key==Keys.Space)
            {
                game.Rotate();
            }
            if(key==Keys.P)
            {
                game.Pause();
            }
        }
    }
}
