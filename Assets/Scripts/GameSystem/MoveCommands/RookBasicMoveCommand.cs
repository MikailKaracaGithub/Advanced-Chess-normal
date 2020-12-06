using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using ReplaySystem;
using System;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    public class RookBasicMoveCommand : AbstractBasicMoveCommand
    {
        public RookBasicMoveCommand(ReplayManager replayManager) : base(replayManager)
        {
        }

        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece)
        {
            var validTiles = new MovementHelper(board, piece)
                 .North(8)
                 .East(8)
                 .South(8)
                 .West(8)
                 .Generate();

            return validTiles;
        }
    }
}
