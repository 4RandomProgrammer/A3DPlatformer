using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{

    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + GameManager.instance.score.ToString();
        highScore.text = "High score: " + GameManager.instance.highScore.ToString();
    }

    public void RestartGame()
    {
        GameManager.instance.Reset();
    }
}
