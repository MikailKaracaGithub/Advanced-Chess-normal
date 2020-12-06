using BoardSystem;
using GameSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultChessPieceViewFactory", menuName = "GameSystem/ChessPieceViewFactory")]
    public class ChessPieceViewFactory : ScriptableObject
    {
        [SerializeField]
        private List<ChessPieceView> _darkChessPieceViews = new List<ChessPieceView>();
        [SerializeField]
        private List<ChessPieceView> _lightChessPieceViews = new List<ChessPieceView>();
        [SerializeField]
        private List<string> _movementNames = new List<string>();
        [SerializeField]
        private PositionHelper _positionHelper = null;


        public ChessPieceView CreateChessPieceView(Board<ChessPiece> board, ChessPiece model, string movementName)
        {
            var list = model.IsLight ? _lightChessPieceViews : _darkChessPieceViews;
            var index = _movementNames.IndexOf(movementName);

            var prefab = list[index];
            var ChessPieceView = GameObject.Instantiate<ChessPieceView>(prefab);

            var tile = board.TileOf(model);


            ChessPieceView.transform.position = _positionHelper.ToWorldPosition(board, tile.Position);
            ChessPieceView.name = $"Spawned ChessPiece ( { movementName} )";
            ChessPieceView.Model = model;

            return ChessPieceView;
        }
    }
}
