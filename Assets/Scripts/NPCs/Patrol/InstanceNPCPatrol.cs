using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstanceNPCPatrol : MonoBehaviour
{
    private NPCMovementController movementController;

    // A flag to denote if the NPC is supposed to be moving - doesn't do anything at the moment.
    private bool moving = false;

    private void Awake()
    {
        movementController = GetComponent<NPCMovementController>();
    }
    private void Start()
    {   
        moving = true;

        StartCoroutine(DefaultPatrol());
    }

    // This coroutine has the loop for the default NPC patrol behavior.
    private IEnumerator DefaultPatrol()
    {
        while (moving)
        {
            // Generates a random number of seconds between movements.
            int movementInterval = Random.Range(2, 8);

            StartCoroutine(MakeDefaultMovement());
            yield return new WaitForSeconds(movementInterval); 
        }
    }

    // This coroutine controls the rotation of the gyro, checks if that direction can be moved in, and either returns null or tells the movement controller to walk.
    private IEnumerator MakeDefaultMovement()
    {
        // A random number is generated to determine which direction the NPC will attempt to move in.
        // 0 = up, 1 = right, 2 = down, 3 = left.
        int movementDirection = Random.Range(0, 4);

        movementController.defaultPatrolMovement = true;

        switch(movementDirection)
        {
            case 0:
                movementController.gyro.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 180));
                yield return new WaitForFixedUpdate();
                if (CheckIfValidMovement())
                    movementController.startWalkingUp = true;
                else
                    yield return null;
                break;
            case 1:
                movementController.gyro.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 90));
                yield return new WaitForFixedUpdate();
                if (CheckIfValidMovement())
                    movementController.startWalkingRight = true;
                else
                    yield return null;
                break;
            case 2:
                movementController.gyro.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
                yield return new WaitForFixedUpdate();
                if (CheckIfValidMovement())
                    movementController.startWalkingDown = true;
                else
                    yield return null;
                break;
            case 3:
                movementController.gyro.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 270));
                yield return new WaitForFixedUpdate();
                if (CheckIfValidMovement())
                    movementController.startWalkingLeft = true;
                else
                    yield return null;
                break;
        }
    }

    // Contains logic to check if a movement is valid using the NPC's ray -- incomplete
    private bool CheckIfValidMovement()
    {
        // Check for if the ray ends inside or outside a patrol box.
        if (movementController.rayController.RayCollisions.Where(x => x.tag == "NPC_Patrol").FirstOrDefault() == null)
            return false;
        else
            return true;
    }
}
