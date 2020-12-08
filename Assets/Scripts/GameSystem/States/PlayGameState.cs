using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using System.Linq;
using UnityEngine;

namespace GameSystem.States
{
    public class PlayGameState : GameStateBase
    {
        public bool IsLightTurn { get; internal set; } = true;

        private IMoveCommand<ChessPiece> _currentMoveCommand;
        private MoveManager<ChessPiece> _moveManager;

        private Board<ChessPiece> _board;
        public Board<ChessPiece> Board => _board;

        private ChessPiece _selectedChessPiece = null;
        public ChessPiece SelectedChessPiece => _selectedChessPiece;


        public PlayGameState(Board<ChessPiece> board, MoveManager<ChessPiece> moveManager)
        {
            _moveManager = moveManager;
            _board = board;
        }
        public override void OnEnter()
        {
            _moveManager.MoveCommandProviderChanged += OnMoveCommandProviderChanged;
        }
        public override void OnExit()
        {
            _moveManager.Deactivate();

            _currentMoveCommand = null;
            _selectedChessPiece = null;

            _moveManager.MoveCommandProviderChanged -= OnMoveCommandProviderChanged;
        }
        public override void Select(ChessPiece chessPiece)
        {
            if (chessPiece == null || chessPiece == _selectedChessPiece)
                return;

            if (chessPiece != null && chessPiece.IsLight != IsLightTurn)
            {
                var tile = _board.TileOf(chessPiece);
                Select(tile);
            }
            else
            {
                _moveManager.Deactivate();

                _currentMoveCommand = null;

                _selectedChessPiece = chessPiece;

                _moveManager.ActivateFor(_selectedChessPiece);
            }
        }

        public override void Select(Tile tile)
        {
            if (_selectedChessPiece != null && _currentMoveCommand != null)
            {
                var tiles = _currentMoveCommand.Tiles(_board, _selectedChessPiece);
                if (tiles.Contains(tile))
                {
                    _board.UnHighlight(tiles);

                    _currentMoveCommand.Execute(_board, _selectedChessPiece, tile);

                    _moveManager.Deactivate();

                    _selectedChessPiece = null;
                    _currentMoveCommand = null;

                    IsLightTurn = !IsLightTurn;
                }
            }
        }

        public override void Backward()
        {
            StateMachine.MoveTo(GameStates.Replay);
        }

        public override void Select(IMoveCommand<ChessPiece> moveCommand)
        {
            if (_currentMoveCommand != null)
                _board.UnHighlight(_currentMoveCommand.Tiles(_board, _selectedChessPiece));

            _currentMoveCommand = moveCommand;

            if (_currentMoveCommand != null)
                _board.Highlight(_currentMoveCommand.Tiles(_board, _selectedChessPiece));

        }
        private void OnMoveCommandProviderChanged(object sender, MoveCommandProviderChangedEventArgs<ChessPiece> e)
        {
            if (_currentMoveCommand == null)
                return;

            var tiles = _currentMoveCommand.Tiles(_board, _selectedChessPiece);

            _board.UnHighlight(tiles);
        }

    }
}
