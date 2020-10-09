using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerData")]
public class PlayerGeneralStats : ScriptableObject
{
    [SerializeField]
    string name;
    [SerializeField]
    string mail;

    [SerializeField]
    [Range(250, 850,order = 1)]
    int mPsu; 

    [SerializeField]
    [Range(250, 850,order = 1)]
    int lPsu;

    [SerializeField]
    [Range(250, 850,order = 1)]
    int cPsu;

    [SerializeField]
    [Range(250, 850,order = 1)]
    int hPsu;
    
}
