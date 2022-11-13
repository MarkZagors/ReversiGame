using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ReversiGame
{
    public class BoardRenderer : IRendering
    {
        private Board board;
        private Renderer renderer;
        private int outlineThickness = 3;
        private Color outlineColor = Color.Black;
        private Color spaceColorNormal = new Color(5, 138, 71);
        private Color spaceColorPossibleMove = new Color(155, 138, 71);

        public BoardRenderer()
        {
            board = Board.GetInstance();
            renderer = Renderer.GetInstance();
        }

        public void Render()
        {
            board.LoopSpaces((Coords coords) =>
            {
                RenderSpace(coords);
            });
        }

        private void RenderSpace(Coords coords)
        {
            RectangleShape rect = new RectangleShape();
            rect.Position = board.GetSpace(coords).position;
            rect.Size = board.sizeSpace;
            if (board.GetSpace(coords).possibleMove) rect.FillColor = spaceColorPossibleMove;
            else rect.FillColor = spaceColorNormal;
            rect.OutlineThickness = outlineThickness;
            rect.OutlineColor = outlineColor;
            renderer.window.Draw(rect);
        }
    }
}