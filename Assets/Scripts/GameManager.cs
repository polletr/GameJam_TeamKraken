using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 1;
    private int maxLevels = 4;
    private int CurrentLevel
    {
        get { return currentLevel; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainMenu");
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
}
