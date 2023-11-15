using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentLevel = 1;
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
            // O jogador concluiu todos os n�veis
            ShowVictoryScreen();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnPlayButtonClicked()
    {
        GameManager.Instance.LoadNextLevel();
    }
    public void ShowLevelCompletionPopup()
    {
        SceneManager.LoadScene("");
    }
    public void ShowVictoryScreen()
    {
        // L�gica para exibir a tela de vit�ria e agradecimentos
        Debug.Log("Congratulations! You completed all levels. Thanks for playing!");
        // Aqui voc� pode chamar m�todos para exibir uma tela de vit�ria, agradecimentos e cr�ditos.
    }
}
