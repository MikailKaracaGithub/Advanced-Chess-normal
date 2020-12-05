using GameSystem.Models;
using GameSystem.MoveCommands;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommandProviders
{
    public class PawnMoveCommandProvider : AbstractMoveCommandProvider
    {
        public static readonly string Name = "Pawn";

        public PawnMoveCommandProvider() : base(new PawnBasicMoveCommand() ) {}

    }
}
