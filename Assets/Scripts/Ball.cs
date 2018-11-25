using System.Collections;
using UnityEngine;

//Required rigidbody component to the gameobject to attach this script
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour {

    //Reset time before the ball is put back on the penalty point
    [SerializeField]
    private int _resetBallPositionTime;
    //Base force for shooting in the z direction
    [SerializeField]
    private int _throwForceZ;

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
    private Vector2 _direction;
    //StartPosition of the ball and the position the ball has to be set back to after x amount of seconds
    private Vector3 _spawnPosition;
    //Rigidbody attached to this gameobject
    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        //gets the rigidbody that is attached to this gameobject
        _rigidbody = GetComponent<Rigidbody>();
        //set starting position of the ball so we can respawn it correctly
        _spawnPosition = gameObject.transform.position;
	}
	// Update is called once per frame, use FixedUpdate for updating rigidbodies
	void FixedUpdate () {
        ShootBall();
	}
    //Shoot the ball with a force and direction
    private void ShootBall()
    {
        //press left mouse button or start pressing the screen
        if(GameManager.instance.GetGameState() == GameState.idle)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                //set start time and position for swipping
                _touchTimeStart = Time.time;
                if (!Application.isEditor)
                    _startPosition = Input.GetTouch(0).position;
                else
                    _startPosition = Input.mousePosition;
            }
            //release left mouse button or release finger from screen
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                GameManager.instance.SetGameState(GameState.shooting);

                //set end time and end position for swipping
                _touchTimeEnd = Time.time;
                if (!Application.isEditor)
                    _endPosition = Input.GetTouch(0).position;
                else
                    _endPosition = Input.mousePosition;

                //calculate diffrence between end and start time
                _interval = _touchTimeEnd - _touchTimeStart;
                //calculate diffrence between start and end position of swipping
                _direction = _startPosition - _endPosition;

                _rigidbody.isKinematic = false;
                //Add force to the ball
                _rigidbody.AddForce(-_direction.x * 1.0f, -_direction.y * 1.0f, _throwForceZ / _interval);

                //Reset the ball position after x amount of time
                StartCoroutine(ResetBallPosition(_resetBallPositionTime));
            }
        }
    }
    /// <summary>
    /// Reset the ball position after x amount of seconds
    /// </summary>
    /// <param name="seconds">amount of seconds waiting before you reset the ball</param>
    private IEnumerator ResetBallPosition(int timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        _rigidbody.isKinematic = true;
        gameObject.transform.position = _spawnPosition;
        GameManager.instance.SetGameState(GameState.idle);
    }
}
