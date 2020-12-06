using GameSystem.MoveCommands;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(RookMoveCommandProvider.Name)]
    public class RookMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Rook";

        public RookMoveCommandProvider(ReplayManager replayManager) : base(new RookBasicMoveCommand(replayManager)) { }

    }
}
