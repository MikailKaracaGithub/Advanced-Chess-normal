using System;
using BoardSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    [SelectionBase]
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        
        [SerializeField]
        private PositionHelper _positionHelper = null;

        [SerializeField]
        private Material _highlightMaterial = null;
        private Material _originalMaterial = null;
        private MeshRenderer _meshRenderer = null;

        private Tile _model;

        private void Start()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _originalMaterial = _meshRenderer.sharedMaterial;

            GameLoop.Instance.Initialized += OnGameInitialized;
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            var board = GameLoop.Instance.Board;
            var boardPosition = _positionHelper.ToBoardPosition(board, transform.position);
            var tile = board.TileAt(boardPosition);

            Model = tile;
        }

        public Tile Model
        {
            get => _model;
            set
            {
                if (_model != null)
                    _model.HighlightStatusChanged -= ModelHighlightStatusChanged;

                _model = value;

                if(_model != null)
                _model.HighlightStatusChanged += ModelHighlightStatusChanged;
            }
        }

        private void ModelHighlightStatusChanged(object sender, EventArgs e)
        {
            if (Model.IsHighlighted)
                _meshRenderer.material = _highlightMaterial;
            else
                _meshRenderer.material = _originalMaterial;

        }

        internal Vector3 Size { 
            set
            {
                transform.localScale = Vector3.one;

                var meshRenderer = GetComponentInChildren<MeshRenderer>();
                var meshSize = meshRenderer.bounds.size;

                var ratioX = value.x / meshSize.x;
                var ratioY = value.y / meshSize.y;
                var ratioZ = value.z / meshSize.z;

                transform.localScale = new Vector3(ratioX, ratioY, ratioZ);

            }

        }

        public void OnPointerClick(PointerEventData eventData)
        {
           
            GameLoop.Instance.Select(Model);
        }
        private void OnDestroy()
        {
            Model = null;   
        }
    }
}
