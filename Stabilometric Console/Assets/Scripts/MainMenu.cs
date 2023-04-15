using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // go to File-> Building Settings in Unity to check scene order. [this is dependent on the build order]
        SceneManager.LoadScene("Runner Game"); // Alternatively, Could just call the game scene by its name (ie a string -> .LoadScene("[name]"))
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("Settings Menu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
