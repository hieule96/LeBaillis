using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Le_baillis
{
    class CPiece
    {
        private int _PieceType;// si 1 (Roi), 2 (Pions Noirs), 3 (Pions Blanc)
        private int _pos_x; // position en x des pièces
        private int _pos_y;  //position en y des pièces
        private int _Piece_ID; // Identifiants des pièces (0-4) Pions Noirs, (5-9) pions blancs, 10 Roi

        public CPiece()
        {
            _PieceType = 0; 
            _pos_x = 0;
            _pos_y = 0;
            _Piece_ID = 0;
        }

        public CPiece(int PieceType, int x, int y, int Piece_ID)
        {
            _PieceType = PieceType;
            _Piece_ID = Piece_ID;
            _pos_x = x;
            _pos_y = y;
        }

        public int Piece_ID
        {
            set { _Piece_ID = value; }
            get { return _Piece_ID; }
        }
        public int PieceType
        {
            set { _PieceType = value; }
            get { return _PieceType; }
        }
        public int x
        {
            set { _pos_x = value; }
            get { return _pos_x; }
        }
        public int y
        {
            set { _pos_y = value; }
            get { return _pos_y; }
        }
    }
}
