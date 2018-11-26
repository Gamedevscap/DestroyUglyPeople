using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

    /* ---
    ATTACH THIS SCRIPT 
    TO THE EVENT SYSTEM GAMEOBJECT 
    --- */

    [Header("Main Menu Composition")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject selectLevelMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject infoMenu;

    [Header("First Selected Buttons")]
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject selectLevelButton;
    [SerializeField] GameObject settingsButton;


    private void Update()
    {
        BackToMainMenu();
    }

    public void StartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

	public void ExitGame()
    {
        Application.Quit();
    }

    void BackToMainMenu()
    {
        if (Input.GetButton("Cancel"))
        {
            if (mainMenu.gameObject.activeSelf == false && selectLevelMenu.gameObject.activeSelf == true)
            {
                selectLevelMenu.gameObject.SetActive(false);
                mainMenu.gameObject.SetActive(true);
            }
            if (mainMenu.gameObject.activeSelf == false && settingsMenu.gameObject.activeSelf == true)
            {
                settingsMenu.gameObject.SetActive(false);
                mainMenu.gameObject.SetActive(true);
            }
            if (mainMenu.gameObject.activeSelf == false && infoMenu.gameObject.activeSelf == true)
            {
                infoMenu.gameObject.SetActive(false);
                mainMenu.gameObject.SetActive(true);
            }
        }
    }
}
