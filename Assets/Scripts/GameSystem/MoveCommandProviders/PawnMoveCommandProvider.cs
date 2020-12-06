using GameSystem.MoveCommands;
using GameSystem.States;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(PawnMoveCommandProvider.Name)]
    public class PawnMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Pawn";

        public PawnMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState, new PawnBasicMoveCommand(replayManager), new PawnFirstMoveCommand(replayManager) ) {}

    }
}
