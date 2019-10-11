using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2;

    private void Start()
    {
        Screen.SetResolution(540, 960, false);
    }

    public void LoadNextStage()
    {
        SceneManager.LoadScene("Stage " + FindObjectOfType<GameSession>().GetNextStage().ToString());
    }

    public void LoadGameOver(bool isPlayerWin)
    {
        StartCoroutine(WaitAndLoad(isPlayerWin));
    }


    private IEnumerator WaitAndLoad(bool isPlayerWin)
    {
        yield return new WaitForSeconds(delayInSeconds);
        if (isPlayerWin)
        {
            SceneManager.LoadScene("Win Stage");

        }
        else
        {
            SceneManager.LoadScene("Game Over");

        }

    }

    public void LoadSelectMenu()
    {
        SceneManager.LoadScene("Select Menu");
    }

    public void LoadStage(int nr)
    {
        SceneManager.LoadScene(nr);
    }

    public void LoadFirstStage()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void LoadStageOver()
    {
        SceneManager.LoadScene("Stage " + (FindObjectOfType<GameSession>().GetNextStage() - 1).ToString());
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
