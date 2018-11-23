using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Required rigidbody component to the gameobject to attach this script
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour {

    //Reset time before the ball is put back on the penalty point
    [SerializeField]
    private int resetBallPositionTime;
    //Base force for shooting in the z direction
    [SerializeField]
    private float _throwForceZ;

    //Time you start Swipping
    private float _touchTimeStart;
    //Time you stopped swipping
    private float _touchTimeEnd;
    //Diffrence between start time and end time
    private float _interval;

    //StartPostion when you start start pressing the screen
    private Vector2 _startPosition;
    //End position when you release your finger from the screen
    private Vector2 _endPosition;
    //Direction the ball has to move
    private Vector3 _direction;
    //Rigidbody attached to this gameobject
    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        //gets the rigidbody that is attached to this gameobject
        _rigidbody = GetComponent<Rigidbody>();

        _startPosition = new Vector2(0, 0);
        _endPosition = new Vector2(0, 0);
	}
	// Update is called once per frame, use FixedUpdate for updating rigidbodies
	void FixedUpdate () {
        ShootBall();
	}
    /// <summary>
    /// Reset the ball position after x amount of seconds
    /// </summary>
    /// <param name="seconds">amount of seconds waiting before you reset the ball</param>
    private void ResetBallPosition(int seconds)
    {

    }
    //Shoot the ball with a force and direction
    private void ShootBall()
    {
        //press left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            //set start time and position for swipping
            _touchTimeStart = Time.time;
            _startPosition = Input.mousePosition;
        }
        //release left mouse button
        if (Input.GetMouseButtonUp(0))
        {
            GameManager.instance.SetGameState(GameState.shooting);

            //set end time and end position for swipping
            _touchTimeEnd = Time.time;
            _endPosition = Input.mousePosition;

            //calculate diffrence between end and start time
            _interval = _touchTimeEnd - _touchTimeStart;

            //calculate diffrence between start and end position of swipping
            _direction = _startPosition - _endPosition;

            _rigidbody.isKinematic = false;
            //Add force to the ball
            _rigidbody.AddForce(-_direction.x * _interval, -_direction.y * _interval, _throwForceZ / _interval, ForceMode.Force);

            //Reset the ball position after x amount of time
            ResetBallPosition(5);
        }
    }
}
