using BoardSystem;
using GameSystem.Models;
using GameSystem.MoveCommandProviders;
using GameSystem.Views;
using MoveSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    [SerializeField]
    private PositionHelper _positionHelper = null;

    private ChessPiece _selectedPiece = null;
    public bool IsLightTurn { get; internal set; } = true;
    public Board<ChessPiece> Board { get; } = new Board<ChessPiece>(8, 8);

    public MoveManager<ChessPiece> MoveManager { get; internal set; }

    // PawnBasicMoveCommand _pawnMovement = new PawnBasicMoveCommand();
    private void Awake()
    {
        MoveManager = new MoveManager<ChessPiece>(Board);
        MoveManager.Register(PawnMoveCommandProvider.Name, new PawnMoveCommandProvider());
        MoveManager.Register(KnightMoveCommandProvider.Name, new KnightMoveCommandProvider());
        MoveManager.Register(KingMoveCommandProvider.Name, new KingMoveCommandProvider());
        MoveManager.Register(QueenMoveCommandProvider.Name, new QueenMoveCommandProvider());
        MoveManager.Register(RookMoveCommandProvider.Name, new RookMoveCommandProvider());
        MoveManager.Register(BishopMoveCommandProvider.Name, new BishopMoveCommandProvider());
    }

    private void Start()
    {
        ConnectViewsToModel();
    }

    private void ConnectViewsToModel()
    {
        var pieceViews = FindObjectsOfType<ChessPieceView>();
        foreach (var pieceView in pieceViews)
        {
            var worldPosition = pieceView.transform.position;
            var boardPosition = _positionHelper.ToBoardPosition(Board, worldPosition);

            var tile = Board.TileAt(boardPosition);

            var piece = new ChessPiece(pieceView.IsLight);

            Board.Place(tile, piece);

            pieceView.Model = piece;
            MoveManager.Register(piece, pieceView.MovementName);
        }
    }


    public void Select(ChessPiece chessPiece)
    {
        if (chessPiece == null || chessPiece == _selectedPiece)
            return;

        if (chessPiece != null && chessPiece.IsLight != IsLightTurn)
        {
            var tile = Board.TileOf(chessPiece);
            Select(tile);
        }
        else
        {
            Board.UnHighlight(MoveManager.Tiles());

            _selectedPiece = chessPiece;
            MoveManager.ActivateFor(_selectedPiece);

            Board.Highlight(MoveManager.Tiles());
        }
    }

    public void Select(Tile tile)
    {
        if (_selectedPiece != null)
        {
            MoveManager.Execute(_selectedPiece, tile);

            Board.UnHighlight(MoveManager.Tiles());

            MoveManager.Deactivate();

            _selectedPiece = null;

            IsLightTurn = !IsLightTurn;
        }
    }
}