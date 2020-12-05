using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveSystem
{
    public interface IMoveCommand<TPiece> where TPiece : class, IPiece
    {
        List<Tile> Tiles(Board<TPiece> board, TPiece piece);

        void Excecute(Board<TPiece> board, TPiece piece, Tile toTile);
    }
}
