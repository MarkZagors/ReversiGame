using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ReversiGame
{
    public class PiecesRenderer : IRendering
    {
        private Board board;
        private Renderer renderer;
        private int outlineThickness = 4;
        private Color whitePieceOutlineColor = Color.Black;
        private Color blackPieceOutlineColor = Color.White;
        private Color whitePieceColor = Color.White;
        private Color blackPieceColor = Color.Black;
        private float circleSize = 0.8f;

        public PiecesRenderer()
        {
            board = Board.GetInstance();
            renderer = Renderer.GetInstance();
        }

        public void Render()
        {
            board.LoopSpaces((Coords coords) =>
            {
                Piece? piece = board.GetSpace(coords).piece;
                if (piece != null)
                    RenderPiece(coords, piece.isBlack);
            });
        }

        private void RenderPiece(Coords coords, bool _isBlack)
        {
            CircleShape circle = new CircleShape();
            float maxAxis = Math.Max(board.sizeSpace.X, board.sizeSpace.Y);
            float circleCenterX = board.sizeSpace.X * (1f - circleSize) * board.sizeSpace.X / maxAxis;
            float circleCenterY = board.sizeSpace.Y * (1f - circleSize) * board.sizeSpace.Y / maxAxis;
            Vector2f circleCenterOffset = new Vector2f(circleCenterX, circleCenterY) / 2;

            circle.Radius = maxAxis / 2;
            circle.Scale = new Vector2f(board.sizeSpace.X / maxAxis, board.sizeSpace.Y / maxAxis) * circleSize;
            circle.Position = board.GetSpace(coords).position + circleCenterOffset;

            if (_isBlack)
            {
                circle.FillColor = blackPieceColor;
                circle.OutlineColor = blackPieceOutlineColor;
            }
            else
            {
                circle.FillColor = whitePieceColor;
                circle.OutlineColor = whitePieceOutlineColor;
            }
            circle.OutlineThickness = outlineThickness;
            renderer.window.Draw(circle);
        }
    }
}