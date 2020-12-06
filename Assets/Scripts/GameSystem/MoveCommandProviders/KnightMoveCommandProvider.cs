using GameSystem.MoveCommands;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(KnightMoveCommandProvider.Name)]
    public class KnightMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Knight";

        public KnightMoveCommandProvider(ReplayManager replayManager) : base(new KnightBasicMoveCommand(replayManager)) { }

    }
}
