using SFML.System;

namespace ReversiGame
{
    public class Space
    {
        public Piece? piece { set; get; }
        public Coords coords { private set; get; }
        public Vector2f position { private set; get; }
        public bool possibleMove { set; get; }

        public Space(Coords _coords, Vector2f _sizeSpace, Vector2f _boardOffset)
        {
            piece = null;
            coords = _coords;
            position = new Vector2f(_coords.col * _sizeSpace.X, _coords.row * _sizeSpace.Y) + _boardOffset;
            possibleMove = false;
        }
    }
}