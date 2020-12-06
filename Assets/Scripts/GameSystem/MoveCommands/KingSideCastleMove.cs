using BoardSystem;
using GameSystem.Models;
using GameSystem.MoveCommands;
using ReplaySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommandProviders
{
    class KingSideCastleMove : AbstractBasicMoveCommand
    {
        public KingSideCastleMove(ReplayManager replayManager) : base(replayManager)
        {

        }

        public override bool CanExecute(Board<ChessPiece> board, ChessPiece piece)
        {
            if (piece.HasMoved)
                return false;

            var tile = board.TileOf(piece);

            var rookPosition = tile.Position;
            rookPosition.X += 3;

            var rookTile = board.TileAt(rookPosition);
            if (rookTile == null)
                return false;

            var rookPiece = board.PieceAt(rookTile);
            if (rookTile == null || rookPiece.HasMoved)
                return false;

            var intermediatePosition = tile.Position;
            for (int i = 1; i < 3; i++)
            {
                intermediatePosition.X += 1;
                var intermediateTile = board.TileAt(intermediatePosition);
                if (intermediateTile == null)
                    return false;

                var intermediatePiece = board.PieceAt(intermediateTile);
                if (intermediatePiece != null)
                    return false;
            }

            return true;
        }

        public override void Execute(Board<ChessPiece> board, ChessPiece piece, Tile toTile)
        {
            var fromTile = board.TileOf(piece);
            

            var rookFromPosition = toTile.Position;
            rookFromPosition.X += 1;
            var rookFromTile = board.TileAt(rookFromPosition);

            var rookToPosition = toTile.Position;
            rookToPosition.X -= 1;
            var rookToTile = board.TileAt(rookToPosition);

            Action forward = () =>
            {
                board.Move(fromTile, toTile);
                board.Move(rookFromTile, rookToTile);
            };
            Action backward = () =>
            {
                board.Move(toTile, fromTile);
                board.Move(rookToTile, rookFromTile);
            };

            var replayCommand = new DelegateReplayCommand(forward, backward);
            ReplayManager.Execute(replayCommand);
        }

        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece)
        {
            var tile = board.TileOf(piece);

            var targetPosition = tile.Position;
            targetPosition.X += 2;

            var targetTile = board.TileAt(targetPosition);

            return new List<Tile> { targetTile };

        }
    }
}
