using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Le_baillis
{
    class CCartographie
    {
        private List<CPiece> ListPieces; // Liste eds pièces
        public CCartographie()
        {
            ListPieces = new List<CPiece>();
        }
        
        public void Initilisation() //initialisation des Pièces dans le damier
        {
            ListPieces.Add(new CPiece(2, 0, 0, 0)); //pion noir
            ListPieces.Add(new CPiece(2, 1, 0, 1)); //pion noir
            ListPieces.Add(new CPiece(2, 2, 0, 2)); //pion noir
            ListPieces.Add(new CPiece(2, 3, 0, 3)); //pion noir
            ListPieces.Add(new CPiece(2, 4, 0, 4)); //pion noir
            ListPieces.Add(new CPiece(3, 0, 4, 5)); //pion blanc
            ListPieces.Add(new CPiece(3, 1, 4, 6)); //pion blanc
            ListPieces.Add(new CPiece(3, 2, 4, 7)); //pion blanc
            ListPieces.Add(new CPiece(3, 3, 4, 8)); //pion blanc
            ListPieces.Add(new CPiece(3, 4, 4, 9)); //pion blanc
            ListPieces.Add(new CPiece(1, 2, 2, 10)); //pion blanc
        }
        public List<CPiece> ReturnListDesPieces // Retourner la liste des pièces
        {
            get { return ListPieces; } 
        }

        public int Return_information(int x, int y) // Retourner le types des pièces soit 1,2,3, 0 est vide
        {
            int PieceType = 0;
            foreach (CPiece c in ListPieces)
            {
                if (c.x == x && c.y == y)
                {
                    PieceType = c.PieceType;
                }
            }
            return PieceType;
        }
        public bool[,] Position_Mouvement(int Piece_Type_Select, int x, int y) // Retourner les possition possible dans un table booléans [5,5]
        {
            bool[,] position_possible = new bool[5, 5]; // contient les possition valides
            bool coincidence = false; // vérification s'il y a un coincidence dans le chemin de recherche
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    position_possible[i, j] = false;
                }
            if (Piece_Type_Select != 0) // TRouver dans toutes les directions diagonales, verticales, horizontales les possitions possible
            {
                int col = x;
                int row = y;
                while (col < 4)
                {
                    col++;
                    // verifier si il y a pas la coincidence avec l'autre pièces, et les cases sont libre
                    if (Return_information(col, row) == 0 && coincidence == false) position_possible[col, row] = true; 
                    if (Return_information(col, row) != 0) coincidence = true;

                }
                col = x;
                row = y;
                coincidence = false;
                while (row < 4)
                {
                    row++;
                    if (Return_information(col, row) == 0 && coincidence == false) position_possible[col, row] = true;
                    if (Return_information(col, row) != 0) coincidence = true;
                }
                col = x;
                row = y;
                coincidence = false;
                while (col > 0)
                {
                    col--;
                    if (Return_information(col, row) == 0 && coincidence == false) position_possible[col, row] = true;
                    if (Return_information(col, row) != 0) coincidence = true;
                }
                col = x;
                row = y;
                coincidence = false;
                while (row > 0)
                {
                    row--;
                    if (Return_information(col, row) == 0 && coincidence == false) position_possible[col, row] = true;
                    if (Return_information(col, row) != 0) coincidence = true;
                }
                col = x;
                row = y;
                coincidence = false;
                while (row < 4 && col < 4)
                {
                    col++;
                    row++;
                    if (Return_information(col, row) == 0 && coincidence == false) position_possible[col, row] = true;
                    if (Return_information(col, row) != 0) coincidence = true;
                }
                col = x;
                row = y;
                coincidence = false;
                while (row > 0 && col < 4)
                {
                    row--;
                    col++;
                    if (Return_information(col, row) == 0 && coincidence == false) position_possible[col, row] = true;
                    if (Return_information(col, row) != 0) coincidence = true;
                }
                col = x;
                row = y;
                coincidence = false;
                while (row < 4 && col > 0)
                {
                    row++;
                    col--;
                    if (Return_information(col, row) == 0 && coincidence == false) position_possible[col, row] = true;
                    if (Return_information(col, row) != 0) coincidence = true;
                }
                col = x;
                row = y;
                coincidence = false;
                while (row > 0 && col > 0)
                {
                    col--;
                    row--;
                    if (Return_information(col, row) == 0 && coincidence == false) position_possible[col, row] = true;
                    if (Return_information(col, row) != 0) coincidence = true;
                }
            }
            return position_possible; // retourner la table
        }

        public void Update_position(int PieceType, int x, int y, int dx, int dy)
        {
            bool[,] position_possible = new bool[5, 5];
            position_possible = Position_Mouvement(PieceType, x, y);
            foreach (CPiece c in ListPieces)
            {
                if (c.PieceType == PieceType && c.y == y && c.x == x)
                {
                    c.x = dx;
                    c.y = dy;
                }
            }
        }
        public int Game_state()
        {
            int Winner = 0;
            foreach (CPiece c in ListPieces)
            {
                if (c.PieceType == 1)
                {
                    if (c.y == 4)
                    {
                        Winner = 3;
                    }
                    if (c.y == 0)
                    {
                        Winner = 2;
                    }
                }
            }
            return Winner;
        }
        public CPiece King_Piece()
        {
            CPiece King = new CPiece();
            foreach (CPiece c in ListPieces)
            {
                if (c.PieceType == 1)
                {
                    King = c;
                }
            }
            return King;
        }
        public CPiece Pawn_Piece(int PieceID)
        {
            CPiece Pawn = new CPiece();
            foreach (CPiece c in ListPieces)
            {
                if (c.Piece_ID == PieceID)
                {
                    Pawn = c;
                }
            }
            return Pawn;
        }
    }
}
