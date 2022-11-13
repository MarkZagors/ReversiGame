namespace ReversiGame
{
    public class Piece
    {
        public bool isBlack { private set; get; }

        public Piece(bool _isBlack)
        {
            isBlack = _isBlack;
        }
    }
}