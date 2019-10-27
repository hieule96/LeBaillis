using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Le_baillis
{
    class CAnalyse
    {
        #region Declaration des variable
        private bool _AI_White; //true is white and false is black
        private bool _WhiteTurn;
        private bool _KingMoved;
        private bool _GameState;
        private int _KingdX;
        private int _KingdY;
        #endregion

        #region Constructeur/ Passage par parametre
        public CAnalyse(bool WhiteTurn, bool AI_White = false, bool KingMoved = false, bool GameState = true)
        {
            _KingdX = 0;
            _KingdY = 0;
            _WhiteTurn = WhiteTurn;
            _AI_White = AI_White;
            _KingMoved = KingMoved;
            _GameState = GameState;
        }

        public bool White_Turn
        {
            get { return White_Turn; }
            set { White_Turn = value; }
        }
        public bool KingMoved
        {
            get { return _KingMoved; }
            set { KingMoved = value; }
        }
        public bool WhiteTurn
        {
            get { return _WhiteTurn; }
            set { _WhiteTurn = value; }
        }
        public bool GameState
        {
            get { return _GameState; }
            set { _GameState = value; }
        }
        public int Kingdx
        {
            get { return _KingdX; }
        }
        public int Kingdy
        {
            get { return _KingdY; }
        }
        #endregion

        #region Mouvement de ROI
        public void King_Evaluation(CCartographie cartographie)
        {
            bool[,] table_mouvement_possible = new bool[5, 5];
            bool Need_Random = true;
            table_mouvement_possible = cartographie.Position_Mouvement(1, cartographie.King_Piece().x, cartographie.King_Piece().y);
            _KingdX = cartographie.King_Piece().x;
            _KingdY = cartographie.King_Piece().y;
            for (int j = 0; j < 5; j++)
            {
                if (table_mouvement_possible[j, 0] == true && _AI_White == false && _WhiteTurn == false && _KingMoved == false)
                {
                    _KingdX = j;
                    _KingdY = 0;
                    Need_Random = false;
                }
                if (table_mouvement_possible[j, 4] == true && _AI_White == true && _WhiteTurn == true && _KingMoved == false)
                {
                    _KingdX = j;
                    _KingdY = 4;
                    Need_Random = false;
                }
            }
           while(Need_Random)
            {
                int[,] table = new int[5, 5];
                int compt = 0;
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (table_mouvement_possible[i, j])
                        {
                            table[i, j] = compt;
                            compt++;
                        }
                Random rnd = new Random();
                int rnd_number = rnd.Next(0, compt);
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (rnd_number == table[i, j]&&table_mouvement_possible[i,j])
                        {
                            if (!_AI_White && j != 4)
                            {
                                _KingdX = i;
                                _KingdY = j;
                                Need_Random = false;
                            }
                            if (_AI_White && j != 0)
                            {
                                _KingdX = i;
                                _KingdY = j;
                                Need_Random = false;
                            }
                        }

            }
        }
        #endregion

        #region Mouvement de Pawn
        public CPiece Pawn_Evaluation(CCartographie cartographie)
        {

            CPiece Pawn_deplace = new CPiece();
            bool Need_random = true;
            bool[,] table_mouvement_Pawn_0 = new bool[5, 5];
            bool[,] table_mouvement_Pawn_1 = new bool[5, 5];
            bool[,] table_mouvement_Pawn_2 = new bool[5, 5];
            bool[,] table_mouvement_Pawn_3 = new bool[5, 5];
            bool[,] table_mouvement_Pawn_4 = new bool[5, 5];
            int[,] table0 = new int[5, 5];
            int[,] table1 = new int[5, 5];
            int[,] table2 = new int[5, 5];
            int[,] table3 = new int[5, 5];
            int[,] table4 = new int[5, 5];
            #region Traitement si AI n'est pas blanc
            if (!_AI_White)
            {
                table_mouvement_Pawn_0 = cartographie.Position_Mouvement(cartographie.ReturnListDesPieces[0].PieceType, cartographie.ReturnListDesPieces[0].x, cartographie.ReturnListDesPieces[0].y);
                table_mouvement_Pawn_1 = cartographie.Position_Mouvement(cartographie.ReturnListDesPieces[1].PieceType, cartographie.ReturnListDesPieces[1].x, cartographie.ReturnListDesPieces[1].y);
                table_mouvement_Pawn_2 = cartographie.Position_Mouvement(cartographie.ReturnListDesPieces[2].PieceType, cartographie.ReturnListDesPieces[2].x, cartographie.ReturnListDesPieces[2].y);
                table_mouvement_Pawn_3 = cartographie.Position_Mouvement(cartographie.ReturnListDesPieces[3].PieceType, cartographie.ReturnListDesPieces[3].x, cartographie.ReturnListDesPieces[3].y);
                table_mouvement_Pawn_4 = cartographie.Position_Mouvement(cartographie.ReturnListDesPieces[4].PieceType, cartographie.ReturnListDesPieces[4].x, cartographie.ReturnListDesPieces[4].y);
                for (int i = 0; i < 5; i++)
                {
                    if (table_mouvement_Pawn_0[i, 4] == true && cartographie.Pawn_Piece(0).y != 4)
                    {
                        Pawn_deplace.PieceType = 2;
                        Pawn_deplace.x = i;
                        Pawn_deplace.y = 4;
                        Pawn_deplace.Piece_ID = 0;
                        Need_random = false;
                    }
                    if (table_mouvement_Pawn_1[i, 4] == true && cartographie.Pawn_Piece(1).y != 4)
                    {
                        Pawn_deplace.PieceType = 2;
                        Pawn_deplace.x = i;
                        Pawn_deplace.y = 4;
                        Pawn_deplace.Piece_ID = 1;
                        Need_random = false;
                    }
                    if (table_mouvement_Pawn_2[i, 4] == true && cartographie.Pawn_Piece(2).y != 4)
                    {
                        Pawn_deplace.PieceType = 2;
                        Pawn_deplace.x = i;
                        Pawn_deplace.y = 4;
                        Pawn_deplace.Piece_ID = 2;
                        Need_random = false;
                    }
                    if (table_mouvement_Pawn_3[i, 4] == true && cartographie.Pawn_Piece(3).y != 4)
                    {
                        Pawn_deplace.PieceType = 2;
                        Pawn_deplace.x = i;
                        Pawn_deplace.y = 4;
                        Pawn_deplace.Piece_ID = 3;
                        Need_random = false;
                    }
                    if (table_mouvement_Pawn_4[i, 4] == true && cartographie.Pawn_Piece(4).y != 4)
                    {
                        Pawn_deplace.PieceType = 2;
                        Pawn_deplace.x = i;
                        Pawn_deplace.y = 4;
                        Pawn_deplace.Piece_ID = 4;
                        Need_random = false;
                    }
                }
                while (Need_random)
                {
                    Random rnd_Pawn = new Random();
                    int rnd_Pawn_number = rnd_Pawn.Next(0, 5);
                    int count = 0;

                    if (rnd_Pawn_number == 0)
                    {
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                            {
                                if (table_mouvement_Pawn_0[i, j])
                                {
                                    table0[i, j] = count;
                                    count++;
                                }
                            }
                        if (count != 0)
                        {
                            int rnd_Pawn_move = rnd_Pawn.Next(0, count);
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                {
                                    if (rnd_Pawn_move == table0[i, j] && table_mouvement_Pawn_0[i, j])
                                    {
                                        Pawn_deplace.PieceType = 2;
                                        Pawn_deplace.x = i;
                                        Pawn_deplace.y = j;
                                        Pawn_deplace.Piece_ID = 0;
                                        Need_random = false;
                                    }
                                }
                        }
                    }
                    count = 0;
                    if (rnd_Pawn_number == 1)
                    {
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                            {
                                if (table_mouvement_Pawn_1[i, j])
                                {
                                    table1[i, j] = count;
                                    count++;
                                }
                            }
                        if (count != 0)
                        {
                            int rnd_Pawn_move = rnd_Pawn.Next(0, count);
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                {
                                    if (table1[i, j] == rnd_Pawn_move && table_mouvement_Pawn_1[i, j])
                                    {
                                        Pawn_deplace.PieceType = 2;
                                        Pawn_deplace.x = i;
                                        Pawn_deplace.y = j;
                                        Pawn_deplace.Piece_ID = 1;
                                        Need_random = false;
                                    }
                                }
                        }
                    }
                    count = 0;
                    if (rnd_Pawn_number == 2)
                    {
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                            {
                                if (table_mouvement_Pawn_2[i, j])
                                {
                                    table2[i, j] = count;
                                    count++;
                                }
                            }
                        if (count != 0)
                        {
                            int rnd_Pawn_move = rnd_Pawn.Next(0, count);
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                {
                                    if (table2[i, j] == rnd_Pawn_move && table_mouvement_Pawn_2[i, j])
                                    {
                                        Pawn_deplace.PieceType = 2;
                                        Pawn_deplace.x = i;
                                        Pawn_deplace.y = j;
                                        Pawn_deplace.Piece_ID = 2;
                                        Need_random = false;
                                    }
                                }
                        }
                    }
                    count = 0;
                    if (rnd_Pawn_number == 3)
                    {
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                            {
                                if (table_mouvement_Pawn_3[i, j])
                                {
                                    table3[i, j] = count;
                                    count++;
                                }
                            }
                        if (count != 0)
                        {
                            int rnd_Pawn_move = rnd_Pawn.Next(0, count);
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                {
                                    if (table3[i, j] == rnd_Pawn_move && table_mouvement_Pawn_3[i, j])
                                    {
                                        Pawn_deplace.PieceType = 2;
                                        Pawn_deplace.x = i;
                                        Pawn_deplace.y = j;
                                        Pawn_deplace.Piece_ID = 3;
                                        Need_random = false;
                                    }
                                }
                        }
                    }
                    count = 0;
                    if (rnd_Pawn_number == 4)
                    {
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                            {
                                if (table_mouvement_Pawn_4[i, j])
                                {
                                    table4[i, j] = count;
                                    count++;
                                }
                            }
                        if (count != 0)
                        {
                            int rnd_Pawn_move = rnd_Pawn.Next(0, count);
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                {
                                    if (table4[i, j] == rnd_Pawn_move && table_mouvement_Pawn_4[i, j])
                                    {
                                        Pawn_deplace.PieceType = 2;
                                        Pawn_deplace.x = i;
                                        Pawn_deplace.y = j;
                                        Pawn_deplace.Piece_ID = 4;
                                        Need_random = false;
                                    }
                                }
                        }
                    }
                }
            }
            #endregion

            #region Traitement si AI est blanc
            else
            {
                table_mouvement_Pawn_0 = cartographie.Position_Mouvement(cartographie.ReturnListDesPieces[5].PieceType, cartographie.ReturnListDesPieces[5].x, cartographie.ReturnListDesPieces[5].y);
                table_mouvement_Pawn_1 = cartographie.Position_Mouvement(cartographie.ReturnListDesPieces[6].PieceType, cartographie.ReturnListDesPieces[6].x, cartographie.ReturnListDesPieces[6].y);
                table_mouvement_Pawn_2 = cartographie.Position_Mouvement(cartographie.ReturnListDesPieces[7].PieceType, cartographie.ReturnListDesPieces[7].x, cartographie.ReturnListDesPieces[7].y);
                table_mouvement_Pawn_3 = cartographie.Position_Mouvement(cartographie.ReturnListDesPieces[8].PieceType, cartographie.ReturnListDesPieces[8].x, cartographie.ReturnListDesPieces[8].y);
                table_mouvement_Pawn_4 = cartographie.Position_Mouvement(cartographie.ReturnListDesPieces[9].PieceType, cartographie.ReturnListDesPieces[9].x, cartographie.ReturnListDesPieces[9].y);
                for (int i = 0; i < 5; i++)
                {
                    if (table_mouvement_Pawn_0[i, 0] == true && cartographie.Pawn_Piece(5).y != 0)
                    {
                        Pawn_deplace.PieceType = 3;
                        Pawn_deplace.x = i;
                        Pawn_deplace.y = 0;
                        Pawn_deplace.Piece_ID = 5;
                        Need_random = false;
                    }
                    if (table_mouvement_Pawn_1[i, 0] == true && cartographie.Pawn_Piece(6).y != 0)
                    {
                        Pawn_deplace.PieceType = 3;
                        Pawn_deplace.x = i;
                        Pawn_deplace.y = 0;
                        Pawn_deplace.Piece_ID = 6;
                        Need_random = false;
                    }
                    if (table_mouvement_Pawn_2[i, 0] == true && cartographie.Pawn_Piece(7).y != 0)
                    {
                        Pawn_deplace.PieceType = 3;
                        Pawn_deplace.x = i;
                        Pawn_deplace.y = 0;
                        Pawn_deplace.Piece_ID = 7;
                        Need_random = false;
                    }
                    if (table_mouvement_Pawn_3[i, 0] == true && cartographie.Pawn_Piece(8).y != 0)
                    {
                        Pawn_deplace.PieceType = 3;
                        Pawn_deplace.x = i;
                        Pawn_deplace.y = 0;
                        Pawn_deplace.Piece_ID = 8;
                        Need_random = false;
                    }
                    if (table_mouvement_Pawn_4[i, 0] == true && cartographie.Pawn_Piece(9).y != 0)
                    {
                        Pawn_deplace.PieceType = 3;
                        Pawn_deplace.x = i;
                        Pawn_deplace.y = 0;
                        Pawn_deplace.Piece_ID = 8;
                        Need_random = false;
                    }
                }
                while (Need_random)
                {
                    Random rnd_Pawn = new Random();
                    int rnd_Pawn_number = rnd_Pawn.Next(0, 5);
                    int count = 0;

                    if (rnd_Pawn_number == 0)
                    {
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                            {
                                if (table_mouvement_Pawn_0[i, j])
                                {
                                    table0[i, j] = count;
                                    count++;
                                }
                            }
                        if (count != 0)
                        {
                            int rnd_Pawn_move = rnd_Pawn.Next(0, count);
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                {
                                    if (rnd_Pawn_move == table0[i, j] && table_mouvement_Pawn_0[i, j])
                                    {
                                        Pawn_deplace.PieceType = 3;
                                        Pawn_deplace.x = i;
                                        Pawn_deplace.y = j;
                                        Pawn_deplace.Piece_ID = 5;
                                        Need_random = false;
                                    }
                                }
                        }
                    }
                    count = 0;
                    if (rnd_Pawn_number == 1)
                    {
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                            {
                                if (table_mouvement_Pawn_1[i, j])
                                {
                                    table1[i, j] = count;
                                    count++;
                                }
                            }
                        if (count != 0)
                        {
                            int rnd_Pawn_move = rnd_Pawn.Next(0, count);
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                {
                                    if (table1[i, j] == rnd_Pawn_move && table_mouvement_Pawn_1[i, j])
                                    {
                                        Pawn_deplace.PieceType = 3;
                                        Pawn_deplace.x = i;
                                        Pawn_deplace.y = j;
                                        Pawn_deplace.Piece_ID = 6;
                                        Need_random = false;
                                    }
                                }
                        }
                    }
                    count = 0;
                    if (rnd_Pawn_number == 2)
                    {
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                            {
                                if (table_mouvement_Pawn_2[i, j])
                                {
                                    table2[i, j] = count;
                                    count++;
                                }
                            }
                        if (count != 0)
                        {
                            int rnd_Pawn_move = rnd_Pawn.Next(0, count);
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                {
                                    if (table2[i, j] == rnd_Pawn_move && table_mouvement_Pawn_2[i, j])
                                    {
                                        Pawn_deplace.PieceType = 3;
                                        Pawn_deplace.x = i;
                                        Pawn_deplace.y = j;
                                        Pawn_deplace.Piece_ID = 7;
                                        Need_random = false;
                                    }
                                }
                        }
                    }
                    count = 0;
                    if (rnd_Pawn_number == 3)
                    {
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                            {
                                if (table_mouvement_Pawn_3[i, j])
                                {
                                    table3[i, j] = count;
                                    count++;
                                }
                            }
                        if (count != 0)
                        {
                            int rnd_Pawn_move = rnd_Pawn.Next(0, count);
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                {
                                    if (table3[i, j] == rnd_Pawn_move && table_mouvement_Pawn_3[i, j])
                                    {
                                        Pawn_deplace.PieceType = 3;
                                        Pawn_deplace.x = i;
                                        Pawn_deplace.y = j;
                                        Pawn_deplace.Piece_ID = 8;
                                        Need_random = false;
                                    }
                                }
                        }
                    }
                    count = 0;
                    if (rnd_Pawn_number == 4)
                    {
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                            {
                                if (table_mouvement_Pawn_4[i, j])
                                {
                                    table4[i, j] = count;
                                    count++;
                                }
                            }
                        if (count != 0)
                        {
                            int rnd_Pawn_move = rnd_Pawn.Next(0, count);
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                {
                                    if (table4[i, j] == rnd_Pawn_move && table_mouvement_Pawn_4[i, j])
                                    {
                                        Pawn_deplace.PieceType = 3;
                                        Pawn_deplace.x = i;
                                        Pawn_deplace.y = j;
                                        Pawn_deplace.Piece_ID = 9;
                                        Need_random = false;
                                    }
                                }
                        }
                    }
                }                
            }
            #endregion

            return Pawn_deplace;
        }
        #endregion

    }
}
