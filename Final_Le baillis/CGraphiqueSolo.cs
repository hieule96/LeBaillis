using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Le_baillis
{
    public partial class CGraphiqueSolo : Form
    {
        public CGraphiqueSolo(bool AI_White)
        {
            InitializeComponent();
            Game_Setting(AI_White);
            cBoardSolo1.Parent = this;
        }
        public void majlabel(string txt)
        {
            label1.Text = txt;
        }
        public void debug_label1(string txt)
        {
            label2.Text = txt;
        }
        public void debug_label2(string txt)
        {
            label3.Text = txt;
        }
        public void Game_Setting(bool AI_White)
        {
            cBoardSolo1.AI_White = AI_White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
