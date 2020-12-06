using GameSystem.MoveCommands;
using GameSystem.States;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(KingMoveCommandProvider.Name)]
    public class KingMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "King";

        public KingMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState, new KingBasicMoveCommand(replayManager),
            new KingSideCastleMove(replayManager)) { }

    }
}
