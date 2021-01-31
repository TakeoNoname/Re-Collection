using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightHandler : MonoBehaviour
{
    public GameObject MainCamera;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MainCamera.GetComponent<CameraTransition>().MoveCameraRight();
        }
    }
}
