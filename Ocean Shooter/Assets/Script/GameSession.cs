using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    [SerializeField] int bonusPointValue = 500;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void AddBonusScore(int bonusPointValue)
    {
        score += bonusPointValue;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }

    public void AddBonusPoints()
    {
        score += bonusPointValue;
    }

}
