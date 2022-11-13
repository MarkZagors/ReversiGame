using SFML.Graphics;
using SFML.System;

namespace ReversiGame
{
    public class TextRenderer : IRendering, IObserver
    {
        private readonly String FONTS_DIRECTORY = Directory.GetCurrentDirectory() + "/src/Assets/Fonts/";
        private Renderer renderer;
        private Board board;
        private Color backgroundColor = new Color(220, 138, 71);
        private Vector2f backgroundSize = new Vector2f(900, 200);
        private Font font;
        private Text turnText;
        private Text countTextWhite;
        private Text countTextBlack;
        private Color whiteTextColor = new Color(255, 255, 255);
        private Color blackTextColor = new Color(0, 0, 0);
        private bool blackTurn = true;
        private bool gameEnded = false;
        private bool isDraw = false;
        public TextRenderer()
        {
            renderer = Renderer.GetInstance();
            board = Board.GetInstance();

            font = new Font(FONTS_DIRECTORY + "font.ttf");

            turnText = new Text()
            {
                Font = font,
                CharacterSize = 60,
                Position = new Vector2f(100, 10),
            };

            countTextWhite = new Text()
            {
                Font = font,
                CharacterSize = 32,
                Position = new Vector2f(600, 10),
                FillColor = whiteTextColor,
            };

            countTextBlack = new Text()
            {
                Font = font,
                CharacterSize = 32,
                Position = new Vector2f(600, 50),
                FillColor = blackTextColor,
            };
        }

        public void Render()
        {
            RenderBackground();
            RenderText();
        }

        private void RenderBackground()
        {
            RectangleShape background = new RectangleShape();
            background.Size = backgroundSize;
            background.FillColor = backgroundColor;
            renderer.window.Draw(background);
        }

        private void RenderText()
        {
            if (!gameEnded)
                RenderTurnText();
            else
                RenderEndText();
            RenderPiecesCountText();
        }

        private void RenderTurnText()
        {
            if (blackTurn)
            {
                turnText.DisplayedString = "BLACK TURN";
                turnText.FillColor = blackTextColor;
            }
            else
            {
                turnText.DisplayedString = "WHITE TURN";
                turnText.FillColor = whiteTextColor;
            }
            renderer.window.Draw(turnText);
        }

        private void RenderEndText()
        {
            if (isDraw)
            {
                turnText.DisplayedString = "DRAW";
                turnText.FillColor = blackTextColor;
            }
            else if (blackTurn)
            {
                turnText.DisplayedString = "BLACK WINS";
                turnText.FillColor = blackTextColor;
            }
            else
            {
                turnText.DisplayedString = "WHITE WINS";
                turnText.FillColor = whiteTextColor;
            }
            renderer.window.Draw(turnText);
        }

        private void RenderPiecesCountText()
        {
            int blackCount = board.GetPiecesCount(true);
            int whiteCount = board.GetPiecesCount(false);
            countTextWhite.DisplayedString = "White: " + whiteCount;
            countTextBlack.DisplayedString = "Black: " + blackCount;
            renderer.window.Draw(countTextWhite);
            renderer.window.Draw(countTextBlack);
        }

        public void Update(Globals.Signal signal)
        {
            switch (signal)
            {
                case Globals.Signal.ON_CLICK:
                    blackTurn = !blackTurn;
                    break;
                case Globals.Signal.ON_DRAW:
                    blackTurn = true;
                    gameEnded = true;
                    isDraw = true;
                    break;
                case Globals.Signal.ON_WHITE_WIN:
                    blackTurn = false;
                    gameEnded = true;
                    break;
                case Globals.Signal.ON_BLACK_WIN:
                    blackTurn = true;
                    gameEnded = true;
                    break;
            }
        }
    }
}