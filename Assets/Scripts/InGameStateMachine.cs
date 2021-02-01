using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    public partial class GameManager : IStateObject
    {
        private string State;

        private static Lazy<StateMachine<GameManager>> instance = new Lazy<StateMachine<GameManager>>(Register);
        public static readonly StateMachine<GameManager> StateMachineInstance = instance.Value;

        //public GameManager()
        //{
        //    StateMachineInstance.InitializeObjectState(this);
        //}

        public enum States_InGame
        {
            Active,
            Paused
        }

        string IStateObject.GetState()
        {
            return this.State;
        }

        void IStateObject.SetState(string state)
        {
            this.State = state;
        }

        private static StateMachine<GameManager> Register()
        {
            if (StateMachineInstance != null)
            {
                Debug.Log(StateMachineInstance);
            }
            StateMachine<GameManager> stateMachine = new StateMachine<GameManager>();
            stateMachine.DefineInitialState(States_InGame.Active);

            stateMachine.DefineTransition(
                fromState: States_InGame.Active,
                toState: States_InGame.Paused
            );

            stateMachine.DefineTransition(
                fromState: States_InGame.Paused,
                toState: States_InGame.Active
            );

            return stateMachine;
        }
    }
}