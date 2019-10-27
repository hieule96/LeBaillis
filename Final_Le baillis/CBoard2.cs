using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Le_baillis
{
    public partial class CBoard2 : UserControl
    {
        #region Variable de la classe
        private int W = 500; // Dimension de Damier
        private int H = 500;
        private int Case_x = 100; // Dimension de chaque cqse
        private int Case_y = 100;
        private CCartographie m = new CCartographie();
        private Point _Mouse_Position; // case selectione
        private Point _Mouse_Position_Select;
        private Point _Mouse_Position_Depart;
        private Pen _hightlight_pen_Blue;
        private Pen _hightlight_pen_Red;
        private Pen _hightlight_pen_Green;
        private bool _highlight;
        private bool _WhiteTurn;
        private bool _KingMoved;
        private bool _GameState; // etat de jeux
        #endregion

        #region Configuration des outil/ Constructeur par defaut
        public CBoard2()
        {
            _KingMoved = false;
            _WhiteTurn = true;
            _highlight = false;
            _GameState = true;
            _Mouse_Position = new Point();
            _Mouse_Position_Select = new Point();
            _hightlight_pen_Blue = new Pen(Color.Blue, 5);
            _hightlight_pen_Red = new Pen(Color.Red, 5);
            _hightlight_pen_Green = new Pen(Color.Green, 5);
            m.Initilisation();
            InitializeComponent();
        }
        #endregion

        private void CBoard2_Load(object sender, EventArgs e)
        {
            ((CGraphique2)(this.Parent)).majlabel("White Turn first !");
        }

        private void CBoard2_Paint(object sender, PaintEventArgs e)
        {
            draw();
        }



        private void draw()
        {

            Graphics graphies = this.CreateGraphics();
            graphies.Clear(Color.White);
            graphies.DrawImage(Final_Le_baillis.Properties.Resources.table, 0, 0, W, H);
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    int PieceType = m.Return_information(x, y);
                    if (PieceType == 1)
                    {
                        graphies.DrawImage(Final_Le_baillis.Properties.Resources.WQueen, x + x * Case_x, y + y * Case_y, Case_x, Case_y);
                    }
                    if (PieceType == 3)
                    {
                        graphies.DrawImage(Final_Le_baillis.Properties.Resources.WPawn, x + x * Case_x, y + y * Case_y, Case_x, Case_y);
                    }
                    if (PieceType == 2)
                    {
                        graphies.DrawImage(Final_Le_baillis.Properties.Resources.BPawn, x + x * Case_x, y + y * Case_y, Case_x, Case_y);
                    }
                }
            }



            if (_highlight == true)
            {
                if (m.Return_information(_Mouse_Position.X, _Mouse_Position.Y) == 1)
                {
                    bool[,] position_possible = new bool[5, 5];
                    position_possible = m.Position_Mouvement(1, _Mouse_Position.X, _Mouse_Position.Y);
                    Rectangle rectangle_highlight = new Rectangle(_Mouse_Position.X * Case_x, _Mouse_Position.Y * Case_y, Case_x, Case_y);
                    graphies.DrawRectangle(_hightlight_pen_Red, rectangle_highlight);
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (position_possible[i, j])
                            {
                                Rectangle rectangle_highlight_possible = new Rectangle(i * Case_x, j * Case_y, Case_x, Case_y);
                                graphies.DrawRectangle(_hightlight_pen_Red, rectangle_highlight_possible);
                            }
                        }
                    }
                }
                if (m.Return_information(_Mouse_Position.X, _Mouse_Position.Y) == 2)
                {
                    bool[,] position_possible = new bool[5, 5];
                    position_possible = m.Position_Mouvement(2, _Mouse_Position.X, _Mouse_Position.Y);
                    Rectangle rectangle_highlight = new Rectangle(_Mouse_Position.X * Case_x, _Mouse_Position.Y * Case_y, Case_x, Case_y);
                    graphies.DrawRectangle(_hightlight_pen_Blue, rectangle_highlight);
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (position_possible[i, j])
                            {
                                Rectangle rectangle_highlight_possible = new Rectangle(i * Case_x, j * Case_y, Case_x, Case_y);
                                graphies.DrawRectangle(_hightlight_pen_Blue, rectangle_highlight_possible);
                            }
                        }
                    }


                }
                if (m.Return_information(_Mouse_Position.X, _Mouse_Position.Y) == 3)
                {
                    bool[,] position_possible = new bool[5, 5];
                    position_possible = m.Position_Mouvement(3, _Mouse_Position.X, _Mouse_Position.Y);
                    Rectangle rectangle_highlight = new Rectangle(_Mouse_Position.X * Case_x, _Mouse_Position.Y * Case_y, Case_x, Case_y);
                    graphies.DrawRectangle(_hightlight_pen_Green, rectangle_highlight);
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (position_possible[i, j])
                            {
                                Rectangle rectangle_highlight_possible = new Rectangle(i * Case_x, j * Case_y, Case_x, Case_y);
                                graphies.DrawRectangle(_hightlight_pen_Green, rectangle_highlight_possible);
                            }
                        }
                    }
                }


            }
        }



        private void CBoard2_MouseClick(object sender, MouseEventArgs e)
        {
            if (_GameState)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (e.X > x * Case_x)
                        {
                            _Mouse_Position.X = x; //convertir la position de la souris en position de damier
                        }
                    }
                    for (int y = 0; y < 5; y++)
                    {
                        if (e.Y > y * Case_y)
                        {
                            _Mouse_Position.Y = y;//convertir la position de la souris en position de damier
                        }
                    }
                    int Piece_Type = m.Return_information(_Mouse_Position.X, _Mouse_Position.Y); //retourner le type selectioné.

                    if (m.Return_information(_Mouse_Position.X, _Mouse_Position.Y) != 0) // si rien est choisi la position départ n'est pas affecté
                    {
                        _Mouse_Position_Depart.X = _Mouse_Position.X;
                        _Mouse_Position_Depart.Y = _Mouse_Position.Y;
                        _highlight = true;
                    }
                    if (m.Return_information(_Mouse_Position.X, _Mouse_Position.Y) == 0) // si la position d'arrivé est sur la case null, affecté à la variable destiation.
                    {
                        _Mouse_Position_Select.X = _Mouse_Position.X;
                        _Mouse_Position_Select.Y = _Mouse_Position.Y;
                        _highlight = false;
                        move(m.Return_information(_Mouse_Position_Depart.X, _Mouse_Position_Depart.Y)); // cette fonction reçoit la type de pièce avec sa position de départ effectue le mouvement
                        if (m.Game_state() == 2)
                        {
                            MessageBox.Show("Black Win !!!!, Game Over");
                            _GameState = false;
                        }
                        if (m.Game_state() == 3)
                        {
                            MessageBox.Show("White Win !!!!, Game Over ");
                            _GameState = false;
                        }
                    }
                    draw();
                }
            }


        }


        private void move(int Piece_Type) // cette fonction vérifie si le déplacement est valide en basant sur la position de départ et d'arrivée.
        {
            if (m.Return_information(_Mouse_Position_Depart.X, _Mouse_Position_Depart.Y) != 0) // Si la position de départ est un pièce
            {
                bool[,] Position_Possible = new bool[5, 5];
                Position_Possible = m.Position_Mouvement(Piece_Type, _Mouse_Position_Depart.X, _Mouse_Position_Depart.Y);
                if (Position_Possible[_Mouse_Position_Select.X, _Mouse_Position_Select.Y]) // Vérifier si la position d'arrivé est bien valide
                {
                    if (_KingMoved == false) // si on n'a pas bougé le roi, il faut bouger d'abord
                    {
                        if (Piece_Type == 1) // forcer de choisi le riu
                        {
                            m.Update_position(Piece_Type, _Mouse_Position_Depart.X, _Mouse_Position_Depart.Y, _Mouse_Position_Select.X, _Mouse_Position_Select.Y);
                            _KingMoved = true;
                        }
                        if (_WhiteTurn)
                        {
                            ((CGraphique2)(this.Parent)).majlabel("White Turn: Move your King first");
                        }
                        if (!_WhiteTurn)
                        {
                            ((CGraphique2)(this.Parent)).majlabel("Black Turn: Move your King first");
                        }

                    }
                    if (_KingMoved == true && _WhiteTurn == true) // forcer de bouger le pions blanc
                    {
                        if (Piece_Type == 3)
                        {
                            m.Update_position(Piece_Type, _Mouse_Position_Depart.X, _Mouse_Position_Depart.Y, _Mouse_Position_Select.X, _Mouse_Position_Select.Y);
                            ((CGraphique2)(this.Parent)).majlabel("White Moved, Black Turn");
                            _WhiteTurn = false;
                            _KingMoved = false;
                        }
                        if (Piece_Type != 3)
                        {
                            ((CGraphique2)(this.Parent)).majlabel("White Turn: You move your White Pawn");
                        }
                    }
                    if (_KingMoved == true && _WhiteTurn == false) // forcer de bouger le pions noir
                    {
                        if (Piece_Type == 2)
                        {
                            m.Update_position(Piece_Type, _Mouse_Position_Depart.X, _Mouse_Position_Depart.Y, _Mouse_Position_Select.X, _Mouse_Position_Select.Y);
                            ((CGraphique2)(this.Parent)).majlabel("White Moved, Black Turn");
                            _WhiteTurn = true;
                            _KingMoved = false;
                        }
                        if (Piece_Type != 2)
                        {
                            ((CGraphique2)(this.Parent)).majlabel("Black Turn: You move your Black Pawn");
                        }
                     }
                        }
                    }
                }
   }
}
