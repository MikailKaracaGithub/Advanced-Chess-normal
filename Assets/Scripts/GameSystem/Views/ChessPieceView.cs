using System;
using GameSystem.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class ChessPieceView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private PositionHelper _positionHelper = null;

        [SerializeField]
        private bool _isLight = true;

        [SerializeField]
        private string _movementName = null;
        public string MovementName => _movementName;

        private ChessPiece _model;
        public ChessPiece Model 
        {
            get => _model;
            internal set
            {
                if (_model != null)
                {
                    _model.ChessPieceMoved -= ModelMoved;
                    _model.ChessPieceTaken -= ModelTaken;
                }

                _model = value;

                if (_model != null)
                {
                    _model.ChessPieceMoved += ModelMoved;
                    _model.ChessPieceTaken += ModelTaken;
                }
            }
        }

        private void ModelTaken(object sender, EventArgs e)
        {
            Destroy(this.gameObject);
        }

        private void ModelMoved(object sender, ChessPieceMovedEventArgs e)
        {
            var worldPosition = _positionHelper.ToWorldPosition(GameLoop.Instance.Board, e.To.Position);
            transform.position = worldPosition;
        }
        private void OnDestroy()
        {
            Model = null;
        }

        public bool IsLight => _isLight;

        public void OnPointerClick(PointerEventData eventData)
        {
            var board = GameLoop.Instance;
            board.Select(Model);
        }
    }
}
