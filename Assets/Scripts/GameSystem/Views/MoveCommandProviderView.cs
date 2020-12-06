using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem.Models;
using MoveSystem;
using UnityEngine;

namespace GameSystem.Views
{
    public class MoveCommandProviderView : MonoBehaviour
    {
        [SerializeField]
        private MoveCommandView _moveCommandView = null;

        private List<MoveCommandView> _moveCommandViews = new List<MoveCommandView>();

        private void Start()
        {
            GameLoop.Instance.Initialized += OnGameInitialized;
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            var MoveManager = GameLoop.Instance.MoveManager;
            MoveManager.MoveCommandProviderChanged += OnMoveManagerProviderChanged;
        }

        private void OnMoveManagerProviderChanged(object sender, MoveCommandProviderChangedEventArgs<ChessPiece> e)
        {
            foreach (var moveCommand in _moveCommandViews)
            {
                GameLoop.Destroy(moveCommand.gameObject);
            }

            _moveCommandViews.Clear();

            var moveCommandProvider = e.MoveCommandProvider;

            if(moveCommandProvider != null)
            {
                var board = GameLoop.Instance.Board;
                var piece = GameLoop.Instance.SelectedPiece;

                var moveCommands = moveCommandProvider.Commands();
                foreach (var moveCommand in moveCommands)
                {
                    if (!moveCommand.CanExecute(board, piece))
                        continue;

                    var view = GameObject.Instantiate(_moveCommandView, transform);

                    view.Model = moveCommand;


                    _moveCommandViews.Add(view);
                }
            }
        }
    }
}
