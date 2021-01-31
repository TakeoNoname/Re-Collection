using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftHandler : MonoBehaviour
{
    public GameObject MainCamera;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MainCamera.GetComponent<CameraTransition>().MoveCameraLeft();
        }
    }
}
