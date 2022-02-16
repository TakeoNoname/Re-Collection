using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{


    public void PlayGameButtonClick()
    {
        // For test purposes only
        SceneManager.LoadScene("Overworld");
    }

    public void OptionsButtonClick()
    {

    }

    public void ExitGameButtonClick()
    {
        // For Testing purposes only
        Application.Quit();
    }
}
