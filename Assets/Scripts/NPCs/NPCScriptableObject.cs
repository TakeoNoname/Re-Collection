using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Scriptable Objects/NPC", order = 1)]
public class NPCScriptableObject : ScriptableObject
{
    public string NPC_ID;
    public NPCInteractionTypes interactionType;
    public string dialogue;
    public string npcName;
    public Vector2 location;
}
