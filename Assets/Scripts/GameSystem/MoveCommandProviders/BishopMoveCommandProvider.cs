using GameSystem.MoveCommands;

namespace GameSystem.MoveCommandProviders
{
    public class BishopMoveCommandProvider : AbstractMoveCommandProvider
    {
        public static readonly string Name = "Bishop";

        public BishopMoveCommandProvider() : base(new BishopBasicMoveCommand()) { }

    }
}
