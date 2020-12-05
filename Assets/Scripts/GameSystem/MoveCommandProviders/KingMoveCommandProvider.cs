using GameSystem.MoveCommands;

namespace GameSystem.MoveCommandProviders
{
    public class KingMoveCommandProvider : AbstractMoveCommandProvider
    {
        public static readonly string Name = "King";

        public KingMoveCommandProvider() : base(new KingBasicMoveCommand()) { }

    }
}
