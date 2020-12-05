using GameSystem.MoveCommands;

namespace GameSystem.MoveCommandProviders
{
    public class KnightMoveCommandProvider : AbstractMoveCommandProvider
    {
        public static readonly string Name = "Knight";

        public KnightMoveCommandProvider() : base(new KnightBasicMoveCommand()) { }

    }
}
