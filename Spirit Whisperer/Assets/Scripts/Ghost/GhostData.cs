using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostData : MonoBehaviour
{
    /* Code for generating ghost data runs only once in the awake method. 
     * The way some things were generated may not be pretty ,but since the generation code runs once 
     it's really not a big deal*/

    public static GhostData Instance { get; private set; }

    public string FullName { get; private set; }
    public string DateOfBirth { get; private set; }
    public string DateOfDeath { get; private set; }
    public string Age { get; private set; }

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
        SetUpBirthAndDeath();
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

    void SetUpBirthAndDeath()
    {
        var BirthDay = Random.Range(0, 32);
        var DeathDay = Random.Range(0, 32);
        var BirthMonth = Random.Range(1, 13);
        var DeathMonth = Random.Range(1, 13);
        var BirthYear = Random.Range(0, 2000);
        var DeathYear = BirthYear + Random.Range(0, 101);

        var actualAge = DeathYear - BirthYear;

        Age = actualAge.ToString();
        DateOfBirth = BirthDay.ToString() + "." + BirthMonth.ToString() + "." + BirthYear.ToString();
        DateOfDeath = DeathDay.ToString() + "." + DeathMonth.ToString() + "." + DeathYear.ToString();
    }
}
