using BoardSystem;
using GameSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameSystem.MoveCommands
{
    public class MovementHelper
    {
        public delegate bool Validator(Board<ChessPiece> board, ChessPiece ChessPiece, Tile toTile);

        private Board<ChessPiece> _board;
        private ChessPiece _chessPiece;
        private List<Tile> _tiles = new List<Tile>();

        public MovementHelper(Board<ChessPiece> board, ChessPiece piece)
        {
            _board = board;
            _chessPiece = piece;
        }
        public MovementHelper North(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(0,1, step, validators);
        }
        public MovementHelper NorthEast(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(1,1, step, validators);
        }
        public MovementHelper East(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(1,0, step, validators);
        }
        public MovementHelper West(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1,1, step, validators);
        }
        public MovementHelper NorthWest(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1,1, step, validators);
        }
        public MovementHelper South(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(0,-1, step, validators);
        }
        public MovementHelper SouthWest(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1,-1, step, validators);
        }
        public MovementHelper SouthEast(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(1,-1, step, validators);
        }
        public MovementHelper Collect(int x,int y, int steps = int.MaxValue, params Validator[] validators)
        {

            Position MoveNext(Position position) 
            {
                position.X += _chessPiece.IsLight ? x : -x;
                position.Y += _chessPiece.IsLight ? y : -y;

                return position;
            }

            var startTile = _board.TileOf(_chessPiece);
            var startPosition = startTile.Position;

            var nextPosition = MoveNext(startPosition);

            int currentStep = 0;

            var blocked = false;

            while (!blocked && currentStep < steps)
            {                                                                                       
                var nextTile = _board.TileAt(nextPosition);
                if (nextTile == null)
                {
                    blocked = true;
                    break;
                }

            var nextPiece = _board.PieceAt(nextTile);

            if (nextPiece != null)
                blocked = true;

            if ((nextPiece == null || _chessPiece.IsLight != nextPiece.IsLight) && validators.All(v => v(_board, _chessPiece, nextTile)))
                _tiles.Add(nextTile);

                nextPosition =  MoveNext(nextPosition);
                currentStep++;
             }
            return this;
        }

       

        public List<Tile> Generate()
        {
            return _tiles;
        }

        public static bool CanCapture(Board<ChessPiece> board, ChessPiece piece, Tile tile)
        {
            return board.PieceAt(tile) != null;
        }
        public static bool IsEmpty(Board<ChessPiece> board, ChessPiece piece, Tile tile)
        {
            return board.PieceAt(tile) == null;
        }

    }
}
