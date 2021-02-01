using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Transition<T>
    {
        public Enum FromState;
        public Enum ToState;
        public List<Func<T, TryGuardResponse>> Guards;
        public Action<T> Effect;
    }

    public class StateMachine<T>
    {
        Enum InitialState;

        List<Transition<T>> Transitions = new List<Transition<T>>();
        //public StateMachine()
        //{


        //}

        public void SetStateForObject<StateEnum>(T entity, Enum state) where StateEnum : struct
        {
            IStateObject e = entity as IStateObject;
            Transition<T> transition = Transitions.Where(t => t.FromState.Equals(StateUtils.GetStateStringAsEnum<StateEnum>(e.GetState())) && t.ToState.Equals(state)).FirstOrDefault();

            if (transition != null)
            {
                if (transition.Guards != null)
                {
                    foreach (Func<T, TryGuardResponse> guard in transition.Guards)
                    {
                        TryGuardResponse tgr = guard.Invoke(entity);
                        if (!tgr.Success)
                        {
                            throw new Exception("SetStateFailedError");
                        }
                    }
                }
                e.SetState(state.ToString());

                //Effect
                if (transition.Effect != null)
                    transition.Effect.Invoke(entity);
            }
            else
                throw new Exception("Could not find transition from state " + e.GetState() + " to state " + state.ToString());
        }

        public bool TrySetStateForObject<StateEnum>(T entity, Enum state, out List<KeyValuePair<string, string>> errorList) where StateEnum : struct
        {
            errorList = new List<KeyValuePair<string, string>>();
            IStateObject e = entity as IStateObject;
            Transition<T> transition = Transitions.Where(t => t.FromState.Equals(StateUtils.GetStateStringAsEnum<StateEnum>(e.GetState())) && t.ToState.Equals(state)).FirstOrDefault();

            if (transition != null)
            {
                if (transition.Guards != null)
                {
                    foreach (Func<T, TryGuardResponse> guard in transition.Guards)
                    {
                        TryGuardResponse tgr = guard.Invoke(entity);
                        if (!tgr.Success)
                        {
                            errorList.Add(tgr.Error);
                        }
                    }
                }
                if (errorList.Count == 0)
                {
                    e.SetState(state.ToString());

                    //Effect
                    if (transition.Effect != null)
                        transition.Effect.Invoke(entity);
                    return true;
                }
            }
            else
            {
                errorList.Add(new KeyValuePair<string, string>("NoTransitionError", "Could not find transition from state " + e.GetState() + " to state " + state.ToString()));
            }
            return false;
        }

        public bool CanSetState<StateEnum>(T entity, Enum state) where StateEnum : struct
        {
            IStateObject e = entity as IStateObject;
            Transition<T> transition = Transitions.Where(t =>
            t.FromState.Equals(StateUtils.GetStateStringAsEnum<StateEnum>(e.GetState())) && t.ToState.Equals(state)).FirstOrDefault();

            if (transition != null)
            {
                if (transition.Guards != null)
                {
                    foreach (Func<T, TryGuardResponse> guard in transition.Guards)
                    {
                        TryGuardResponse tgr = guard.Invoke(entity);
                        if (!tgr.Success)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public void InitializeObjectState(T entity)
        {
            (entity as IStateObject).SetState(InitialState.ToString());
        }

        public void DefineTransition(Enum fromState, Enum toState, List<Func<T, TryGuardResponse>> guards = null, Action<T> effect = null)
        {
            var transition = new Transition<T>();
            transition.FromState = fromState;
            transition.ToState = toState;
            transition.Guards = guards;
            transition.Effect = effect;
            Transitions.Add(transition);
        }
        public void DefineInitialState(Enum state)
        {
            InitialState = state;
        }
    }

    public static class StateUtils
    {
        public static T GetStateStringAsEnum<T>(string state) where T : struct
        {
            Enum.TryParse(state, false, out T s);
            return s;
        }
    }

    public class TryGuardResponse
    {
        public TryGuardResponse(bool success, KeyValuePair<string, string> error = default)
        {
            Success = success;
            Error = error;
        }

        public bool Success { get; private set; }
        public KeyValuePair<string, string> Error { get; private set; }
    }
}