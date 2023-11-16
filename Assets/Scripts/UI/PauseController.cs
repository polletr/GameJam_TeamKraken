using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public GameManager gameManager; 

    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    public GameObject pausePopUp;

    // Start is called before the first frame update
    void Start()
    {
        if (continueButton != null)
        { 
        continueButton.onClick.AddListener(BackToLevel);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Ativa o objeto se estiver desativado, ou desativa se estiver ativado
            if (pausePopUp != null)
            {
                pausePopUp.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    void BackToLevel()
    {
        pausePopUp.SetActive(false);
        Time.timeScale = 1f;
    }
    void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    void ExitGame()
    {
        Application.Quit();
        Debug.Log("Saindo do jogo");
    }
}
