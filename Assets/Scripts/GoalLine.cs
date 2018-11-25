using UnityEngine.UI;
using UnityEngine;

public class GoalLine : MonoBehaviour {

    //score text field
    [SerializeField]
    private Text _scoreText;
    //Keeps the current amount of score goals
    private ushort _score;

    private void Start()
    {
        _score = 0;
    }
    //Check if a goal has been scored
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            UpdateScore();
        }
    }
    //Update the score and the ui
    private void UpdateScore()
    {
        _score++;
        _scoreText.text = "Score: " + _score;
    }
}
