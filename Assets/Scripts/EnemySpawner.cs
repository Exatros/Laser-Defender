using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    bool lastWave = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        do
        {
            yield return StartCoroutine(SpawnAllWaves());

        } while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            if (waveIndex + 1 == waveConfigs.Count && !looping) { lastWave = true; }
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            if (waveConfig.GetIsDoubleWave())
            {
                CreateEnemy(waveConfig, waveConfig.GetWaypoints());
                CreateEnemy(waveConfig, waveConfig.GetWaypointsFromPath2());
            }
            else
            {
                CreateEnemy(waveConfig, waveConfig.GetWaypoints());

            }


            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }

    }

    private void CreateEnemy(WaveConfig waveConfig, List <Transform> waypoints)
    {
        var newEnemy = Instantiate(
                         waveConfig.GetEnemyPrefab(),
                         waypoints[0].position,
                         Quaternion.identity);
        newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
        newEnemy.GetComponent<EnemyPathing>().SetWaypoints(waypoints);
        FindObjectOfType<GameSession>().AddEnemyCounter();

    }



    // Update is called once per frame
    void Update()
    {
        if(lastWave && FindObjectOfType<GameSession>().GetCountOfEnemy() == 0)
        {
            
            FindObjectOfType<Level>().LoadGameOver(true);
            FindObjectOfType<GameSession>().SetNextStage();
        }
    }
}
