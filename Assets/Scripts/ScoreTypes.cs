using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTypes
{
    public ScoreTypes() { }

    public ScoreTypes(int deaths, int iSCP, int iContainment, bool bInfo, string cContainment, bool died, bool WSCP, int tBonus_Info)
    {
        Deaths = deaths;
        Incorrect_SCP = iSCP;
        Incorrect_Containment = iContainment;
        Total_Bonus_Info = tBonus_Info;
        Chosen_Containment = cContainment;
        Died = died;
        Wrong_SCP = WSCP;
        Bonus_Info = bInfo;
    }

    // Properties.
    public int Deaths { get; set; }
    public int Incorrect_SCP { get; set; }
    public int Incorrect_Containment { get; set; }
    public bool Bonus_Info { get; set; }
    public string Chosen_Containment { get; set; }
    public bool Died { get; set; }
    public bool Wrong_SCP { get; set; }
    public int Total_Bonus_Info { get; set; }
}
