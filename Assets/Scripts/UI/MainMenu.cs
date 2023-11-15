using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button howToPlayButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button creditsButton;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //These if statements will check if there are some buttons attached to the variables and if yes, add a new listener to their respective functions
        #region If Statements
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            Debug.Log("Listener for PlayButton added!");
        }
        if (settingsButton != null)
        {
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            Debug.Log("Listener for Settings added!");
        }
        if (howToPlayButton != null)
        {
            howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
            Debug.Log("Listener for howtoPlayButton added!");
        }
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
            Debug.Log("Listener for quit added!");
        }
        if (creditsButton != null)
        {
            creditsButton.onClick.AddListener(OnCreditsButtonClicked);
            Debug.Log("Listener for credits added!");
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Level1");
        Debug.Log("clicou em play");
    }
    void OnSettingsButtonClicked()
    {
        SceneManager.LoadScene("Settings");
        Debug.Log("clicou em settings");
    }
    void OnHowToPlayButtonClicked()
    {
        SceneManager.LoadScene("How To Play");
        Debug.Log("clicou em how to play");
    }
    void OnQuitButtonClicked()
    {
        Debug.Log("clicou em quit");
        Application.Quit();
    }
    void OnCreditsButtonClicked()
    {
        Debug.Log("clicou em Credits");
        SceneManager.LoadScene("Credits");
    }
}
