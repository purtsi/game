using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                Instance = this as T;
            }
        }
    }
}