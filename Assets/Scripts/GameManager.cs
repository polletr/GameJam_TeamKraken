using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject transitionPopUp;
    [SerializeField]
    private PauseController pauseController;

    private int maxLevels = 4;

    private int currentLevel;

    // Use PlayerPrefs para obter e definir o n�vel atual
    private void Awake()
    {

    }

    // Restante do seu c�digo...

    void OnPlayButtonClicked()
    {

    }

    public void LoadNextLevel()
    {
        if (currentLevel < maxLevels)
        {
            currentLevel++;
            SceneManager.LoadScene("Level" + currentLevel);
            Time.timeScale = 1f;
        }
        else
        {
            // O jogador concluiu todos os n�veis
            ShowVictoryScreen();
        }
    }

    public void VictoryScreen()
    {
        Invoke("ShowVictoryScreen", 2f);
    }

    public void ShowVictoryScreen()
    {
        Debug.Log("Congratulations! You completed all levels. Thanks for playing!");
        SceneManager.LoadScene("VictoryScene");
    }
    public void LevelFinished()
    {
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(screenCenter);
        transitionPopUp.transform.position = worldCenter;
        transitionPopUp.SetActive(true);
        Time.timeScale = 0f;
        PauseController.isPaused = true;
    }
}
