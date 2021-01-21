using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    bool isWalkingUp = false, isWalkingDown = false, isWalkingRight = false, isWalkingLeft = false;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isWalkingUp)
        {
            player.GetComponent<Animator>().SetTrigger("walkUp");
            
            isWalkingUp = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isWalkingDown)
        {
            player.GetComponent<Animator>().SetTrigger("walkDown");

            isWalkingDown = true;

        }
        else if (Input.GetKeyDown(KeyCode.D) && !isWalkingRight)
        {
            player.GetComponent<Animator>().SetTrigger("walkRight");

            isWalkingRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isWalkingLeft)
        {
            player.GetComponent<Animator>().SetTrigger("walkLeft");

            isWalkingLeft = true;
        }
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            if (isWalkingLeft || isWalkingRight || isWalkingDown || isWalkingUp)
            {
                player.GetComponent<Animator>().SetTrigger("idle");

                isWalkingUp = false;
                isWalkingDown = false;
                isWalkingRight = false;
                isWalkingLeft = false;
            }
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            isWalkingUp = false;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isWalkingDown = false;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isWalkingRight = false;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isWalkingLeft = false;
        }



        if (isWalkingUp)
            player.transform.position += new Vector3(0f, .1f);
        else if (isWalkingDown)
            player.transform.position -= new Vector3(0f, .1f);
        else if (isWalkingRight)
            player.transform.position += new Vector3(.1f, 0f);
        else if (isWalkingLeft)
            player.transform.position -= new Vector3(.1f, 0f);
    }
}
