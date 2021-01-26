using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace Game
{
    //[RequireComponent(typeof(GameManager))]
    public class InputController : Singleton
    {
        [SerializeField]
        private Controller _controller;

        protected override void Awake()
        {
            if (GetComponents(typeof(Singleton)).Count() == 1)
            {
                base.Awake();
            }
        }

        public void DirectionalInput(InputAction.CallbackContext context)
        {
            //Debug.Log(context.ReadValue<Vector2>());
            //Debug.Log(context.phase);

            if (context.performed)
            {
                //Log.Info("DirectionalInput performed");
                _controller.DirectionInput(context.ReadValue<Vector2>());
            }
            else if (context.started)
            {
                //Debug.Log("DirectionalInput started");
                _controller.DirectionInputStart(context.ReadValue<Vector2>());
            }
            else if (context.canceled)
            {
                //Debug.Log("DirectionalInput canceled");
                _controller.DirectionInputCancel(context.ReadValue<Vector2>());
            }
        }
    }
}