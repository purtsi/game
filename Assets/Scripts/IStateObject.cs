using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    interface IStateObject
    {
        void SetState(string state);
        string GetState();
    }
}