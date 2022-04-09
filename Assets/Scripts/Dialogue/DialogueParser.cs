using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    private const int MaxCharactersPerLine = 87;   // Max characters to fit into a text box

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

        // This function divides each paragraph into lines of the correct length to be displayed in the text box
        ParseRawDialogue(rawDialogue);

        IsTextToDisplayFlag = true;
        StartCoroutine(PrintDialogue_TypeWriter());
    }

    public void DisplayOneSidedDialogue(string rawDialogue)
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

        // Check if the paragraph contains a space to avoid errors
        if (paragraph.Contains(" "))
        {
            // This character is where the iterator starts in the paragraph while dividing it up into segments.
            int startingChar = 0;

            // This loop seperates the paragraph into sections to be displayed in order in the MainDialogueBox.
            for (double i = 0; i < ((double)paragraph.Length / (double)MaxCharactersPerLine); i++)
            {
                // This is the position of the last space that fits inside of the segment.
                int lastSpace = 0;

                // This if statement describes the case that the first section has more characters than the MaxCharactersPerLine.
                if (i == 0 && paragraph.Length - startingChar > MaxCharactersPerLine)
                    // Find the last space in the section using the index value of the MaxCharactersPerLine.
                    lastSpace = FindLastSpace(paragraph, MaxCharactersPerLine);
                // This else if statement describes the case that any section but the first has more characters than the MaxCharactersPerLine. 
                else if (i > 0 && paragraph.Length - startingChar > MaxCharactersPerLine)
                    // Find the last space in the section using the index value of the MaxCharactersPerLine plus the index value of the current starting character.
                    lastSpace = FindLastSpace(paragraph, startingChar + MaxCharactersPerLine);
                // This else statement describes any other case.
                else
                    // Use the remaining length of the section as the value for the last space.
                    lastSpace = paragraph.Length;

                // Create a variable to store the value of the new section.
                string newSectionContents = "";

                for (int j = startingChar; j < lastSpace; j++)
                {
                    newSectionContents += paragraph[j];
                }

                textToDisplay.Add(newSectionContents);
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
