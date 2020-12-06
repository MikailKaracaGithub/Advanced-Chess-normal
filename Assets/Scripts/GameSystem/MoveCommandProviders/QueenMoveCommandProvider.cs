using GameSystem.MoveCommands;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(QueenMoveCommandProvider.Name)]
    public class QueenMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Queen";

        public QueenMoveCommandProvider(ReplayManager replayManager) : base(new QueenBasicMoveCommand(replayManager)) { }

    }
}
