using GameSystem.MoveCommands;
using GameSystem.States;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(QueenMoveCommandProvider.Name)]
    public class QueenMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Queen";

        public QueenMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState, new QueenBasicMoveCommand(replayManager)) { }

    }
}
