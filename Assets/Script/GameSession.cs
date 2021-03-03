using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    Player player;
    
    int currentSceneIndex;

    private void Start()
    {
        pauseCanvas.SetActive(false);
        player = FindObjectOfType<Player>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;       
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
        }
    }

    public void StartDeathSequance()
    {
        Destroy(player.gameObject);
        StartCoroutine(WaitForNextScene());
    }

    IEnumerator WaitForNextScene()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void PauseCanvasDisabler()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }



  





}
