using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    float horizontal = 0f;
    float vertical = 0f;

    public bool paused = false;

    bool isWalkingLeft = false;
    bool isWalkingRight = false;
    bool isWalkingUp = false;
    bool isWalkingDown = false;

    public void FixedUpdate()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        player.transform.Translate(horizontal / 7.5f, vertical / 7.5f, 0);
    }

    public void Update()
    {
        if (!paused)
        {
            player.GetComponent<Animator>().ResetTrigger("idle");

            if (Input.GetKeyDown(KeyCode.A))
            {
                player.GetComponent<Animator>().SetTrigger("walkLeft");
                player.GetComponent<Animator>().SetBool("isWalkingLeft", true);
                isWalkingLeft = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                player.GetComponent<Animator>().SetTrigger("walkRight");
                player.GetComponent<Animator>().SetBool("isWalkingRight", true);
                isWalkingRight = true;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                player.GetComponent<Animator>().SetTrigger("walkUp");
                player.GetComponent<Animator>().SetBool("isWalkingUp", true);
                isWalkingUp = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                player.GetComponent<Animator>().SetTrigger("walkDown");
                player.GetComponent<Animator>().SetBool("isWalkingDown", true);
                isWalkingDown = true;
            }


            if (Input.GetKeyUp(KeyCode.A))
            {
                player.GetComponent<Animator>().SetBool("isWalkingLeft", false);
                isWalkingLeft = false;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                player.GetComponent<Animator>().SetBool("isWalkingRight", false);
                isWalkingRight = false;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                player.GetComponent<Animator>().SetBool("isWalkingUp", false);
                isWalkingUp = false;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                player.GetComponent<Animator>().SetBool("isWalkingDown", false);
                isWalkingDown = false;
            }

            if (!isWalkingDown && !isWalkingUp && !isWalkingLeft && !isWalkingRight)
                player.GetComponent<Animator>().SetTrigger("idle");

        }
        
    }

    public void IsNotWalking()
    {
        isWalkingDown = false;
        isWalkingLeft = false;
        isWalkingRight = false;
        isWalkingUp = false;
    }
}
