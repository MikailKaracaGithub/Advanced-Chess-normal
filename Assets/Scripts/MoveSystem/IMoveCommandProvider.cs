using BoardSystem;
using System.Collections.Generic;

namespace MoveSystem
{
    public interface IMoveCommandProvider<TPiece> where TPiece : class , IPiece 
    {
        List<IMoveCommand<TPiece>> Commands();
    }
}
