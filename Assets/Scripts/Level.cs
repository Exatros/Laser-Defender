using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2;

    public void LoadGameOver()
    {

        StartCoroutine(WaitAndLoad());
    }
    
    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");

    }

    public void LoadStage()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
