using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question",menuName = "Questions/New Question", order  = 52)]
public class Questions : ScriptableObject
{
    [TextArea(0, 100)]
    public string QuestionText;

    public List<Responses> responses = new List<Responses>();
}
