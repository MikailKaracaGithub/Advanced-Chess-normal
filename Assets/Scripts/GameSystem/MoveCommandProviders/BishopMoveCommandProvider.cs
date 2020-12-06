using GameSystem.MoveCommands;
using GameSystem.States;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(BishopMoveCommandProvider.Name)]
    public class BishopMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Bishop";

        public BishopMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState,new BishopBasicMoveCommand(replayManager)) { }

    }
}
