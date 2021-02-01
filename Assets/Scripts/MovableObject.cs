using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovableObject : MonoBehaviour
    {
        [SerializeField]
        private Text _velocityText;
        [SerializeField]
        private Text _directionText;
        [SerializeField]
        private Text _currentDirectionText;
        [SerializeField]
        private Text _destinationText;

        [SerializeField]
        private float _maxVelocity = 1.0f;
        [SerializeField]
        private float _accelerationRate;
        [SerializeField]
        private float _decelerationRate;
        [SerializeField]
        private float _turnRate;
        [SerializeField]
        private float _mass;

        private Rigidbody2D _rb;

        private bool _moving;

        private Vector2 _destination;

        // Start is called before the first frame update
        public virtual void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate()
        {
            if (_moving)
            {
                _rb.AddForce(_destination * _accelerationRate, ForceMode2D.Force);
            }
        }

        // Update is called once per frame
        //public virtual void Update()
        //{

        //}

        public void StartMovement(Vector2 direction)
        {

            _destination = direction;
            _moving = true;
        }

        public void CancelMovement(Vector2 direction)
        {
            _destination = direction;
            //_acceleration = _decelerationRate * -1;
        }

        public void ChangeDirection(Vector2 direction)
        {
            //_startingPoint = _currentDirection;
            _destination = direction;
        }
    }
}