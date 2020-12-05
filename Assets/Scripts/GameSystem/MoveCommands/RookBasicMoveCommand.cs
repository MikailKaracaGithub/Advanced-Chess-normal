using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using System;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    public class RookBasicMoveCommand : AbstractBasicMoveCommand
    {
        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece)
        {
            var validTiles = new MovementHelper(board, piece)
                 .North()
                 .East()
                 .South()
                 .West()
                 .Generate();

            return validTiles;
        }
    }
}
