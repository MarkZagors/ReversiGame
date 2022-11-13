using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ReversiGame
{
    public class Board
    {
        private static Board? instance;
        private readonly Vector2f sizeBoard = new Vector2f(700, 700);
        private readonly Vector2f boardOffset = new Vector2f(100, 100);
        public readonly Vector2f sizeSpace;
        public readonly int boardWidth = 8;
        public readonly int boardHeight = 8;
        private readonly Space[,] spaces;

        public Board()
        {
            sizeSpace.X = sizeBoard.X / boardWidth;
            sizeSpace.Y = sizeBoard.Y / boardHeight;
            spaces = new Space[boardHeight, boardWidth];
            SetupSpaces();
            instance = this;
        }

        public static Board GetInstance()
        {
            if (instance == null) return new Board();
            return instance;
        }

        private void SetupSpaces()
        {
            LoopSpaces((Coords coords) =>
            {
                GetSpace(coords) = new Space(coords, sizeSpace, boardOffset);
            });
        }

        public void SetPiece(Coords coords, bool _isBlack)
        {
            spaces[coords.row, coords.col].piece = new Piece(_isBlack);
        }

        public void LoopSpaces(Action<Coords> function)
        {
            for (int row = 0; row < boardHeight; row++)
            {
                for (int col = 0; col < boardWidth; col++)
                {
                    function(new Coords(row, col));
                }
            }
        }

        public ref Space GetSpace(Coords coords)
        {
            return ref spaces[coords.row, coords.col];
        }

        public int GetPiecesCount(bool blackPieces)
        {
            int count = 0;
            LoopSpaces((Coords coords) =>
            {
                Piece? piece = GetSpace(coords).piece;
                if (piece?.isBlack == blackPieces) count++;
            });
            return count;
        }

        public int GetPossibleMovesCount()
        {
            int count = 0;
            LoopSpaces((Coords coords) =>
            {
                if (GetSpace(coords).possibleMove) count++;
            });
            return count;
        }
    }
}