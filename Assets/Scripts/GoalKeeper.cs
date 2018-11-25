using UnityEngine;

public class GoalKeeper : MonoBehaviour {

    //movement speed of the keeper
    [SerializeField]
    private float _speed;

    //reference to the ball so we can calculate where the keeper has to move to
    private GameObject _ball;
    //Position the goalkeeper needs to move
    private Vector3 _targetPosition;
    //Start position of the goal keeper
    private Vector3 _spawnPosition;

	// Use this for initialization
	void Start () {
        //find the gameobject with the tag ball and set the reference
        _ball = GameObject.FindGameObjectWithTag("Ball");
        //set startposition of the gameobject
        _spawnPosition = transform.position;
        //add this method to a listerner and gets called whenever the game state changes
        GameManager.instance.changeGameState.AddListener(SetBasePosition);
	}
	// Update is called once per frame
	void Update () {
        //check if the game is in the shooting state
		if(GameManager.instance.GetGameState() == GameState.shooting)
        {
            //set the target position
            _targetPosition.Set(_ball.transform.position.x, transform.position.y, transform.position.z);
            //translate to that target position
            gameObject.transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
	}
    //reset the keeper position back to it's starting position
    private void SetBasePosition()
    {
        if(GameManager.instance.GetGameState() == GameState.idle)
        {
            transform.position = _spawnPosition;
        }
    }
}
