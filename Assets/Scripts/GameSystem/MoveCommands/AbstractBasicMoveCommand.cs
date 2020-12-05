using System.Collections.Generic;
using BoardSystem;
using GameSystem.Models;
using MoveSystem;

namespace GameSystem.MoveCommands
{
    public abstract class AbstractBasicMoveCommand : IMoveCommand<ChessPiece>
    {
        public void Excecute(Board<ChessPiece> board, ChessPiece piece, Tile toTile)
        {
            var toPiece = board.PieceAt(toTile);

            if (toPiece != null)
            {
                board.Take(toTile);
            }

            var fromTile = board.TileOf(piece);

            board.Move(fromTile, toTile);

            piece.HasMoved = true;
        }

        public abstract List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece);
       
    }
}
