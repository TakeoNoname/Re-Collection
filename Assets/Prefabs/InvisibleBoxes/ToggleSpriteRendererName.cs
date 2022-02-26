using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSpriteRendererName : MonoBehaviour
{
    public void ToggleDebugBoxes()
    {
        Component[] components = GetComponentsInChildren(typeof(SpriteRenderer), true);

        foreach (var c in components)
        {
            c.GetComponent<SpriteRenderer>().enabled = !c.GetComponent<SpriteRenderer>().enabled;
        }
    }
}
