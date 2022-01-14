using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{

    Vector3 newPosition;
    Vector3 newPlayerPosition;

    bool cameraMoving = false;

    int iterator = 0;

    public GameObject player;
    public GameObject playerMovement;


    private void FixedUpdate()
    {
        if (cameraMoving)
        {
            transform.position += newPosition / 60;
            player.transform.position += newPlayerPosition / 60;
            iterator++;

            if (iterator == 60)
            {
                cameraMoving = false;
                playerMovement.SetActive(true);
                player.GetComponent<BoxCollider2D>().enabled = true;
                playerMovement.GetComponent<Movement>().IsNotWalking();
                iterator = 0;
            }
        }
    }

    public void MoveCameraLeft()
    {
        player.GetComponent<BoxCollider2D>().enabled = false;
        newPosition = new Vector3(-10f, 0, 0);
        newPlayerPosition = new Vector3(-2f, 0, 0);
        cameraMoving = true;
        playerMovement.SetActive(false);      
    }

    public void MoveCameraRight()
    {
        player.GetComponent<BoxCollider2D>().enabled = false;
        newPosition = new Vector3(10f, 0, 0);
        newPlayerPosition = new Vector3(2f, 0, 0);
        cameraMoving = true;
        playerMovement.SetActive(false);
    }

    public void MoveCameraUp()
    {
        player.GetComponent<BoxCollider2D>().enabled = false;
        newPosition = new Vector3(0, 9f, 0);
        newPlayerPosition = new Vector3(0, 1.8f, 0);
        cameraMoving = true;
        playerMovement.SetActive(false);
    }

    public void MoveCameraDown()
    {
        player.GetComponent<BoxCollider2D>().enabled = false;
        newPosition = new Vector3(0, -9f, 0);
        newPlayerPosition = new Vector3(0, -1.8f, 0);
        cameraMoving = true;
        playerMovement.SetActive(false);
    }
}
