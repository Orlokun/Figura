using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUnit : MonoBehaviour
{
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

    public void ObjectReceiveData(int _nDimensions, int _minScore, int _maxScore)
    {
        numberOfDimension = _nDimensions;
        minScore = _minScore;
        maxScore = _maxScore;
        unitScores = new int[numberOfDimension];
        GenerateRandomData();
    }
}

