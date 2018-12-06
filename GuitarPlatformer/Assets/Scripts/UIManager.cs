using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    #region Public
    [Header("The text before the score display.")]
    public string scorePrefix;       //The text before the score display
    [Header("The 2D Canvas Prefab.")]
    public GameObject canvas;        //The canvas object being initialized
    #endregion

    #region Private
    private int score;               //The integer score count
    private static string scoreText; //The text of the score text object
    #endregion

    /// <summary>
    /// Initializes the UI Manager.
    /// </summary>
    void Start () {
        score = 0;
        setScoreText(); //Set the score text
        canvas = GameObject.Instantiate(canvas);
        canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "DEFAULT_TEXT";
        setScoreText();
    }

    /// <summary>
    /// Updates the UI Manager. (For testing purposes ATM)
    /// </summary>
    void Update()
    {
        if(true) // Set to FALSE
        // DEBUGGING -----------------------------------------
        if (Input.GetKeyDown(KeyCode.UpArrow))        Score++;  
        else if (Input.GetKeyDown(KeyCode.DownArrow)) Score--; 
        // ---------------------------------------------------
    }

    /// <summary>
    /// Sets the score string.
    /// </summary>
    /// <param name="s">An optional parameter for a custom score text.</param>
    /// <returns></returns>
    public bool setScoreText(string s = "")
    {
        scoreText = (s == "") ? scorePrefix + " " + score : s;
        if (scoreText == "")
        {
            //Debug.Log("String Error!");
            return false;
        }
        canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = scoreText;
        return true;
    }

    #region Properties
    /// <summary>
    /// Returns the score and increments the score counter.
    /// </summary>
    public int Score
    {
        get { return score; }
        set
        {
            //Debug.Log("Changing score count... SCORE: " + score);
            score = value;
            setScoreText();
        }
    }
    #endregion
}
