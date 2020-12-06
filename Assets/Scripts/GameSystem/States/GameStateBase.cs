﻿using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.States
{
    public abstract class GameStateBase : IState<GameStateBase>
    {
        public StateMachine<GameStateBase> StateMachine { set;  get; }

        public virtual void OnEnter(){}

        public virtual void OnExit(){}

        public virtual void Select(ChessPiece chessPiece){}

        public virtual void Select(Tile tile){}

        public virtual void Select(IMoveCommand<ChessPiece> moveCommand){}

        public virtual void Forward() {}
        public virtual void Backward() {}
    }
}
