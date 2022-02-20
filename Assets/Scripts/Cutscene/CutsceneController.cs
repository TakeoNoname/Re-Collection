using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    private bool isCutscenePlaying = false;

    public GameObject player;

    public void StartCutscene()
    {
        isCutscenePlaying = true;

        TogglePlayerControls();
    }

    private void TogglePlayerControls()
    {
        if(isCutscenePlaying)
        {
            player.GetComponent<GlobalControls>().enabled = false;
            player.transform.GetChild(0).GetComponent<Movement>().enabled = false;
        }
        else
        {
            player.GetComponent<GlobalControls>().enabled = true;
            player.transform.GetChild(0).GetComponent<Movement>().enabled = true;
        }
    }
}
