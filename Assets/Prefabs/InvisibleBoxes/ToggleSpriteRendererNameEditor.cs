using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (ToggleSpriteRendererName))]
public class ToggleSpriteRendererNameEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ToggleSpriteRendererName sr = (ToggleSpriteRendererName)target;

        if(GUILayout.Button("Toggle Debug Boxes"))
        {
            sr.ToggleDebugBoxes();
        }
    }
}
