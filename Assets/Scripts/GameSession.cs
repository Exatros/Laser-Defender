using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    int score = 0;
    [SerializeField] int countOfEnemy = 0;
    int nextScene = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        if (numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetNextStage()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public int GetNextStage()
    {
        return nextScene;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetCountOfEnemy()
    {
        return countOfEnemy;
    }

    public void AddEnemyCounter()
    {
        countOfEnemy++;
    }

    public void DecraseEnemyCounter()
    {
        countOfEnemy--;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
