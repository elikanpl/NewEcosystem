using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckManager : MonoBehaviour
{
    public static DuckManager reference;
    public GameObject score;
    private TMPro.TextMeshProUGUI scoreText;
    private int _currentScore = 0;

    //awake runs before start
    private void Awake()
    {
        reference = this;
    }

    private void Start()
    {
        
        scoreText = score.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void IncrementScore()
    {
        _currentScore += 1;
        scoreText.text = _currentScore.ToString(); // or + ""
    }
}