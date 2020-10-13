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

    private Dictionary<string, int> psuScores;

    public string[] psuTestNames;

    void Awake()
    {
        if (psuScores==null)
        {
            psuScores = new Dictionary<string, int>();
        }

        if (psuTestNames == null)
        {
            psuTestNames = new string[4] { "Mat", "Leng", "Hist", "Cienc" };
        }

        if (psuScores.Count <= 0)
        {
            GenerateRandomPSUData();
        }
    }

    private void GenerateRandomPSUData()
    {
        for (int i = 0; i<psuTestNames.Length; i++)
        {
            int rScore = Random.Range(250, 850);
            psuScores.Add(psuTestNames[i], rScore);
            Debug.Log("I added the random score of: " + rScore);
        }
    }
}
