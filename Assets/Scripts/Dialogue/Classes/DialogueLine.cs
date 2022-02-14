using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLine
{
    public string Contents { get; set; }
    public Vector3 Position { get; set; }

    public DialogueLine()
    {
        Contents = "";
        Position = new Vector3(0, 0, 0);
    }

    public DialogueLine(string _contents)
    {
        Contents = _contents;
        Position = new Vector3(0, 0, 0);
    }

    public DialogueLine(string _contents, Vector3 _position)
    {
        Contents = _contents;
        Position = _position;
    }
}
