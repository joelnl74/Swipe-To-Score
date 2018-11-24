using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    idle,
    shooting,
}

public class GameManager : MonoBehaviour {

    //singleton so that each object can check the state of the game
    public static GameManager instance;
    //gets called whenever the gamestate changes, unity implementation of a observer pattern
    public UnityEvent changeGameState;
    //state the entire game is in
    private GameState gameState;

	// Use this for initialization
	void Awake () {
        if(instance == null)
        {
            instance = this;
            gameState = GameState.idle;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
    //Get the current game state
    public GameState GetGameState()
    {
        return gameState;
    }
    //Set the current game state
    public void SetGameState(GameState state)
    {
        gameState = state;
        changeGameState.Invoke();
    }
}
