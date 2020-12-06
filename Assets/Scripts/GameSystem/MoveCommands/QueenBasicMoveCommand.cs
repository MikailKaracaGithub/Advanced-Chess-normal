using System.Collections.Generic;
using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using ReplaySystem;

namespace GameSystem.MoveCommands
{
    public class QueenBasicMoveCommand : AbstractBasicMoveCommand
    {
        public QueenBasicMoveCommand(ReplayManager replayManager) : base(replayManager)
        {
        }

        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece)
        {
            var validTiles = new MovementHelper(board, piece)
                .North()
                .NorthEast()
                .SouthEast()
                .South()
                .SouthWest()
                .West()
                .NorthWest()
                .Generate();

            return validTiles;
                
        }

    }
}
