using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlobalNPCController : MonoBehaviour
{
    public NPCScriptableObject npcData;
    public SignScriptableObject signData;

    public GameObject MainDialogueBox;

    public bool IsInteracting = false;

    private List<NPCScriptableObject> npcDataList = new List<NPCScriptableObject>();
    private List<SignScriptableObject> signDataList = new List<SignScriptableObject>();

    private void Start()
    {
        npcDataList.AddRange(Resources.LoadAll<NPCScriptableObject>("NPC_Data"));
        signDataList.AddRange(Resources.LoadAll<SignScriptableObject>("Sign_Data"));
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

    SignScriptableObject SearchSign(string npcId)
    {
        foreach (SignScriptableObject sign in signDataList)
        {
            if (sign.NPC_ID == npcId)
                return sign;
        }

        // If sign is not found, return null sign data
        return signDataList.Where(x => x.NPC_ID == "SIGN_000").First();
    }

    // This method discerns the type of interaction the NPC data contains
    public void InteractWithNPC(string npcId)
    {
        IsInteracting = true;

        if (npcId.StartsWith("SIGN"))
        {
            signData = SearchSign(npcId);

            ReadSign(npcData);
        }
        else
        {
            npcData = SearchNPC(npcId);

            switch (npcData.interactionType)
            {
                case NPCInteractionTypes.Speak:
                    SpeakToNPC(npcData);
                    break;
            }
        }
    }

    // This NPC interaction is specifically for sign interactions and doesn't include multiple speakers
    void ReadSign(NPCScriptableObject npcData)
    {
        // Make sure the main dialogue box is displayed
        if (!MainDialogueBox.activeSelf)
            MainDialogueBox.SetActive(true);

        MainDialogueBox.GetComponent<DialogueParser>().DisplayDialogue(signData.dialogue);
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

public enum NPCInteractionTypes { Sign, Speak, SpeakAndGiveItem, Custom };
