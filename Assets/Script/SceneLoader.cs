using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    const string MAIN_MENU = "Main Menu";
    const string SPLASH_SCREEN = "Splash Screen";
    const string CREDITS_BUTTON = "Credits";
    const string OPTIONS = "Options";
    const string GAME_SCENE = "Game";
    const string GAME_OVER = "Game Over";
    int currentSceneIndex;
    string currentSceneName;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneIndex == 0)
        {
            SplashScreenTimer();
        }
    }

    private void Update()
    {
        if ( currentSceneIndex > 1)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(currentSceneIndex);
            }
        }
        if (currentSceneName == "Credits")
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                SceneManager.LoadScene(MAIN_MENU);
            }
        }
    }
    public void SplashScreenTimer()
    {
        StartCoroutine(WaitMainMenu());
    }

    IEnumerator WaitMainMenu()
    {
        yield return new WaitForSeconds(2);
        MainMenu();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(MAIN_MENU);
    }

    public void SplashScreen()
    {
        SceneManager.LoadScene(SPLASH_SCREEN);
    }

    public void SameSceneLoad()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void GameSceneLoad()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }

    public void NextSceneLoad()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Options()
    {
        SceneManager.LoadScene(OPTIONS);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(GAME_OVER);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene(CREDITS_BUTTON);
    }
}
