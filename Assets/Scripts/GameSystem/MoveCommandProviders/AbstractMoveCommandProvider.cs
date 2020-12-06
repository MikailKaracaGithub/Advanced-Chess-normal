using GameSystem.Models;
using GameSystem.States;
using MoveSystem;
using System.Collections.Generic;
using System.Linq;

namespace GameSystem.MoveCommandProviders
{
    public abstract class AbstractMoveCommandProvider : IMoveCommandProvider<ChessPiece>
    {
        private readonly PlayGameState _playGameState;
        private List<IMoveCommand<ChessPiece>> _commands;

        public AbstractMoveCommandProvider(PlayGameState playGameState, params IMoveCommand<ChessPiece>[] commands)
        {
            _commands = commands.ToList();
            this._playGameState = playGameState;
        }
        public List<IMoveCommand<ChessPiece>> Commands()
        {
            return _commands.Where((command) => command.CanExecute(_playGameState.Board,_playGameState.SelectedChessPiece)).ToList();
        }
    }
}
