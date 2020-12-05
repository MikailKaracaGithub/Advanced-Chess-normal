using GameSystem.Models;
using MoveSystem;
using System.Collections.Generic;
using System.Linq;

namespace GameSystem.MoveCommandProviders
{
    public abstract class AbstractMoveCommandProvider : IMoveCommandProvider<ChessPiece>
    {
        private List<IMoveCommand<ChessPiece>> _commands;

        public AbstractMoveCommandProvider(params IMoveCommand<ChessPiece>[] commands)
        {
            _commands = commands.ToList();
        }
        public List<IMoveCommand<ChessPiece>> Commands()
        {
            return _commands;
        }
    }
}
