using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using ReplaySystem;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    public class PawnBasicMoveCommand : AbstractBasicMoveCommand
    {
        public PawnBasicMoveCommand(ReplayManager replayManager) : base(replayManager)
        {

        }

        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece)
        {
            var validTiles = new MovementHelper(board, piece)
               .North(1, MovementHelper.IsEmpty)
               .NorthEast(1, MovementHelper.CanCapture)
               .NorthWest(1, MovementHelper.CanCapture)
               .Generate();

            return validTiles;
        }
    }

}
