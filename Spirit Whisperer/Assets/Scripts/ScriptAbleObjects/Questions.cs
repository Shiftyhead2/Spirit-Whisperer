using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question",menuName = "Gameplay/Player/Questions/New Question", order  = 52)]
public class Questions : ScriptableObject
{
    [TextArea(0, 100)]
    public string QuestionText;

    [Range(0, 1)]
    public float FailureChanceModifier;
    [Range(0, 1)]
    public float AngerChanceModifier;

    public int Reveals;

    public List<Responses> responses = new List<Responses>();
    public List<Responses> starterResponses = new List<Responses>();
}
