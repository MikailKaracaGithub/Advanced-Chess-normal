using BoardSystem;
using GameSystem.Models;
using GameSystem.MoveCommandProviders;
using GameSystem.States;
using GameSystem.Views;
using MoveSystem;
using ReplaySystem;
using StateSystem;
using System;
using System.Collections;
using UnityEngine;
using Utils;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    public event EventHandler Initialized;
    [SerializeField]
    private PositionHelper _positionHelper = null;

    public Board<ChessPiece> Board { get; } = new Board<ChessPiece>(8, 8);

    //public ChessPiece SelectedPiece => _selectedPiece;

    public MoveManager<ChessPiece> MoveManager { get; internal set; }

    private StateMachine<GameStateBase> _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine<GameStateBase>();
        var replayManager = new ReplayManager();
        MoveManager = new MoveManager<ChessPiece>(Board);

        var playGameState = new PlayGameState(Board, MoveManager);
        var replayState = new ReplayGameState(replayManager);

        _stateMachine.RegisterState(GameStates.Play, playGameState);
        _stateMachine.RegisterState(GameStates.Replay, replayState);
        _stateMachine.MoveTo(GameStates.Play);

        MoveManager.Register(PawnMoveCommandProvider.Name, new PawnMoveCommandProvider(playGameState, replayManager));
        MoveManager.Register(KnightMoveCommandProvider.Name, new KnightMoveCommandProvider(playGameState, replayManager));
        MoveManager.Register(KingMoveCommandProvider.Name, new KingMoveCommandProvider(playGameState, replayManager));
        MoveManager.Register(QueenMoveCommandProvider.Name, new QueenMoveCommandProvider(playGameState, replayManager));
        MoveManager.Register(RookMoveCommandProvider.Name, new RookMoveCommandProvider(playGameState, replayManager));
        MoveManager.Register(BishopMoveCommandProvider.Name, new BishopMoveCommandProvider(playGameState, replayManager));



    }

    private void Start()
    {
        ConnectViewsToModel();
        StartCoroutine(PostStart());
    }

    
    public void Select(ChessPiece chessPiece)
    {
        _stateMachine.CurrentState.Select(chessPiece);
    }

    public void Select(Tile tile)
    {
        _stateMachine.CurrentState.Select(tile);
    }


    public void Select(IMoveCommand<ChessPiece> moveCommand)
    {
        _stateMachine.CurrentState.Select(moveCommand);
    }

    public void Forward()
    {
        _stateMachine.CurrentState.Forward();
    }
    public void Backward()
    {
        _stateMachine.CurrentState.Backward();
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