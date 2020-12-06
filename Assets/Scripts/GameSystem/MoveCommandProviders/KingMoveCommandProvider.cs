using GameSystem.MoveCommands;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(KingMoveCommandProvider.Name)]
    public class KingMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "King";

        public KingMoveCommandProvider(ReplayManager replayManager) : base(new KingBasicMoveCommand(replayManager),
            new KingSideCastleMove(replayManager)) { }

    }
}
