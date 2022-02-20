using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the normal ray extruding from each NPC.
/// </summary>
public class NPCRayController : MonoBehaviour
{
    // This is the collider used by the end of the ray
    public new Collider2D collider;

    // This list keeps an up-to-the-frame collection of hits on the end of the ray
    public List<Collider2D> RayCollisions = new List<Collider2D>();

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // These functions keep the RayCollisions list up to date
    #region RayCollisions List Control
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RayCollisions.Add(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RayCollisions.Add(collision.collider);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RayCollisions.Remove(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        RayCollisions.Remove(collision.collider);
    }
    #endregion
}
