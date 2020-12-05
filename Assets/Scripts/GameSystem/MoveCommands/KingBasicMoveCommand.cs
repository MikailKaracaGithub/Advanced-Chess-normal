using System.Collections.Generic;
using BoardSystem;
using GameSystem.Models;
using MoveSystem;

namespace GameSystem.MoveCommands
{
    public class KingBasicMoveCommand : AbstractBasicMoveCommand
    {
        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece)
        {
            var validTiles = new MovementHelper(board, piece)
                .North(1)
                .NorthEast(1)
                .SouthEast(1)
                .South(1)
                .SouthWest(1)
                .West(1)
                .NorthWest(1)
                .Generate();

            return validTiles;

        }
    }
}
