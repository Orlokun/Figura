using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionsArchitect : MonoBehaviour
{
    [SerializeField]
    private int numberOfDimensions;

    [SerializeField]
    [Range(0, 1000)]
    private int maxScore;

    [SerializeField]
    [Range(0, 1000)]
    private int minScore;


    private float maxRadius;


    DimensionComponent[] dimensions;
    RangesVisualizer rVisualizer;

    Quaternion centralRotation;
    Vector3 centralPosition;

    // Start is called before the first frame update

    void Awake()
    {
        rVisualizer = FindObjectOfType<RangesVisualizer>();
        maxRadius = rVisualizer.GetMaxRadius();
        centralPosition = transform.position;
        centralRotation = transform.rotation;
        StartInstantiationProcess();
    }

    private void StartInstantiationProcess()
    {
        float anglesForDimension = 360 / numberOfDimensions;

        for (int i = 0; i < numberOfDimensions; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * anglesForDimension, Vector3.up);
            Vector3 direction = rotation * Vector3.forward;
            int falseData = RandomNumber();

            float radius = GetRatiusBasedOnPercentageOfAchievement(maxRadius, falseData, maxScore);
            Debug.Log("First Stat was: " + falseData);
            Vector3 dimensionPosition = centralPosition + (direction * radius);

            GameObject testObject = (GameObject)Instantiate(Resources.Load("TestObject"), dimensionPosition, centralRotation);
        }
    }




    Vector3 GetDimensionPosition(Vector3 _cPosition, float newRotation, float radius)
    {
        float ang = centralRotation.y + newRotation;

        Vector3 pos;
        pos.x = _cPosition.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = _cPosition.y;
        pos.z = _cPosition.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

        return pos;
    }

    float GetRatiusBasedOnPercentageOfAchievement(float maxRadius, float score, float maxScore)
    {
        return maxRadius * (score / maxScore);
    }


    int RandomNumber()
    {
        return Random.Range(minScore, maxScore);
    }
}

