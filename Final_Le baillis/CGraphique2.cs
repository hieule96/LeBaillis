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
    public partial class CGraphique2 : Form
    {

        public CGraphique2(string Name_Player_1, string Name_Player_2)
        {
            InitializeComponent();
            label2.Text = Name_Player_1;
            label3.Text = Name_Player_2;            
            cBoard21.Parent = this;
        }

        public void majlabel(string txt)
        {
            label1.Text = txt;
        }
        private void Exit_button_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
