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

        private float _acceleration;
        private float _velocity;
        private float _turnTime;
        private float _hitByVelocity;

        private bool _moving;
        private bool _impactPending;

        private Vector2 _destination;
        private Vector2 _currentDirection;
        private Vector2 _startingPoint;

        private MovableObject _hitByObject;

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
        public virtual void Update()
        {



            //if (_impactPending)
            //{
            //    Debug.Log("Handle impact");
            //    if (_moving)
            //    {
            //        _velocity = Mathf.Abs(_velocity * _mass - _hitByObject._hitByVelocity * _hitByObject._mass);
            //        ChangeDirection(_currentDirection * _hitByObject._currentDirection);
            //    }
            //    else
            //    {
            //        _velocity = Mathf.Abs((_velocity * _mass) - (_hitByObject._hitByVelocity * _hitByObject._mass));
            //        StartMovement(_hitByObject._currentDirection);
            //        CancelMovement(_hitByObject._currentDirection);
            //    }
            //    _impactPending = false;
            //}

            //if (_moving)
            //{
            //    if (_destination != Vector2.zero)
            //        _currentDirection = Vector2.Lerp(_startingPoint, _destination, _turnTime * _turnRate);
            //    if ((_velocity < _maxVelocity && _acceleration > 0) || (_velocity > 0 && _acceleration < 0))
            //    { 
            //        _velocity += _acceleration * Time.deltaTime;
            //    }
            //    if (_velocity <= 0)
            //    {
            //        //Debug.Log("StopMovement");
            //        _currentDirection = Vector2.zero;
            //        _moving = false;
            //        _velocity = 0;
            //    }

            //    _turnTime += Time.deltaTime;
            //    transform.Translate(_currentDirection * _velocity);
            //    _destinationText.text = "Destination: " + _destination.ToString();
            //    _velocityText.text = "Velocity: " + _velocity.ToString() + " " + Mathf.Abs(_velocity * _currentDirection.x).ToString() + " " + Mathf.Abs(_velocity * _currentDirection.y).ToString();
            //    _currentDirectionText.text = "CurrentDirection: " + _currentDirection.ToString();
            //}
        }

        public void StartMovement(Vector2 direction)
        {

            _destination = direction;
            //ChangeDirection(direction);
            //_acceleration = _accelerationRate;
            _moving = true;
        }

        public void CancelMovement(Vector2 direction)
        {
            _destination = direction;
            _acceleration = _decelerationRate * -1;
        }

        public void ChangeDirection(Vector2 direction)
        {
            _turnTime = 0;
            _startingPoint = _currentDirection;
            _destination = direction;
        }

        //private void OnTriggerEnter2D(Collider2D collider)
        //{
        //    Debug.Log("OnTriggerEnter2D");
        //    _impactPending = true;
        //    //MovableObject mo = collider.gameObject.GetComponent<MovableObject>();
        //    _hitByObject = collider.gameObject.GetComponent<MovableObject>();
        //    _hitByVelocity = _hitByObject._velocity;

        //    //Debug.Log("obejct velocity: " + _velocity + " crashed velocity: " + mo._velocity);


        //    //if (_moving)
        //    //{
        //    //    _velocity = Mathf.Abs(_velocity - mo._impactVelocity) * _mass;
        //    //    ChangeDirection(_currentDirection * -mo._currentDirection);
        //    //}
        //    //else
        //    //{
        //    //    _velocity = Mathf.Abs(_velocity - mo._impactVelocity) * _mass;
        //    //    StartMovement(_currentDirection * -mo._currentDirection);
        //    //    CancelMovement(_currentDirection);
        //    //}
        //}

        //private void OnTriggerStay2D(Collider2D collider)
        //{
        //    Debug.Log("OnTriggerStay2D");
        //    if (_moving)
        //    {
        //        _velocity = Mathf.Abs(_velocity - _hitByObject._hitByVelocity) * _mass;
        //        ChangeDirection(_currentDirection * _hitByObject._currentDirection);
        //    }
        //    else
        //    {
        //        _velocity = Mathf.Abs(_velocity - _hitByVelocity) * _mass;
        //        StartMovement(_hitByObject._currentDirection);
        //        CancelMovement(_hitByObject._currentDirection);
        //    }
        //}
    }
}