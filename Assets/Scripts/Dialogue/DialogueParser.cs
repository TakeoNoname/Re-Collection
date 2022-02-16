using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    private const int MaxCharactersPerLine = 145;   // Max characters to fit into a text box

    private UnityEngine.UI.Text MainTextContainer;

    private bool IsReadyToAdvanceTextFlag = false;  // Flag to check if the parser is waiting on user input to advance text
    private bool IsTextToDisplayFlag = false;   // Flag to check if there is still text left to display

    public bool FinishInteraction = false;  // Flag to determine if an NPC interaction is finished.

    private List<string> textToDisplay;
    private int textToDisplayIndex = 0;

    private void Awake()
    {
        MainTextContainer = transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
    }

    private void Update()
    {
        // Check if player is trying to advance text and text is ready to advance
        if (IsReadyToAdvanceTextFlag && Input.GetKeyDown(KeyCode.E))
        {
            if (IsTextToDisplayFlag)
            {
                IsReadyToAdvanceTextFlag = false;
                StartCoroutine("PrintDialogue_TypeWriter");
            }
            else
            {
                EndInteraction();
            }
        }
    }

    // Method to be called by other classes that displays dialogue sent to it
    public void DisplayDialogue(string rawDialogue)
    {
        Time.timeScale = 0;
        ParseRawDialogue(rawDialogue);

        IsTextToDisplayFlag = true;
        StartCoroutine(PrintDialogue_TypeWriter());
    }

    // Print current line of text in type writer style
    private IEnumerator PrintDialogue_TypeWriter()
    {
        MainTextContainer.text = "";

        int messageLength = textToDisplay[textToDisplayIndex].Length;
        int messageCharacterIndex = 0;

        while (messageCharacterIndex < messageLength)
        {
            MainTextContainer.text += textToDisplay[textToDisplayIndex][messageCharacterIndex];

            yield return new WaitForEndOfFrame();
            messageCharacterIndex++;
        }

        if (textToDisplayIndex != textToDisplay.Count - 1)
            textToDisplayIndex++;
        else
            IsTextToDisplayFlag = false;

        IsReadyToAdvanceTextFlag = true;

        Debug.Log($"IsReadyToAdvanceTextFlag: {IsReadyToAdvanceTextFlag}");
    }

    // Convert raw dialogue into an array of strings that will individually fit the text box
    private void ParseRawDialogue(string paragraph)
    {
        textToDisplay = new List<string>();

        if (paragraph.Contains(" "))
        {
            int startingChar = 0;

            for (double i = 0; i < ((double)paragraph.Length / (double)MaxCharactersPerLine); i++)
            {
                int lastSpace = 0;

                if (i == 0 && paragraph.Length - startingChar > MaxCharactersPerLine)
                    lastSpace = FindLastSpace(paragraph, MaxCharactersPerLine);
                else if (i > 0 && paragraph.Length - startingChar > MaxCharactersPerLine)
                    lastSpace = FindLastSpace(paragraph, startingChar + MaxCharactersPerLine);
                else
                    lastSpace = paragraph.Length;

                string newLineContents = "";

                for (int j = startingChar; j < lastSpace; j++)
                {
                    newLineContents += paragraph[j];
                }

                textToDisplay.Add(newLineContents);
                startingChar = lastSpace + 1;
            }
        }
    }

    // Find the last space in a chunk of text
    private int FindLastSpace(string paragraph, int index)
    {
        bool spaceNotFound = true;

        if (index + 1 == paragraph.Length)
            return paragraph.Length;

        while (spaceNotFound)
        {
            if (paragraph[index] == ' ')
                spaceNotFound = false;
            else
                index--;
        }

        return index;
    }

    // End the dialogue interaction
    private void EndInteraction()
    {
        IsTextToDisplayFlag = false;
        IsReadyToAdvanceTextFlag = false;

        textToDisplayIndex = 0;

        Time.timeScale = 1;
        FinishInteraction = true;
    }
}
