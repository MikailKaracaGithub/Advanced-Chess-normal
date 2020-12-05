using BoardSystem;
using UnityEngine;

namespace GameSystem.Utils
{
    public static class BoardExtensions
    {
        public static Vector3 AsVector3<TPiece>(this Board<TPiece> board) where TPiece : class, IPiece
        {
           return new Vector3(board.Columns, 1, board.Columns);
        }
    }
}
