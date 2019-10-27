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
    public partial class CMainmenu : Form
    {
        private bool _Ai_White;
        private string _Name_Player_White;
        private string _Name_Player_Black;
        public CMainmenu()
        {
            InitializeComponent();
            richTextBox1.Text = "Qu’est - ce que c’est ? ";
            richTextBox1.Text += "\n\n";
            richTextBox1.Text += "-Le bailli est un jeu du moyenne Age, dans ancien temps, ce jeu est joué par des rois. Ce jeu est un jeu stratégique plus simple que l’échecs, mais il reste quand même intéressant grâce à son côté compétitif. Ce programme est le premier concept de ce jeu qui existe sur ordinateur.";
            richTextBox1.Text += "\n\n";
            richTextBox1.Text += "Comment joue-t-on ce jeu ?";
            richTextBox1.Text += "\n\n";
            richTextBox1.Text += "-La règle du jeu :";
            richTextBox1.Text += "\n\n";
            richTextBox1.Text += "-Les pièces : il y a 2 types de pièces sur un damier de 5x5:\n";
            richTextBox1.Text += "\n\n";
            richTextBox1.Text += "1.Les pions : chaque pion a le droit de déplacer horizontalement, verticalement ou diagonalement. Chaque équipe possède 5 pions de différents couleurs soit le Noir ou le Blanc \n";
            richTextBox1.Text += "\n\n";
            richTextBox1.Text += "2.Le roi: La pièce unique du damier et il a même droit de déplacer que les pions.\n";
            richTextBox1.Text += "\n\n";
            richTextBox1.Text += "- A chaque tour ; le joueur doit déplacer le roi d’abord puis un pion de son côté après. Les pions servent à empêcher les déplacements du roi. Le jeu sera terminé quand un joueur arrive à amener le Roi à son coté, donc il sera gagné.\n";
            richTextBox1.Text += "\n\n";
            richTextBox1.Text += "- le roi n’a plus droit de déplacer, le match sera nul dans ce cas.";




            richTextBox1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Ai_White = Get_Choice();
            CGraphiqueSolo Running = new CGraphiqueSolo(Ai_White);
            Running.Show();
        }
        public bool Ai_White
        {
            get { return _Ai_White; }
        }

        public bool Get_Choice()
        {
            if (radioButton1.Checked == true) _Ai_White = false;
            if (radioButton2.Checked == true) _Ai_White = true;
            return _Ai_White;
             
        }
        private void button2_Click(object sender, EventArgs e)
        {
            _Name_Player_White = textBox1.Text.ToString();
            _Name_Player_Black = textBox2.Text.ToString();
            CGraphique2 Running = new CGraphique2(_Name_Player_Black,_Name_Player_White);
            Running.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
