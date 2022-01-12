using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Genders
{
    Male,
    Female
}

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
    public string Gender { get; private set; }
    public string CauseOfDeath { get; private set; }

    private List<Names> MaleFirstNames = new List<Names>();
    private List<Names> FemaleFirstNames = new List<Names>();
    private List<Names> SurnNames = new List<Names>();
    private List<DeathCauses> DeathCauses = new List<DeathCauses>();


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
        GetDeathCauses();
        SetUpGender();
        SetUpName();
        SetUpBirthAndDeath();
        SetUpCauseOfDeath();
    }

    void SetUpCauseOfDeath()
    {
        CauseOfDeath = DeathCauses[Random.Range(0, DeathCauses.Count)].DeathText;
    }

    void SetUpGender()
    {
        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                Gender = Genders.Male.ToString();
                break;
            case 1:
                Gender = Genders.Female.ToString();
                break;
        }
    }

    void GetDeathCauses()
    {
        var _deathCauses = Resources.LoadAll<DeathCauses>("Death Causes");

        foreach(var d in _deathCauses)
        {
            DeathCauses.Add(d);
        }
    }

    void GetNames()
    {
        var _femalefirstnames = Resources.LoadAll<Names>("Names/First Names/Female");
        var _malefirstnames = Resources.LoadAll<Names>("Names/First Names/Male");
        var _surnnames = Resources.LoadAll<Names>("Names/Surnames");

        foreach(var n in _femalefirstnames)
        {
            FemaleFirstNames.Add(n);
        }

        foreach(var n in _malefirstnames)
        {
            MaleFirstNames.Add(n);
        }

        foreach(var s in _surnnames)
        {
            SurnNames.Add(s);
        }
        
    }

    void SetUpName()
    {
        var firstName = string.Empty;
        var surnName = SurnNames[Random.Range(0, SurnNames.Count)].NameText;

        switch (Gender)
        {
            case "Male":
                firstName = MaleFirstNames[Random.Range(0, MaleFirstNames.Count)].NameText;
                break;
            case "Female":
                firstName = FemaleFirstNames[Random.Range(0, FemaleFirstNames.Count)].NameText;
                break;
        }

        FullName = $"{firstName} {surnName}";
    }

    void SetUpBirthAndDeath()
    {
        var BirthMonth = Random.Range(1, 13);
        var DeathMonth = Random.Range(1, 13);
        var BirthDay = 0;
        var DeathDay = 0;

        if (BirthMonth == 2)
        {
            
            BirthDay = Random.Range(1, 29);
            
        }
        else 
        {
            BirthDay = Random.Range(1, 32); 
        }

        if(DeathMonth == 2)
        {
            DeathDay = Random.Range(1, 29);
        }
        else
        {
            DeathDay = Random.Range(1, 32);
        }


        var BirthYear = Random.Range(0, 2000);
        var DeathYear = BirthYear + Random.Range(0, 101);

        var actualAge = DeathYear - BirthYear;

        Age = $"{actualAge}";
        DateOfBirth = $"{BirthDay}.{BirthMonth}.{BirthYear}";
        DateOfDeath = $"{DeathDay}.{DeathMonth}.{DeathYear}";
    }
}
