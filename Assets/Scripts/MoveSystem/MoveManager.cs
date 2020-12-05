﻿using BoardSystem;
using System.Collections.Generic;
using System.Linq;

namespace MoveSystem
{
    public class MoveManager<TPiece> where TPiece : class, IPiece
    {
        private Dictionary<string, IMoveCommandProvider<TPiece>> _providers = new Dictionary<string, IMoveCommandProvider<TPiece>>();
        private Dictionary<TPiece, string> _pieceMovements = new Dictionary<TPiece, string>();
        private IMoveCommandProvider<TPiece> _activeProvider;
        private Board<TPiece> _board;
        private List<Tile> _validTiles = new List<Tile>();

        public MoveManager(Board<TPiece> board)
        {
            _board = board;
        }

        public void Register(string name, IMoveCommandProvider<TPiece> provider)
        {
            if (_providers.ContainsKey(name))
                return;

            _providers.Add(name, provider);

        }

        public void Register(TPiece piece, string name)
        {
            if (_pieceMovements.ContainsKey(piece))
                return;
            else
                _pieceMovements.Add(piece, name);
        }

        public IMoveCommandProvider<TPiece> Provider(TPiece piece)
        {
            if (piece == null) return null;

            if (_pieceMovements.TryGetValue(piece, out var name))
            {
                if (_providers.TryGetValue(name, out var moveCommandProvider))
                    return moveCommandProvider;
            }

            return null;
        }

        public void ActivateFor(TPiece currentPiece)
        {
                _activeProvider = Provider(currentPiece);
                _validTiles = _activeProvider.Commands().SelectMany((command) => command.Tiles(_board, currentPiece)).ToList(); // ISSUE IN HERE
        }

        public void Deactivate()
        {
            _validTiles.Clear();
            _activeProvider = null;
        }

        public void Execute(TPiece piece, Tile tile)
        {
            if (_validTiles.Contains(tile))
            {
                var foundCommand = _activeProvider.Commands().Find((command) => command.Tiles(_board, piece).Contains(tile));
            
                if (foundCommand != null)
                {
                    foundCommand.Excecute(_board, piece, tile);
    
                    _activeProvider = null;
                }
            }
        }

        public List<Tile> Tiles()
        {
            return _validTiles;
        }
    }
}