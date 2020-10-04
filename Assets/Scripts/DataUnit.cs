using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUnit : MonoBehaviour
{
    int numberOfDimension;
    int[] dimAchievements;

    int minScore;
    int maxScore;

    private void GenerateRandomData()
    {
        for (int i = 0; i < dimAchievements.Length; i++)
        {
            dimAchievements[i] = RandomNumber();
        }
    }


    int RandomNumber()
    {
        return Random.Range(minScore, maxScore);
    }

    public int[] ReturnAchievements()
    {
        return dimAchievements;
    }

    public void ObjectReceiveData(int _nDimensions, int _minScore, int _maxScore)
    {
        numberOfDimension = _nDimensions;
        minScore = _minScore;
        maxScore = _maxScore;
        dimAchievements = new int[numberOfDimension];
        GenerateRandomData();
    }
}

