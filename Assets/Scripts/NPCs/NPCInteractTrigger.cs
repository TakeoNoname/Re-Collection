using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractTrigger : MonoBehaviour
{
    private bool IsInteractable = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && IsInteractable)
            transform.parent.GetComponent<InstanceNPCController>().InteractWithNPC();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            IsInteractable = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        IsInteractable = false;
    }
}
