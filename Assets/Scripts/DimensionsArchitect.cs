using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DimensionsArchitect : MonoBehaviour
{
    [SerializeField]
    private int numberOfDimensions;
    private int dUnits;

    [SerializeField]
    [Range(0, 1000)]
    private int maxScore;

    [SerializeField]
    [Range(0, 1000)]
    private int minScore;


    private float maxRadius;
    private float angleRate;

    RangesVisualizer rVisualizer;

    Quaternion centralRotation;
    Vector3 centralPosition;


    DataUnitsManager dUnitsManager;



    void Awake()
    {
        rVisualizer = FindObjectOfType<RangesVisualizer>();
        dUnitsManager = FindObjectOfType<DataUnitsManager>();
        maxRadius = rVisualizer.GetMaxRadius();
        centralPosition = transform.position;
        centralRotation = transform.rotation;
        DrawGuideLinesforDimension();
    }

    public float GetangleRate(int numberOfDimensions)
    {
        return 360 / numberOfDimensions;
    }

    private void DrawGuideLinesforDimension()
    {
        angleRate = GetangleRate(numberOfDimensions);
        GameObject gLinesObj = (GameObject)Instantiate(Resources.Load("RangesPrefabs/GuideLinesObject"));
        gLinesObj.transform.SetParent(rVisualizer.transform);
        for (int i = 0; i < numberOfDimensions; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angleRate, Vector3.up);
            Vector3 direction = rotation * Vector3.forward;

            float radius = GetRatiusBasedOnPercentageOfAchievement(maxScore * 1.1f);
            Vector3 lineEnd = centralPosition + (direction * radius);

            GameObject lineObject = (GameObject)Instantiate(Resources.Load("RangesPrefabs/DimLine"), rVisualizer.transform.position, centralRotation);
            lineObject.transform.SetParent(gLinesObj.transform);

            DrawLine(lineObject, lineEnd);
        }
    }

    #region Getters
    public int GetNumberOfDimensionsEvaluated()
    {
        return numberOfDimensions;
    }

    public int GetMinValue()
    {
        return minScore;
    }

    public int GetMaxScore()
    {
        return maxScore;
    }

    public Vector3 GetCentralPosition()
    {
        return centralPosition;
    }

    public Quaternion GetCentralRotation()
    {
        return centralRotation;
    }

    public float GetRatiusBasedOnPercentageOfAchievement(float _score)
    {
        return maxRadius * (_score / maxScore);
    }
    #endregion

    private void DrawLine(GameObject _lineObject, Vector3 _lineEnd)
    {
        LineRenderer line = _lineObject.GetComponent<LineRenderer>();

        line.startWidth = 0.07f;
        line.endWidth = 0.07f;
        line.SetPosition(0, _lineObject.transform.position);
        line.SetPosition(1, _lineEnd);
    }

    private void DrawLine(GameObject _lineObject, Vector3[] _linePositions)
    {
        LineRenderer line = _lineObject.GetComponent<LineRenderer>();

        line.startWidth = 0.07f;
        line.endWidth = 0.07f;
        line.SetPositions(_linePositions);
    }
   
}

