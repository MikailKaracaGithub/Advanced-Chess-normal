using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardSystem;
using GameSystem.Models;
using ReplaySystem;

namespace GameSystem.MoveCommands
{
    public class PawnFirstMoveCommand : AbstractBasicMoveCommand
    {
        public PawnFirstMoveCommand(ReplayManager replayManager) : base(replayManager)
        {

        }

        public override bool CanExecute(Board<ChessPiece> board, ChessPiece piece)
        {
            if (piece.HasMoved)
                return false;
            var tile = board.TileOf(piece);
            var position = tile.Position;

            position.Y += (piece.IsLight) ? 1 : -1;
            var tilePlus1 = board.TileAt(position);
            if (tilePlus1 == null)
                return false;

            var piecePlus1 = board.PieceAt(tilePlus1);
            if (piecePlus1 != null)
                return false;
            position.Y += (piece.IsLight) ? 1 : -1;
            var tilePlus2 = board.TileAt(position);
            if (tilePlus2 == null)
                return false;

            var piecePlus2 = board.PieceAt(tilePlus2);
            if (piecePlus2 != null)
                return false;

            return true;
        }

        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece)
        {
            var tile = board.TileOf(piece);
            var position = tile.Position;

            position.Y += (piece.IsLight) ? 2 : -2;

            var tilePlus2 = board.TileAt(position);

            return new List<Tile>() { tilePlus2 };
        }
    }
}
