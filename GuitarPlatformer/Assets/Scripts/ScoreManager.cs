using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public Canvas UICanvas;
    public string scorePrefix = "Score:";

    private uint playerScore;
    private string scoreText;
    private Vector2 scorePosition;
    private GameObject textChild;
  
    /// <summary>
    /// Creates the score manager.
    /// </summary>
	void Start ()
    {
        Instantiate(UICanvas);
        UICanvas.name = "UI Canvas";

        playerScore = 0;
        scoreText = scorePrefix + playerScore;
        scorePosition = Vector2.zero;
        textChild = UICanvas.transform.GetChild(0).gameObject;
        textChild.name = "Score Text";

        IncrementScore(0);
	}

    /// <summary>
    /// Updates the score manager.
    /// </summary>
    private void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Increments the player score.
    /// </summary>
    /// <param name="score">The amount of points being added to the score.</param>
    void IncrementScore(uint score)
    {
        playerScore += score;
        scoreText = scorePrefix + " " + playerScore;
        SetScoreText(scoreText);
    }

    /// <summary>
    /// Sets the text of the score textbox.
    /// </summary>
    /// <param name="text"></param>
    void SetScoreText(string text)
    {
        textChild.GetComponent<UnityEngine.UI.Text>().text = text;
    }
}
