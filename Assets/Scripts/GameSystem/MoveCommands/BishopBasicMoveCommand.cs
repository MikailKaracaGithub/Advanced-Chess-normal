using System.Collections.Generic;
using BoardSystem;
using GameSystem.Models;
using MoveSystem;

namespace GameSystem.MoveCommands
{
    public class BishopBasicMoveCommand : AbstractBasicMoveCommand
    {
        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece)
        {
            var validTiles = new MovementHelper(board, piece)
               .NorthEast()
               .SouthEast()
               .SouthWest()
               .NorthWest()
               .Generate();

            return validTiles;
        }
    }
}
