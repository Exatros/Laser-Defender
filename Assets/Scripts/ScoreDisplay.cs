using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreDisplay : MonoBehaviour
{

    TextMeshProUGUI scoreText;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Win Stage" || SceneManager.GetActiveScene().name == "Game Over")
        {
            scoreText.text = gameSession.GetScore().ToString() + " PTS";


        }
        else
        {
            scoreText.text = gameSession.GetScore().ToString();

        }
    }
}
