using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject tutorialPanel1;
    [SerializeField] private GameObject tutorialPanel2;
    [SerializeField] private GameObject tutorialPanel3;
    [SerializeField] private GameObject tutorialPanel4;
    [SerializeField] private GameObject tutorialPanel5;
    [SerializeField] private GameObject tutorialPanel6;
    [SerializeField] private GameObject tutorialPanel7;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    //BIG NO NO: i am also using this script to handle the win screen when the player beats the level. this is an innapropriate use of this script, but if it ain't 1:30 in the morning again
    public bool isPaused;
    public bool levelComplete; //if true, don't allow player to open pause menu

    private SaveMechanic saveManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = true; //start the level paused
        levelComplete = false; //false at start, because obviously the player would not be able to beat the level as soon as it loads

        ShowTutorial(); //show the tutorial for the level

        if (GameObject.Find("SaveManager") != null)
        {
            saveManagerScript = GameObject.Find("SaveManager").GetComponent<SaveMechanic>();
        }
        else
        {
            // If there is no SaveManager object (bc the game was started from game and not menu)
            // Create a SaveManager object and attach the SaveMechanic script to it
            GameObject saveManagerObject = new GameObject("SaveManager");
            saveManagerObject.AddComponent<SaveMechanic>();
            saveManagerScript = saveManagerObject.GetComponent<SaveMechanic>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!levelComplete) //if level is NOT complete, allow player to check regular pause menu
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // If the game is not already paused
                if (!isPaused)
                    // Open the pause menu
                    OpenPauseMenu();
                else
                {
                    // Else, hide the pause menu
                    pauseMenuCanvas.SetActive(false);
                    isPaused = false;
                }
            }
        }
        
    }

    public void OpenPauseMenu()
    {
        pauseMenuCanvas.SetActive(true);
        isPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Continue()
    {
        // Hide the pause menu
        pauseMenuCanvas.SetActive(false);
        isPaused = false;
    }

    //authored by kevin dolan
    //this method is activated by the button from the pause menu during normal gameplay
    public void ShowTutorial()
    {
        switch(StaticGameInformation.LevelNameToLoad)
        {
            case "Level1_Ice_Tutorial":
                tutorialPanel1.gameObject.SetActive(true);
                break;

            case "Level3_Fire_Tutorial":
                tutorialPanel2.gameObject.SetActive(true);
                break;

            case "Level2_Ice":
                tutorialPanel3.gameObject.SetActive(true);
                break;

            case "Level4_Fire_Ice":
                tutorialPanel4.gameObject.SetActive(true);
                break;

            case "Level5_Wind_Tutorial":
                tutorialPanel5.gameObject.SetActive(true);
                break;

            case "Level6_Wind":
                tutorialPanel6.gameObject.SetActive(true);
                break;

            case "Level7_Final_Level":
                tutorialPanel6.gameObject.SetActive(true);
                break;

            default:
                //don't display any tutorial
                break;
        }

        pauseMenuCanvas.SetActive(false); //in case player opened tutorial form pause menu, close it
    }

    //this script is activated by clicking on a huge, invisible panel that is superimposed over the whole tutorial text thing.
    public void HideTutorial()
    {
        switch (StaticGameInformation.LevelNameToLoad)
        {
            case "Level1_Ice_Tutorial":
                tutorialPanel1.gameObject.SetActive(false);
                break;

            case "Level3_Fire_Tutorial":
                tutorialPanel2.gameObject.SetActive(false);
                break;

            case "Level2_Ice":
                tutorialPanel3.gameObject.SetActive(false);
                break;

            case "Level4_Fire_Ice":
                tutorialPanel4.gameObject.SetActive(false);
                break;

            case "Level5_Wind_Tutorial":
                tutorialPanel5.gameObject.SetActive(false);
                break;

            case "Level6_Wind":
                tutorialPanel6.gameObject.SetActive(false);
                break;

            case "Level7_Final_Level":
                tutorialPanel6.gameObject.SetActive(false);
                break;

            default:
                //don't hide any tutorial
                break;
        }

        

        isPaused = false; //unpause the game when the player finished reading the instructions
    }

    //activates the win menu
    public void ShowWinMenu()
    {
        levelComplete = true;
        
        winMenu.SetActive(true);
    }
    public void ShowLoseMenu()
    {
        loseMenu.SetActive(true);
    }

    public void NextLevel()
    {
        //god damn it this is so stupid
        switch (StaticGameInformation.LevelNameToLoad) //SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reloads current scene and effectively resets level.
        {
            case "Level1_Ice_Tutorial":
                StaticGameInformation.LevelNameToLoad = "Level2_Ice";
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene
                saveManagerScript.levelComplete[0] = true;
                break;

            case "Level2_Ice":
                StaticGameInformation.LevelNameToLoad = "Level3_Fire_Tutorial"; //go to level 3
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene
                saveManagerScript.levelComplete[1] = true;
                break;

            case "Level3_Fire_Tutorial":
                StaticGameInformation.LevelNameToLoad = "Level4_Fire_Ice"; //go to level 4
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene
                saveManagerScript.levelComplete[2] = true;
                break;

            case "Level4_Fire_Ice":
                StaticGameInformation.LevelNameToLoad = "Level5_Wind_Tutorial"; //go to level 5
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene
                saveManagerScript.levelComplete[3] = true;
                break;

            case "Level5_Wind_Tutorial":
                StaticGameInformation.LevelNameToLoad = "Level6_Wind"; //go to level 6
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene
                saveManagerScript.levelComplete[4] = true;
                break;

            case "Level6_Wind":
                StaticGameInformation.LevelNameToLoad = "Level7_Final_Level"; //go to level 7
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene
                saveManagerScript.levelComplete[4] = true;
                break;

            case "Level7_Final_Level":
                StaticGameInformation.LevelNameToLoad = "Level1_Ice_Tutorial"; //go back to level 1
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene
                saveManagerScript.levelComplete[5] = true;
                break;

            default:
                //idk
                break;
        }

        saveManagerScript.SaveToFile();
        Time.timeScale = 1.0f;
    }
}
