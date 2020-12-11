using GameSystem.MoveCommands;
using GameSystem.States;
using GameSystem.Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(RookMoveCommandProvider.Name)]
    public class RookMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Rook";
        public RookMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState, new RookBasicMoveCommand(replayManager)) { }

    }
}
