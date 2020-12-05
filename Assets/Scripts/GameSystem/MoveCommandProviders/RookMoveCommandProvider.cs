using GameSystem.MoveCommands;

namespace GameSystem.MoveCommandProviders
{
    public class RookMoveCommandProvider : AbstractMoveCommandProvider
    {
        public static readonly string Name = "Rook";

        public RookMoveCommandProvider() : base(new RookBasicMoveCommand()) { }

    }
}
