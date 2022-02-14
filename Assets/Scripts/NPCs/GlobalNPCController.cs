using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlobalNPCController : MonoBehaviour
{
    public NPCScriptableObject npcData;
    public GameObject MainDialogueBox;

    public bool IsInteracting = false;

    private List<NPCScriptableObject> npcDataList = new List<NPCScriptableObject>();

    private void Start()
    {
        npcDataList.AddRange(Resources.LoadAll<NPCScriptableObject>("NPC_Data"));
    }
    private void Update()
    {
        if (MainDialogueBox.GetComponent<DialogueParser>().FinishInteraction)
        {
            MainDialogueBox.GetComponent<DialogueParser>().FinishInteraction = false;
            MainDialogueBox.SetActive(false);
            IsInteracting = false;
        }
    }

    NPCScriptableObject SearchNPC(string npcId)
    {
        foreach (NPCScriptableObject npc in npcDataList)
        {
            if (npc.NPC_ID == npcId)
                return npc;
        }

        // If npc is not found, return null NPC data
        return npcDataList.Where(x => x.NPC_ID == "000").First();
    }

    // This method discerns the type of interaction the NPC data contains
    public void InteractWithNPC(string npcId)
    {
        IsInteracting = true;

        npcData = SearchNPC(npcId);

        if (npcData.interactionType == NPCInteractionTypes.Speak)
            SpeakToNPC(npcData);
    }

    // This NPC interaction initiates a conversation with the selected NPC data
    void SpeakToNPC(NPCScriptableObject npcData)
    {
        // Make sure the main dialogue box is displayed
        if(!MainDialogueBox.activeSelf)
            MainDialogueBox.SetActive(true);

        MainDialogueBox.GetComponent<DialogueParser>().DisplayDialogue(npcData.dialogue);
    }
}

public enum NPCInteractionTypes { Speak, SpeakAndGiveItem, Custom };
