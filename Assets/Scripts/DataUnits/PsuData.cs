using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct EssayResult
{
    private Dictionary<string, int> essayResults;

    private System.DateTime date;

    public EssayResult(Dictionary<string, int> _results)
    {
        essayResults = new Dictionary<string, int>();
        essayResults = _results;

        date = System.DateTime.Now;
    }
}


[CreateAssetMenu(fileName = "PsuPlayerData", menuName = "PsuPlayerData")]
public class PsuData : ScriptableObject
{
    protected string pName;
    protected string[] psuTestNames;

    protected int numberOfEssays;

    protected List<EssayResult> generalResults;

    protected void Awake()
    {
        //For test only. Must be deprecated
        CreateTestData();
    }

    public void CreateTestData()
    {
        psuTestNames = new string[4] { "Mate", "Leng", "Hist", "Cien" };
        if (generalResults == null)
        {
            generalResults = new List<EssayResult>();
        }

        if (generalResults.Count <= 0)
        {
            GenerateRandomPSUData();
        }
    }

    private void GenerateRandomPSUData()
    {
        numberOfEssays = Random.Range(2, 10);
        for (int i = 0; i < numberOfEssays; i++)
        {
            CreatePsuTestDataSet(i);
        }
    }

    private void CreatePsuTestDataSet(int tanda)
    {
        Dictionary<string, int> testResults = new Dictionary<string, int>();
        for (int i = 0; i < psuTestNames.Length; i++)
        {
            int rScore = Random.Range(250, 850);
            testResults.Add(psuTestNames[i], rScore);
            Debug.Log("I added the random score of: " + rScore + "a la tanda número: " + tanda + "de ensayos");
        }
        EssayResult eResult = new EssayResult(testResults);
        SaveTestDataSet(eResult);
    }

    private void SaveTestDataSet(EssayResult _eResult)
    {
        generalResults.Add(_eResult);
    }

    public int GetGeneralResultsCount()
    {
        return generalResults.Count;
    }
}

