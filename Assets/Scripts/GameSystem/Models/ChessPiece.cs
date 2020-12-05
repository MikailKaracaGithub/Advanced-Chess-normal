using BoardSystem;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Models
{
    public class ChessPieceMovedEventArgs : EventArgs
    { 
        public Tile From { get; }
        public Tile To { get; }

        public ChessPieceMovedEventArgs(Tile from, Tile to)
        {
            From = from;
            To = to;
        }
    }

    public class ChessPiece : IPiece
    {
        public event EventHandler<ChessPieceMovedEventArgs> ChessPieceMoved;
        public event EventHandler ChessPieceTaken;

       public bool HasMoved { get; set; }
       public bool IsLight { get;}

        public ChessPiece(bool isLight)
        {
            IsLight = isLight;
        }

        void IPiece.Moved(Tile fromTile, Tile toTile)
        {
            OnChessPieceMoved(new ChessPieceMovedEventArgs(fromTile, toTile));
        }
        protected virtual void OnChessPieceMoved(ChessPieceMovedEventArgs arg)
        {
            EventHandler<ChessPieceMovedEventArgs> handler = ChessPieceMoved;
            handler?.Invoke(this, arg);
        }

        void IPiece.Taken()
        {
            OnChessPieceTaken(EventArgs.Empty);
        }
        protected virtual void OnChessPieceTaken(EventArgs arg)
        {
            EventHandler handler = ChessPieceTaken;
            handler?.Invoke(this, arg);
        }
    }
}
