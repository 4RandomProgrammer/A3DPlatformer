using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else if( instance != this)
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
}
