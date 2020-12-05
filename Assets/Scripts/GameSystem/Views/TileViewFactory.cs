using BoardSystem;
using GameSystem.Models;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultTileViewFactory", menuName = "GameSystem/TileViewFactory")]
    public class TileViewFactory : ScriptableObject
    {
        [SerializeField]
        private TileView _darkTileView = null;
        [SerializeField]
        private TileView _lightTileView = null;

        [SerializeField]
        private PositionHelper _positionHelper = null;

        public TileView CreateTileView(Board<ChessPiece> board, Tile tile, Transform parent)
        { 
            var position = _positionHelper.ToWorldPosition(board, tile.Position);

            var prefab = ((tile.Position.X + tile.Position.Y) % 2) == 0 ? _darkTileView : _lightTileView;
            var tileView = GameObject.Instantiate(prefab, position, Quaternion.identity, parent);

            tileView.Size = _positionHelper.TileSize;
            tileView.name = $"Tile {(char)(65 + tile.Position.X)}{tile.Position.X}";

            //tileView.Model = tile;


            return tileView;
        }
    }
}
