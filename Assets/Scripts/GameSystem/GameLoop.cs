using BoardSystem;
using GameSystem.Models;
using GameSystem.MoveCommandProviders;
using GameSystem.Views;
using MoveSystem;
using ReplaySystem;
using System;
using System.Collections;
using UnityEngine;
using Utils;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    public event EventHandler Initialized;
    [SerializeField]
    private PositionHelper _positionHelper = null;

    private ChessPiece _selectedPiece = null;
    public bool IsLightTurn { get; internal set; } = true;

    public Board<ChessPiece> Board { get; } = new Board<ChessPiece>(8, 8);

    private IMoveCommand<ChessPiece> _currentMoveCommand;

    public ChessPiece SelectedPiece => _selectedPiece;

    public MoveManager<ChessPiece> MoveManager { get; internal set; }

    public ReplayManager ReplayManager { get; set; } = new ReplayManager();

    private void Awake()
    {
        MoveManager = new MoveManager<ChessPiece>(Board);
        MoveManager.Register(PawnMoveCommandProvider.Name, new PawnMoveCommandProvider(ReplayManager));
        MoveManager.Register(KnightMoveCommandProvider.Name, new KnightMoveCommandProvider(ReplayManager));
        MoveManager.Register(KingMoveCommandProvider.Name, new KingMoveCommandProvider(ReplayManager));
        MoveManager.Register(QueenMoveCommandProvider.Name, new QueenMoveCommandProvider(ReplayManager));
        MoveManager.Register(RookMoveCommandProvider.Name, new RookMoveCommandProvider(ReplayManager));
        MoveManager.Register(BishopMoveCommandProvider.Name, new BishopMoveCommandProvider(ReplayManager));

        MoveManager.MoveCommandProviderChanged += OnMoveCommandProviderChanged;

    }

    private void Start()
    {
        ConnectViewsToModel();
        StartCoroutine(PostStart());
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

           
            MoveManager.Deactivate();

            _currentMoveCommand = null;

            _selectedPiece = chessPiece;

            MoveManager.ActivateFor(_selectedPiece);

        }
    }

    public void Select(Tile tile)
    {
        if (_selectedPiece != null && _currentMoveCommand != null)
        {
            var tiles = _currentMoveCommand.Tiles(Board, _selectedPiece);
            if (tiles.Contains(tile))
            {
                Board.UnHighlight(tiles);

            _currentMoveCommand.Execute(Board, _selectedPiece, tile);

            MoveManager.Deactivate();

            _selectedPiece = null;
            _currentMoveCommand = null;

            IsLightTurn = !IsLightTurn;
            }
        }
    }
    protected virtual void OnInitialized(EventArgs arg)
    {
        EventHandler handler = Initialized;
        handler?.Invoke(this, arg);
    }
    private IEnumerator PostStart()
    {
        yield return new WaitForEndOfFrame();

        OnInitialized(EventArgs.Empty);
    }

    public void Select(IMoveCommand<ChessPiece> moveCommand)
    {
        if (_currentMoveCommand != null)
            Board.UnHighlight(_currentMoveCommand.Tiles(Board, _selectedPiece));

        _currentMoveCommand = moveCommand;

        if (_currentMoveCommand != null)
            Board.Highlight(_currentMoveCommand.Tiles(Board, _selectedPiece));

    }

    public void Forward()
    {
        ReplayManager.Forward();
    }
    public void Backward()
    {
        ReplayManager.Backward();
    }

    private void OnMoveCommandProviderChanged(object sender, MoveCommandProviderChangedEventArgs<ChessPiece> e)
    {
        if (_currentMoveCommand == null)
            return;

        var tiles = _currentMoveCommand.Tiles(Board, _selectedPiece);

        Board.UnHighlight(tiles);
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



}