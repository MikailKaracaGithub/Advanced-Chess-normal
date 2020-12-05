using GameSystem.Views;
using UnityEditor;
using UnityEngine;

namespace BoardSystem.Editor { 
[CustomEditor(typeof(BoardView))]
public class BoardViewEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Board"))
        {
                var boardView = target as BoardView;

                var tileViewFactorySP = serializedObject.FindProperty("_tileViewFactory"); // accessing whatever was serialized in the property
                var tileViewFactory = tileViewFactorySP.objectReferenceValue as TileViewFactory;

                var game = GameLoop.Instance;
                var board = game.Board;

                foreach (var tile in board.Tiles)
                {
                    tileViewFactory.CreateTileView(board, tile, boardView.transform);
                }

        }
    }
}

}