using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    TextMeshProUGUI timerText;
    EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner.GetLooping())
        {
            timerText = GetComponent<TextMeshProUGUI>();

        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawner.GetTimer() >= 0)
        {
            timerText.text = enemySpawner.GetTimer().ToString("F2");

        }
        else
        {
            timerText.text = 0.ToString("F2");

        }
    }
}
