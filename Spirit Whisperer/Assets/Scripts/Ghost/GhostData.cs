using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostData : MonoBehaviour
{
    public static GhostData Instance { get; private set; }

    public string FullName { get; private set; }

    [SerializeField]private List<Names> FirstNames = new List<Names>();
    [SerializeField]private List<Names> SurnNames = new List<Names>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        GetNames();
        SetUpName();
    }


    void GetNames()
    {
        var _firstnames = Resources.LoadAll<Names>("Names/First Names");
        var _surnnames = Resources.LoadAll<Names>("Names/Surnames");

        foreach(var n in _firstnames)
        {
            FirstNames.Add(n);
        }

        foreach(var s in _surnnames)
        {
            SurnNames.Add(s);
        }
        
    }

    void SetUpName()
    {
        var firstName = FirstNames[Random.Range(0, FirstNames.Count)].NameText;
        var surnName = SurnNames[Random.Range(0, SurnNames.Count)].NameText;

        FullName = firstName + " " + surnName;
    }
}
