namespace ReversiGame
{
    public class MoveManager
    {
        public bool blackTurn = true;
        private List<Coords> traverseVectors = new List<Coords>() {
                new Coords(0,1),
                new Coords(0,-1),
                new Coords(1,0),
                new Coords(-1,0),
                new Coords(1,1),
                new Coords(-1,-1),
                new Coords(1,-1),
                new Coords(-1,1),
            };
        private InputManager inputManager;
        private Board board;

        public MoveManager(InputManager _inputManager)
        {
            board = Board.GetInstance();
            inputManager = _inputManager;
        }

        public void SetPossibleMoves()
        {
            board.LoopSpaces((Coords coords) =>
            {
                Piece? piece = board.GetSpace(coords).piece;
                if (piece != null && blackTurn != piece.isBlack)
                {
                    SetPieceMoves(coords, board, piece.isBlack);
                }
            });
            if (board.GetPossibleMovesCount() == 0)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            int blackCount = board.GetPiecesCount(true);
            int whiteCount = board.GetPiecesCount(false);
            if (blackCount == whiteCount) inputManager.EndGame(Globals.Signal.ON_DRAW);
            else if (blackCount > whiteCount) inputManager.EndGame(Globals.Signal.ON_BLACK_WIN);
            else if (whiteCount > blackCount) inputManager.EndGame(Globals.Signal.ON_WHITE_WIN);
        }

        public void ClearPossibleMoves()
        {
            board.LoopSpaces((Coords coords) =>
            {
                board.GetSpace(coords).possibleMove = false;
            });
        }

        public void SetTrappedPieces(Coords coords, bool isBlack)
        {
            List<Coords> trappedPiecesCoords = new List<Coords>();
            foreach (Coords traverseVector in traverseVectors)
            {
                trappedPiecesCoords.AddRange(TraverseTrap(coords, traverseVector, board, isBlack));
            }
            foreach (Coords trappedPieceCoord in trappedPiecesCoords)
            {
                board.SetPiece(trappedPieceCoord, blackTurn);
            }
        }

        private void SetPieceMoves(Coords currentCoords, Board board, bool isBlack)
        {
            List<Coords> pieceMoves = new List<Coords>();
            foreach (Coords traverseVector in traverseVectors)
            {
                if (Traverse(currentCoords, traverseVector, board, isBlack))
                    SetPossibleIfUnoccupied(currentCoords, traverseVector, board);
            }
        }

        private void SetPossibleIfUnoccupied(Coords currentCoords, Coords traverseVector, Board board)
        {
            Coords newCoords = currentCoords + traverseVector.Reverse();
            if (newCoords.InBoardRange(board) && board.GetSpace(newCoords).piece == null)
                board.GetSpace(newCoords).possibleMove = true;
        }

        private bool Traverse(Coords currentCoords, Coords traverseVector, Board board, bool currentPieceIsBlack)
        {
            Coords traverseCoords = new Coords(currentCoords);
            while (true)
            {
                traverseCoords += traverseVector;
                if (!traverseCoords.InBoardRange(board)) return false;
                Piece? traversePiece = board.GetSpace(traverseCoords).piece;
                if (traversePiece == null) return false;
                if (traversePiece.isBlack != currentPieceIsBlack) return true;
            }
        }

        private List<Coords> TraverseTrap(Coords currentCoords, Coords traverseVector, Board board, bool currentPieceIsBlack)
        {
            List<Coords> coordsList = new List<Coords>();
            Coords traverseCoords = new Coords(currentCoords);
            while (true)
            {
                traverseCoords += traverseVector;
                if (!traverseCoords.InBoardRange(board)) return new List<Coords>();
                Piece? traversePiece = board.GetSpace(traverseCoords).piece;
                if (traversePiece == null) return new List<Coords>();
                if (traversePiece.isBlack != currentPieceIsBlack)
                {
                    coordsList.Add(traverseCoords);
                }
                if (traversePiece.isBlack == currentPieceIsBlack) return coordsList;
            }
        }
    }
}