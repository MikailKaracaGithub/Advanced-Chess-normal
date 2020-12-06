using GameSystem.MoveCommands;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(BishopMoveCommandProvider.Name)]
    public class BishopMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Bishop";

        public BishopMoveCommandProvider(ReplayManager replayManager) : base(new BishopBasicMoveCommand(replayManager)) { }

    }
}
