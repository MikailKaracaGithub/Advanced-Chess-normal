using GameSystem.MoveCommands;
using GameSystem.States;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(KnightMoveCommandProvider.Name)]
    public class KnightMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Knight";

        public KnightMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState, new KnightBasicMoveCommand(replayManager)) { }

    }
}
