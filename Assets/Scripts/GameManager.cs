using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace Game
{
    public partial class GameManager : Singleton
    {

        protected override void Awake()
        {
            base.Awake();
            //Debug.Log("ASD");
            //StateMachineInstance.InitializeObjectState(this);
        }

        public static void Initialize()
        {
            //Debug.Log(Instance.name);
        }

        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log("GameManager: Start");
        }

        // Update is called once per frame
        void Update()
        {
            //if (Keyboard.current.enterKey.wasPressedThisFrame)
            //{
            //    if (SceneManager.GetActiveScene().name == "main")
            //    {
            //        SceneManager.LoadScene("test");
            //    }
            //    else
            //        SceneManager.LoadScene("main");
            //}
        }
    }
}