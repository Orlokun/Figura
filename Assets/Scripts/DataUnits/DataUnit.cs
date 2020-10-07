using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUnit : MonoBehaviour
{
    [SerializeField]
    string userName;

    [SerializeField]
    string curso;


    int numberOfDimension;
    int[] unitScores;

    int minScore;
    int maxScore;

    private void GenerateRandomData()
    {
        for (int i = 0; i < unitScores.Length; i++)
        {
            unitScores[i] = RandomNumber();
        }
    }

    int RandomNumber()
    {
        return Random.Range(minScore, maxScore);
    }

    public int[] ReturnAchievements()
    {
        return unitScores;
    }

    public void SetDataUnitVariables(int _nDimensions, int _minScore, int _maxScore)
    {
        numberOfDimension = _nDimensions;
        unitScores = new int[numberOfDimension];

        //Just for random Data
        maxScore = _maxScore;
        minScore = _minScore;
        GenerateRandomData();
    }
}

