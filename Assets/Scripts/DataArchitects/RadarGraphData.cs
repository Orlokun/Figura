using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RadarGraph", menuName = "Radar Graph")]
public class RadarGraphData : ScriptableObject
{
    [SerializeField]
    private int numberOfDimensions;

    [SerializeField]
    [Range(0, 1000)]
    private int maxScore;

    [SerializeField]
    [Range(0, 1000)]
    private int minScore;

    [SerializeField]
    [Range(0, 10)]
    private float maxRadius;

    public int numberOfRanges;

    #region Public Getters & Setters
    //MinScore
    public int GetMinScore()
    {
        return minScore;
    }

    public void SetMinScore(int _minScore)
    {
        minScore = _minScore;
    }
    //Max Score
    public int GetMaxScore()
    {
        return maxScore;
    }

    public void SetMaxScore(int _maxScore)
    {
        maxScore = _maxScore;
    }
    //Dimensions
    public void SetNumberOfDimensions(int _nDimensions)
    {
        numberOfDimensions = _nDimensions;
    }

    public int GetNumberOfDimensions()
    {
        return numberOfDimensions;
    }

    public float GetangleRate()
    {
        return 360 / numberOfDimensions;
    }

    //Achievements
    public float GetRatiusBasedOnPercentageOfAchievement(float _score)
    {
        return maxRadius * (_score / maxScore);
    }

    //Achievement Ranges
    public int GetNumberOfRanges()
    {
        return numberOfRanges;
    } 

    //radius
    public float GetMaxRadius()
    {
        return maxRadius;
    }

    #endregion
}
