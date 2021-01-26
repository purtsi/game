using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Game
{
    public abstract class Singleton : MonoBehaviour
    {
        public static Singleton Instance { get; private set; }

        virtual protected void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
        }
    }
}