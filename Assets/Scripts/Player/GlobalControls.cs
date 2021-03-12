using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControls : MonoBehaviour
{
    private GameObject PauseMenuGUI;
    private GameObject Player;

    bool Paused = false;

    bool pauseTransition = false;
    bool unpauseTransition = false;

    float transitionCounter = 0f;

    public void Awake()
    {
        PauseMenuGUI = GameObject.Find("PauseMenu");
        Player = GameObject.Find("PlayerMovementController");
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            Interact();
        }

        if(Input.GetKey(KeyCode.Tab))
        {
            if (!pauseTransition && !unpauseTransition)
            {
                if (!Paused)
                    Pause();
                else
                    Unpause();
            }
        }

        if (pauseTransition)
        {
            Time.timeScale = 0;
            Player.GetComponent<Movement>().paused = true;

            if (transitionCounter > 0)
            {
                PauseMenuGUI.GetComponent<RectTransform>().Translate(0f, -1 * Time.unscaledDeltaTime * 10f, 0f, Space.World);
                transitionCounter--;
            }
            else
            {
                pauseTransition = false;
                Paused = true;
            }
        }

        if (unpauseTransition)
        {

            if (transitionCounter < 60)
            {
                PauseMenuGUI.GetComponent<RectTransform>().Translate(0f, Time.unscaledDeltaTime * 10f, 0f, Space.World);
                transitionCounter++;
            }
            else
            {
                Time.timeScale = 1;
                Player.GetComponent<Movement>().paused = false;

                unpauseTransition = false;
                Paused = false;
            }
        }
    }

    private void Interact()
    {

    }

    private void Pause()
    {
        transitionCounter = 60f;
        pauseTransition = true;
    }

    private void Unpause()
    {
        transitionCounter = 0f;
        unpauseTransition = true;
    }
}
