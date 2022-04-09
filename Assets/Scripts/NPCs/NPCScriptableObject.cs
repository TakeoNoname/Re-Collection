using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Entity Scriptable Objects/NPC", order = 1)]
public class NPCScriptableObject : ScriptableObject
{
    public string NPC_ID;
    public NPCInteractionTypes interactionType;
    public Vector2 location;

    public string dialogue;
    public string npcName;
}

[CreateAssetMenu(fileName = "SIGN", menuName = "Entity Scriptable Objects/Sign", order = 2)]
public class SignScriptableObject : ScriptableObject
{
    public string NPC_ID;
    public NPCInteractionTypes interactionType;
    public Vector2 location;

    public string dialogue;
}

