using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private MovableObject _activeMovableObject;

        [SerializeField]
        private InputController _inputController;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DirectionInputStart(Vector2 direction)
        {
            _activeMovableObject.StartMovement(direction);
        }

        public void DirectionInput(Vector2 direction)
        {
            _activeMovableObject.ChangeDirection(direction);
        }

        public void DirectionInputCancel(Vector2 direction)
        {
            if (direction == Vector2.zero)
                _activeMovableObject.CancelMovement(direction);
        }
    }
}