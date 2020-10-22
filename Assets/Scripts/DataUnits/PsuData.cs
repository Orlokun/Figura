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

    public int GetEssayResult(string _key)
    {
        foreach (KeyValuePair<string, int> kPair in essayResults)
        {
            if (kPair.Key == _key)
            {
                return kPair.Value;
            }
        }
        Debug.LogError("No essay was named Like the key delivered in the PSU data Object");
        return 0;
    }
}

[CreateAssetMenu(fileName = "PsuPlayerData", menuName = "PsuPlayerData")]
public class PsuData : ScriptableObject
{
    protected string pName;
    protected string[] psuTestNames;
    protected int maxScore = 850;

    protected int numberOfEssays;

    protected Dictionary<int, EssayResult> generalResults;

    protected void Awake()
    {
        //For test only. Must be deprecated
        CreateTestData();
    }

    public void CreateTestData()
    {
        numberOfEssays = Random.Range(2, 10);
        psuTestNames = new string[4] { "Mate", "Leng", "Hist", "Cien" };
        if (generalResults == null)
        {
            generalResults = new Dictionary<int, EssayResult>();
        }

        if (generalResults.Count <= 0)
        {
            GenerateRandomPSUData();
        }
    }

    private void GenerateRandomPSUData()
    {
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
            int rScore = Random.Range(250, maxScore);
            testResults.Add(psuTestNames[i], rScore);
            Debug.Log("I added the random score of: " + rScore + "a la tanda número: " + tanda + "de ensayos");
        }

        EssayResult eResult = new EssayResult(testResults);
        SaveTestDataSet(tanda, eResult);

    }

    private void SaveTestDataSet(int _tandaEnsayo, EssayResult _eResult)
    {
        generalResults.Add(_tandaEnsayo, _eResult);
    }

    #region Getters&Setters
    public int GetGeneralResultsCount()
    {
        return generalResults.Count;
    }

    public string GetPSUTestName(int _i)
    {
        return psuTestNames[_i];
    }

    public Dictionary<int, EssayResult> GetGeneralResults()
    {
        return generalResults;
    }

    public int GetMaxScore()
    {
        return maxScore;
    }
    #endregion
}

