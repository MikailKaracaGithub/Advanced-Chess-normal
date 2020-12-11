using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem.Models;
using MoveSystem;
using UnityEngine;
using Utils;

namespace GameSystem.Views
{
    [RequireComponent(typeof(ObjectPool))]
    public class MoveCommandProviderView : MonoBehaviour
    {
        private List<MoveCommandView> _moveCommandViews = new List<MoveCommandView>();
        private ObjectPool _pool;

        private void Start()
        {
            GameLoop.Instance.Initialized += OnGameInitialized;
            _pool = GetComponent<ObjectPool>();
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
                moveCommand.gameObject.SetActive(false);
            }

            _moveCommandViews.Clear();

            var moveCommandProvider = e.MoveCommandProvider;

            if(moveCommandProvider != null)
            {

                var moveCommands = moveCommandProvider.Commands();

                foreach (var moveCommand in moveCommands)
                {
                    var go = _pool.GetPooledObject();
                    var view = go.GetComponent<MoveCommandView>();

                    view.Model = moveCommand;
                    _moveCommandViews.Add(view);
                }
            }
        }
    }
}
