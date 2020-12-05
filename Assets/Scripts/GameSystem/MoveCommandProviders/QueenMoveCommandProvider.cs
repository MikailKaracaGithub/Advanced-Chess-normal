using GameSystem.MoveCommands;

namespace GameSystem.MoveCommandProviders
{
    public class QueenMoveCommandProvider : AbstractMoveCommandProvider
    {
        public static readonly string Name = "Queen";

        public QueenMoveCommandProvider() : base(new QueenBasicMoveCommand()) { }

    }
}
