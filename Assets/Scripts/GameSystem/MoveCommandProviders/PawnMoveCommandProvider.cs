using GameSystem.MoveCommands;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(PawnMoveCommandProvider.Name)]
    public class PawnMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Pawn";

        public PawnMoveCommandProvider(ReplayManager replayManager) : base(new PawnBasicMoveCommand(replayManager), new PawnFirstMoveCommand(replayManager) ) {}

    }
}
