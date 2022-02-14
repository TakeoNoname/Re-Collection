using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractTrigger : MonoBehaviour
{
    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("I'm touching you!");

        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            transform.parent.GetComponent<InstanceNPCController>().InteractWithNPC();
        }
    }
}
