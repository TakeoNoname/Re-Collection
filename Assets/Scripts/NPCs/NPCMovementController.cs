using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementController : MonoBehaviour
{
    // The gyro object rotates independently of the NPC object to keep the axes consistent.
    public GameObject gyro;
    public NPCRayController rayController;

    // These variables represent the direction the NPC is moving.
    public float horizontal = 0f;
    public float vertical = 0f;

    // Flag set to denote when default patrol movement is in use.
    public bool defaultPatrolMovement;

    // Checks to see if the movement can be made.
    public bool validMovement = false;

    // Flags set to denote when the NPC should start walking in a certain direction.
    public bool startWalkingLeft;
    public bool startWalkingRight;
    public bool startWalkingUp;
    public bool startWalkingDown;

    // Flags set to denote when the NPC shoud stop walking in a certain direction.
    public bool stopWalkingLeft;
    public bool stopWalkingRight;
    public bool stopWalkingUp;
    public bool stopWalkingDown;

    // Flags that keep track of the bool references in the NPC's animator.
    private bool isWalkingLeft;
    private bool isWalkingRight;
    private bool isWalkingUp;
    private bool isWalkingDown;

    // Counts the frames since a movement started.
    private int frameCounter;

    // The animator component of the NPC.
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gyro = transform.GetChild(0).gameObject;

        rayController = gyro.transform.GetChild(2).GetChild(0).gameObject.GetComponent<NPCRayController>();

        isWalkingLeft = GetComponent<Animator>().GetBool("isWalkingLeft");
        isWalkingRight = GetComponent<Animator>().GetBool("isWalkingRight");
        isWalkingUp = GetComponent<Animator>().GetBool("isWalkingUp");
        isWalkingDown = GetComponent<Animator>().GetBool("isWalkingDown");
    }

    private void FixedUpdate()
    {
        transform.Translate(horizontal / 60f, vertical / 60f, 0);

        if (defaultPatrolMovement)
        {
            if (frameCounter < 60)
                frameCounter++;
            else
            {
                defaultPatrolMovement = false;
                frameCounter = 0;

                StopWalking();
            }
        }
    }

    private void Update()
    {
        // Reset the idle trigger so the NPC isn't listed as Idle after a movement starts.
        animator.ResetTrigger("idle");

        // Checks if the NPC should start walking
        if(startWalkingLeft)
        {
            startWalkingLeft = false;
            animator.SetTrigger("walkLeft");
            animator.SetBool("isWalkingLeft", true);
            isWalkingLeft = true;

            horizontal = -1f;
            vertical = 0f;
        }
        if (startWalkingRight)
        {
            startWalkingRight = false;
            animator.SetTrigger("walkRight");
            animator.SetBool("isWalkingRight", true);
            isWalkingRight = true;

            horizontal = 1f;
            vertical = 0f;
        }
        if (startWalkingUp)
        {
            startWalkingUp = false;
            animator.SetTrigger("walkUp");
            animator.SetBool("isWalkingUp", true);
            isWalkingUp = true;

            horizontal = 0f;
            vertical = 1f;
        }
        if (startWalkingDown)
        {
            startWalkingDown = false;
            animator.SetTrigger("walkDown");
            animator.SetBool("isWalkingDown", true);
            isWalkingDown = true;

            horizontal = 0f;
            vertical = -1f;
        }

        // Checks if the NPC should stop walking.
        if(stopWalkingLeft)
        {
            stopWalkingLeft = false;
            animator.SetBool("isWalkingLeft", false);
            isWalkingLeft = false;
        }
        if(stopWalkingRight)
        {
            stopWalkingRight = false;
            animator.SetBool("isWalkingRight", false);
            isWalkingRight = false;
        }
        if (stopWalkingUp)
        {
            stopWalkingUp = false;
            animator.SetBool("isWalkingUp", false);
            isWalkingUp = false;
        }
        if (stopWalkingDown)
        {
            stopWalkingDown = false;
            animator.SetBool("isWalkingDown", false);
            isWalkingDown = false;
        }

        // Sets the animation to idle if there is no movement.
        if (!isWalkingDown && !isWalkingUp && !isWalkingLeft && !isWalkingRight)
            animator.SetTrigger("idle");
    }

    // Set all flags to stop walking and stop the NPC's motion.
    private void StopWalking()
    {
        if (isWalkingLeft)
            stopWalkingLeft = true;
        if (isWalkingRight)
            stopWalkingRight = true;
        if (isWalkingUp)
            stopWalkingUp = true;
        if (isWalkingDown)
            stopWalkingDown = true;

        horizontal = 0f;
        vertical = 0f;
    }
}
