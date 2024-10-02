using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;
    public int currentLevel = 1;
    public int highestLevel = 2;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public void IncreaseScore(int amount)
    {
        score += amount;

        print("New Score: " + score.ToString());

        if (score > highScore)
        {
            highScore = score;
        }
    }

    public void Reset()
    {
        score = 0;

        currentLevel = 1;

        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void IncreaseLevel()
    {
        if (currentLevel < highestLevel)
        {
            currentLevel++;
        }
        else
        {
            currentLevel = 1;
        }

        SceneManager.LoadScene("Level" + currentLevel);

    }
}
