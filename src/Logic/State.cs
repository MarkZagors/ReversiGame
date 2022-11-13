using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ReversiGame
{
    public class State
    {
        private const string TITLE = "Reversi";
        private const int WINDOW_WIDTH = 900;
        private const int WINDOW_HEIGHT = 900;
        private RenderWindow window { set; get; }

        public State()
        {
            VideoMode mode = new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT);
            window = new RenderWindow(mode, TITLE, style: Styles.Titlebar | Styles.Close);
            window.SetVerticalSyncEnabled(true);
            window.Closed += (sender, args) => window.Close();
        }

        public void NewGame()
        {
            Board board = new Board();
            board.SetPiece(new Coords(4, 3), true);
            board.SetPiece(new Coords(3, 4), true);
            board.SetPiece(new Coords(3, 3), false);
            board.SetPiece(new Coords(4, 4), false);

            Renderer renderer = new Renderer(window);
            TextRenderer textRenderer = new TextRenderer();
            BoardRenderer boardRenderer = new BoardRenderer();
            PiecesRenderer piecesRenderer = new PiecesRenderer();
            renderer.AddLayer(textRenderer);
            renderer.AddLayer(boardRenderer);
            renderer.AddLayer(piecesRenderer);

            InputManager inputManager = new InputManager(this);
            inputManager.Attach(textRenderer);
            renderer.Show();
        }
    }
}