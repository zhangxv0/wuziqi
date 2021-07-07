using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wuziqi
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        const int num_che = 20;
        int[,] ChessBack = new int[num_che, num_che];
        //双方轮换,true是红，false是蓝
        static bool type;
        //是否开始
        static bool start;
        public Form1()
        {
            InitializeComponent();
            this.Width = MainSize.Wid;
            this.Height = MainSize.Hei;
            this.pictureBox1.Width = MainSize.CBWid;
            this.pictureBox1.Height = MainSize.CBHei;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeThis();
        }
        private void InitializeThis()
        {
            for (int i = 0; i < num_che; i++)
            {
                for (int j = 0; j < num_che; j++)
                    ChessBack[i, j] = 0;
            }
            start = false;
            this.label1.Text = "游戏尚未开始";
            ChessBoard.DrawCB(graphics, this.pictureBox1);
            type = true;
            //btnStart.Enabled = true;
            //btnReset.Enabled = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (start)
            {
                //在计算矩阵中的位置
                int bX = (int)((e.X + MainSize.CBGap / 2) / MainSize.CBGap);
                int bY = (int)((e.Y + MainSize.CBGap / 2) / MainSize.CBGap);
                //防止在同一个位置落子
                if (ChessBack[bX, bY] != 0)
                    return;
                Chess.DrawChess(type, pictureBox1, graphics, e);
                ChessBack[bX, bY] = type ? 1 : 2;
                //判断棋盘是否满了

                if (IsFull() && !Victory(bX, bY))
                {
                    if (MessageBox.Show("游戏结束，平局") == DialogResult.OK)
                        InitializeThis();
                    return;
                }
                //判断胜利
                if (Victory(bX, bY))
                {
                    string Vic = type ? "黑" : "白";
                    if (MessageBox.Show(Vic + "方胜利!") == DialogResult.OK)
                        InitializeThis();
                    return;
                }

                //换人
                type = !type;
                label1.Text = type ? "黑方's trun!" : "白方's turn!";
            }
            else {
                MessageBox.Show("游戏尚未开始");
                return; }
        }
        /// <summary>
        /// 判断棋盘是否满
        /// </summary>
        /// <returns></returns>
        private bool IsFull()
        {
            bool full = true;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (ChessBack[i, j] == 0)
                        full = false;
                }
            }
            return full;
        }
        #region 判断胜利
        private bool Victory(int bx, int by)
        {
            if (HorVic(bx, by))
                return true;
            if (VerVic(bx, by))
                return true;
            if (Vic45(bx, by))
                return true;
            else
                return false;
        }

        private bool Vic45(int bx, int by)
        {

            int b1 = (bx - 4) > 0 ? bx - 4 : 0;
            int b2 = (by - 4) > 0 ? by - 4 : 0;
            //int buttom = b1 > b2 ? b2 : b1;
            int val = ChessBack[bx, by];
            for (int i = b1, j = b2; i < 16 && j < 16; i++, j++)
            {
                if (ChessBack[i, j] == val && ChessBack[i + 1, j + 1] == val &&
                    ChessBack[i + 2, j + 2] == val && ChessBack[i + 3, j + 3] == val
                    && ChessBack[i + 4, j + 4] == val)
                    return true;
            }
            for (int i = b1, j = b2; i < 16 && j < 16; i++, j++)
            {
                if (ChessBack[i, j] == val && ChessBack[i + 1, j - 1] == val &&
                    ChessBack[i + 2, j - 2] == val && ChessBack[i + 3, j - 3] == val
                    && ChessBack[i - 4, j - 4] == val)
                    return true;
            }
            return false;
        }

        private bool VerVic(int bx, int by)
        {
            int buttom = (by - 4) > 0 ? by - 4 : 0;
            int val = ChessBack[bx, by];
            for (int i = buttom; i < 16; i++)
            {
                if (ChessBack[bx, i] == val && ChessBack[bx, i + 1] == val &&
                    ChessBack[bx, i + 2] == val && ChessBack[bx, i + 3] == val
                    && ChessBack[bx, i + 4] == val)
                    return true;
            }
            return false;
        }

        private bool HorVic(int bx, int by)
        {
            int left = (bx - 4) > 0 ? bx - 4 : 0;
            int val = ChessBack[bx, by];
            for (int i = left; i < 16; i++)
            {
                if (ChessBack[i, by] == val && ChessBack[i + 1, by] == val &&
                    ChessBack[i + 2, by] == val && ChessBack[i + 3, by] == val
                    && ChessBack[i + 4, by] == val)
                    return true;
            }
            return false;
        }

        #endregion

        private void 开始游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            start = true;
            this.label1.Text = "开始游戏";
            MessageBox.Show("游戏开始!!!!!");
        }
        Color color = Color.Black;
        private void 设置棋盘颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!start)
            {
                Color color;
                DialogResult dialogResult = colorDialog1.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    color = colorDialog1.Color;
                    ChessBoard.Color1 = color;
                    ChessBoard.DrawCB(graphics, this.pictureBox1);
                }
            }
            else
            {
                MessageBox.Show("只有在游戏没有开始时才能设置棋盘颜色");
                return;
            }
            

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }





    /// <summary>
    /// 棋盘类
    /// </summary>
    class ChessBoard
    {

        static Color color = Color.Black;
        static readonly float penWid = 1.0f;
        static readonly Pen pen = new Pen(color, penWid);
        static public Color Color1 = Color.White;

        public static void DrawCB(Graphics gra, PictureBox pic)
        {
            //每排数量
            int horC = MainSize.CBWid / MainSize.CBGap;
            //间隔
            int gap = MainSize.CBGap;
            Image img = new Bitmap(MainSize.CBWid, MainSize.CBHei);
            gra = Graphics.FromImage(img);
            gra.Clear(Color1);
            gra.DrawRectangle(pen, 0, 0, MainSize.CBWid, MainSize.CBHei);
            //画棋盘
            for (int i = 0; i < horC; i++)
            {
                gra.DrawLine(pen, 0, i * gap, MainSize.CBWid, i * gap);
                gra.DrawLine(pen, i * gap, 0, i * gap, MainSize.CBHei);
            }
            //pic.BackColor = Color.Red;
            gra.DrawLine(pen, 0, horC * gap, MainSize.CBWid, horC * gap - 1);
            gra.DrawLine(pen, horC * gap - 1, 0, horC * gap, MainSize.CBHei);
            pic.Image = img;
        }

    }
    /// <summary>
    /// 期盼的基本参数
    /// </summary>
    class MainSize
    {
        //主框体大小
        public static int Wid { get { return 520; } }
        public static int Hei { get { return 460; } }
        //棋盘大小
        public static int CBWid { get { return 401; } }
        public static int CBHei { get { return 401; } }
        //棋盘宽度
        public static int CBGap { get { return 20; } }
        //棋子直径
        public static int ChessRadious { get { return 16; } }
    }
    /// <summary>
    /// 棋子类
    /// </summary>
    class Chess
    {
        public static void DrawChess(bool type, PictureBox pic, Graphics graphic, MouseEventArgs e)
        {
            graphic = pic.CreateGraphics();
            Pen pen1 = new Pen(Color.Black, 1);
            Brush bru1 = new SolidBrush(Color.Black);
            Pen pen2 = new Pen(Color.Black, 1);
            Brush bru2 = new SolidBrush(Color.White);
            int newX = (int)((e.X + MainSize.CBGap / 2) / MainSize.CBGap) * MainSize.CBGap - MainSize.ChessRadious / 2;
            int newY = (int)((e.Y + MainSize.CBGap / 2) / MainSize.CBGap) * MainSize.CBGap - MainSize.ChessRadious / 2;
            if (type)
            {
                graphic.DrawEllipse(pen1, newX, newY, MainSize.ChessRadious, MainSize.ChessRadious);
                graphic.FillEllipse(bru1, newX, newY, MainSize.ChessRadious, MainSize.ChessRadious);
            }
            if (!type)
            {
                graphic.DrawEllipse(pen2, newX, newY, MainSize.ChessRadious, MainSize.ChessRadious);
                graphic.FillEllipse(bru2, newX, newY, MainSize.ChessRadious, MainSize.ChessRadious);
            }
            graphic.Dispose();
        }

    }
}
