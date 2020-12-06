using System;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using ReplaySystem;

namespace GameSystem.MoveCommands
{
    public abstract class AbstractBasicMoveCommand : IMoveCommand<ChessPiece>
    {
       protected ReplayManager ReplayManager;

        protected AbstractBasicMoveCommand(ReplayManager replayManager)
        {
            ReplayManager = replayManager;
        }

        public virtual bool CanExecute(Board<ChessPiece> board, ChessPiece piece)
        {
            var validTIles = Tiles(board, piece);
            return validTIles.Count > 0;
        }

        public virtual void Execute(Board<ChessPiece> board, ChessPiece piece, Tile toTile)
        {
            var toPiece = board.PieceAt(toTile);
            var fromTile = board.TileOf(piece);
            var hasMoved = piece.HasMoved;


            Action forward = () =>
            {
                if (toPiece != null)
                {
                    board.Take(toTile);
                }
                board.Move(fromTile, toTile);

                piece.HasMoved = true;
            };

            Action backward = () =>
            {
                piece.HasMoved = hasMoved;

                board.Move(toTile, fromTile);
                if(toPiece != null)
                {
                    board.Place(toTile, toPiece);
                }
            };

            var replayCommand = new DelegateReplayCommand(forward, backward);
            ReplayManager.Execute(replayCommand);
        }

        public abstract List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece);
       
    }
}
