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
    public partial class CBoardSolo : UserControl
    {
        #region Variable de la classe
        private int W = 500;
        private int H = 500;
        private int Case_x = 100;
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
        private bool _KingWhiteMoved;
        private bool _KingBlackMoved;
        private bool _GameState; // etat de jeux
        private bool _AI_White;
        #endregion

        #region Configuration des outil/ Constructeur par defaut
        public CBoardSolo()
        {
            _AI_White = false;
            _KingWhiteMoved = false;
            _KingBlackMoved = true;
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
        public bool AI_White
        {
            set { _AI_White = value; }
            get { return _AI_White; }
        }

        #endregion

        private void CBoardSolo_Load(object sender, EventArgs e)
        {
            ((CGraphiqueSolo)(this.Parent)).majlabel("White Turn first !");
            AI_Move();
        }

        private void CBoardSolo_Paint(object sender, PaintEventArgs e)
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

        private void CBoardSolo_MouseClick(object sender, MouseEventArgs e)
        {
            if (_GameState)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (e.X > x * Case_x)
                        {
                            _Mouse_Position.X = x;
                        }
                    }
                    for (int y = 0; y < 5; y++)
                    {
                        if (e.Y > y * Case_y)
                        {
                            _Mouse_Position.Y = y;
                        }
                    }
                    int Piece_Type = m.Return_information(_Mouse_Position.X, _Mouse_Position.Y);
                    if (_AI_White)
                    {
                        if (!_WhiteTurn)
                        {
                            if (m.Return_information(_Mouse_Position.X, _Mouse_Position.Y) != 0)
                            {
                                _Mouse_Position_Depart.X = _Mouse_Position.X;
                                _Mouse_Position_Depart.Y = _Mouse_Position.Y;
                                _highlight = true;
                            }
                            if (m.Return_information(_Mouse_Position.X, _Mouse_Position.Y) == 0)
                            {
                                _Mouse_Position_Select.X = _Mouse_Position.X;
                                _Mouse_Position_Select.Y = _Mouse_Position.Y;
                                _highlight = false;
                                Human_Move(m.Return_information(_Mouse_Position_Depart.X, _Mouse_Position_Depart.Y));
                            }
                        }
                        if (_WhiteTurn)
                        {
                            AI_Move();
                        }
                    }

                    if (!_AI_White)
                    {
                        if (_WhiteTurn)
                        {
                            if (m.Return_information(_Mouse_Position.X, _Mouse_Position.Y) != 0)
                            {
                                _Mouse_Position_Depart.X = _Mouse_Position.X;
                                _Mouse_Position_Depart.Y = _Mouse_Position.Y;
                                _highlight = true;
                            }
                            if (m.Return_information(_Mouse_Position.X, _Mouse_Position.Y) == 0)
                            {
                                _Mouse_Position_Select.X = _Mouse_Position.X;
                                _Mouse_Position_Select.Y = _Mouse_Position.Y;
                                _highlight = false;
                                Human_Move(m.Return_information(_Mouse_Position_Depart.X, _Mouse_Position_Depart.Y));
                            }
                        }
                        if (!_WhiteTurn)
                        {
                            AI_Move();
                        }
                    }

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
                    draw();
                }
            }
        }
        private void Human_Move(int Piece_Type)
        {
            if (m.Return_information(_Mouse_Position_Depart.X, _Mouse_Position_Depart.Y) != 0)
            {
                bool[,] Position_Possible = new bool[5, 5];
                Position_Possible = m.Position_Mouvement(Piece_Type, _Mouse_Position_Depart.X, _Mouse_Position_Depart.Y);
                if (Position_Possible[_Mouse_Position_Select.X, _Mouse_Position_Select.Y])
                {
                    if (!AI_White)
                    {
                        if (_KingWhiteMoved == false && _WhiteTurn == true)
                        {
                            if (Piece_Type == 1)
                            {
                                m.Update_position(Piece_Type, _Mouse_Position_Depart.X, _Mouse_Position_Depart.Y, _Mouse_Position_Select.X, _Mouse_Position_Select.Y);
                                _KingWhiteMoved = true;
                                _KingBlackMoved = false;

                            }
                        }
                        if (_KingWhiteMoved == true && _WhiteTurn == true)
                        {
                            if (Piece_Type == 3)
                            {
                                m.Update_position(Piece_Type, _Mouse_Position_Depart.X, _Mouse_Position_Depart.Y, _Mouse_Position_Select.X, _Mouse_Position_Select.Y);
                                _WhiteTurn = false;
                                _KingWhiteMoved = false;
                            }
                        }
                    }
                    else
                    {
                        if (_KingBlackMoved == false && _WhiteTurn == false)
                        {
                            if (Piece_Type == 1)
                            {
                                m.Update_position(Piece_Type, _Mouse_Position_Depart.X, _Mouse_Position_Depart.Y, _Mouse_Position_Select.X, _Mouse_Position_Select.Y);
                                _KingWhiteMoved = false;
                                _KingBlackMoved = true;

                            }
                        }
                        if (_KingBlackMoved == true && _WhiteTurn == false)
                        {
                            if (Piece_Type == 2)
                            {
                                m.Update_position(Piece_Type, _Mouse_Position_Depart.X, _Mouse_Position_Depart.Y, _Mouse_Position_Select.X, _Mouse_Position_Select.Y);
                                _WhiteTurn = true;
                                _KingBlackMoved = false;
                            }
                        }
                    }
                }
            }
        }

        private void AI_Move()
        {

            ((CGraphiqueSolo)(this.Parent)).majlabel("Computer Turn!");
            CAnalyse analyse = new CAnalyse(_WhiteTurn, _AI_White, _KingBlackMoved, _GameState);
            analyse.King_Evaluation(m);
            if (!AI_White)
            {

                if (m.King_Piece() != null && _KingBlackMoved == false)
                {
                    m.Update_position(1, m.King_Piece().x, m.King_Piece().y, analyse.Kingdx, analyse.Kingdy);
                    ((CGraphiqueSolo)(this.Parent)).debug_label1(analyse.Kingdx.ToString() + " " + analyse.Kingdy.ToString() + " ");
                    _KingBlackMoved = true;
                    _KingWhiteMoved = false;
                }
                if (analyse.Pawn_Evaluation(m) != null && _WhiteTurn == false)
                {
                    CPiece get_ans = new CPiece();
                    get_ans = analyse.Pawn_Evaluation(m);
                    int dx = get_ans.x;
                    int dy = get_ans.y;
                    m.Update_position(2, m.Pawn_Piece(get_ans.Piece_ID).x, m.Pawn_Piece(get_ans.Piece_ID).y, get_ans.x, get_ans.y);
                    ((CGraphiqueSolo)(this.Parent)).debug_label2(get_ans.Piece_ID.ToString() + " " + get_ans.x.ToString() + " " + get_ans.y.ToString() + " ");
                    _WhiteTurn = true;
                    ((CGraphiqueSolo)(this.Parent)).majlabel("Player Turn!");
                }
            }
            else
            {

                if (m.King_Piece() != null && _KingWhiteMoved == false)
                {
                    m.Update_position(1, m.King_Piece().x, m.King_Piece().y, analyse.Kingdx, analyse.Kingdy);
                    ((CGraphiqueSolo)(this.Parent)).debug_label1(analyse.Kingdx.ToString() + " " + analyse.Kingdy.ToString() + " ");
                    _KingBlackMoved = false;
                    _KingWhiteMoved = true;
                }
                if (analyse.Pawn_Evaluation(m) != null && _WhiteTurn == true)
                {
                    CPiece get_ans = new CPiece();
                    get_ans = analyse.Pawn_Evaluation(m);
                    int dx = get_ans.x;
                    int dy = get_ans.y;
                    m.Update_position(3, m.Pawn_Piece(get_ans.Piece_ID).x, m.Pawn_Piece(get_ans.Piece_ID).y, get_ans.x, get_ans.y);
                    ((CGraphiqueSolo)(this.Parent)).debug_label2(get_ans.Piece_ID.ToString() + " " + get_ans.x.ToString() + " " + get_ans.y.ToString() + " ");
                    _WhiteTurn = false;
                    ((CGraphiqueSolo)(this.Parent)).majlabel("Player Turn!");
                }
            }
        }
    }
}
