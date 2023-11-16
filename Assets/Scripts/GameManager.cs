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

    public int currentLevel = 1;
    private int maxLevels = 4;
    private int CurrentLevel
    {
        get { return currentLevel; }
    }

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnPlayButtonClicked() //Considering the player clicked on play button on the main menu
    {
        GameManager.Instance.LoadNextLevel();
    }
    public void LoadNextLevel()
    {
        if (currentLevel < maxLevels)
        {
            currentLevel++;
            SceneManager.LoadScene("Level" + currentLevel);
        }
        else
        {
            // O jogador concluiu todos os níveis
            ShowVictoryScreen();
        }
    }
    public void ShowVictoryScreen()
    {
        Debug.Log("Congratulations! You completed all levels. Thanks for playing!");
        SceneManager.LoadScene("Victory Scene");
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
    public void PlayTheLevelAgain()
    {
        SceneManager.LoadSceneAsync("Level" + GameManager.Instance.currentLevel);
    }
}
