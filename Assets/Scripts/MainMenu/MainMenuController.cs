using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{


    public void PlayGameButtonClick()
    {
        // For testing purposes only
        SceneManager.LoadScene("Overworld");
    }

    public void OptionsButtonClick()
    {
        // Implement later
    }

    public void ExitGameButtonClick()
    {
        // For testing purposes only
        Application.Quit();
    }
}
