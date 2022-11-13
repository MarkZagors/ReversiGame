namespace ReversiGame
{
    public class Coords
    {
        public int row { set; get; }
        public int col { set; get; }

        public Coords(int _row, int _col)
        {
            row = _row;
            col = _col;
        }

        public Coords(Coords coords)
        {
            row = coords.row;
            col = coords.col;
        }

        public static Coords operator +(Coords coords1, Coords coords2)
        {
            return new Coords(coords1.row + coords2.row, coords1.col + coords2.col);
        }

        public Coords Reverse()
        {
            return new Coords(-row, -col);
        }

        public bool InBoardRange(Board board)
        {
            if (row < board.boardWidth && row >= 0 && col < board.boardHeight && col >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}