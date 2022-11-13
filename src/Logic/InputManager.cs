using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ReversiGame
{
    public class InputManager : ISubject
    {
        private Board board;
        private Renderer renderer;
        private MoveManager moveManager;
        private List<IObserver> observers = new List<IObserver>();
        private bool gameEnded = false;
        private State state;

        public InputManager(State state)
        {
            board = Board.GetInstance();
            renderer = Renderer.GetInstance();
            renderer.window.MouseButtonPressed += MouseButtonPressed;

            moveManager = new MoveManager(this);
            moveManager.SetPossibleMoves();

            this.state = state;
            this.gameEnded = false;
            Console.WriteLine("Input manager created");
        }

        private void MouseButtonPressed(object? sender, EventArgs e)
        {
            if (gameEnded)
            {
                renderer.window.MouseButtonPressed -= MouseButtonPressed;
                state.NewGame();
                return;
            }
            FindAndClickSpace(Mouse.GetPosition(renderer.window));
        }

        private void FindAndClickSpace(Vector2i mousePos)
        {
            board.LoopSpaces((Coords coords) =>
            {
                if (IsInSpace(coords, (Vector2f)mousePos) && board.GetSpace(coords).possibleMove)
                {
                    Click(coords);
                    return;
                }
            });
        }

        private void Click(Coords _coords)
        {
            Emit(Globals.Signal.ON_CLICK);
            board.SetPiece(_coords, _isBlack: moveManager.blackTurn);
            moveManager.SetTrappedPieces(_coords, moveManager.blackTurn);
            moveManager.ClearPossibleMoves();
            moveManager.blackTurn = !moveManager.blackTurn;
            moveManager.SetPossibleMoves();
        }

        private bool IsInSpace(Coords _coords, Vector2f mousePos)
        {
            Vector2f spacePos = board.GetSpace(_coords).position;
            float lowerX = spacePos.X;
            float higherX = spacePos.X + board.sizeSpace.X;
            float lowerY = spacePos.Y;
            float higherY = spacePos.Y + board.sizeSpace.Y;
            if (mousePos.X > lowerX && mousePos.X < higherX && mousePos.Y > lowerY && mousePos.Y < higherY)
                return true;
            else
                return false;
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Emit(Globals.Signal signal)
        {
            foreach (IObserver observer in observers)
            {
                observer.Update(signal);
            }
        }

        public void EndGame(Globals.Signal signal)
        {
            gameEnded = true;
            Emit(signal);
        }
    }
}