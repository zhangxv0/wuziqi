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
    public partial class GameSteps : Form
    {
        List<List<int>> gamestep;
        public GameSteps(List<List<int>> gamestep)
        {
            InitializeComponent();
            this.gamestep = gamestep;

        }

        private void GameSteps_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < gamestep.Count-1; i=i+2)
            {
                int num = i + 1;
                //黑方
                this.textBox1.Text += ("("+num.ToString()+ ")"+ gamestep[i][0].ToString()+" ,"+ gamestep[i][1].ToString()+"\r\n");
                //白方
                this.textBox2.Text += ("(" + (num+1).ToString() + ")" + gamestep[i+1][0].ToString() + " ," + gamestep[i+1][1].ToString() + "\r\n");
            }
        }
    }
}
