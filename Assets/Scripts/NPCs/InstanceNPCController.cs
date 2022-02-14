using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceNPCController : MonoBehaviour
{
    public string NPC_ID;
    public GameObject GameManager;

    public void InteractWithNPC()
    {
        if(!GameManager.GetComponent<GlobalNPCController>().IsInteracting)
            GameManager.GetComponent<GlobalNPCController>().InteractWithNPC(NPC_ID);
    }
}
