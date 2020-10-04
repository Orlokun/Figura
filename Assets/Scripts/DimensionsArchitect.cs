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

    DimensionComponent[] dimensions;
    RangesVisualizer rVisualizer;

    Quaternion centralRotation;
    Vector3 centralPosition;

    Vector3[] dimensionsPositions;

    List<DataUnit> availableDataUnits = new List<DataUnit>();
    GameObject dUnitObj;
    DataUnit dUnit;

    void Awake()
    {
        rVisualizer = FindObjectOfType<RangesVisualizer>();
        maxRadius = rVisualizer.GetMaxRadius();
        centralPosition = transform.position;
        centralRotation = transform.rotation;
        DrawGuideLinesforDimension();
    }

    private float GetangleRate(int numberOfDimensions)
    {
        return 360 / numberOfDimensions;
    }

    void DimensionsLineBuilder()
    {
        /*lRenderer.SetPositions(dimensionsPositions);
        lRenderer.startWidth = .1f;
        lRenderer.endWidth = .1f;*/
    }

    void DrawConnectingLine(int _dimAmount)
    {
        for (int i = 0; i < _dimAmount; i++)
        {
            float dist;

            if (i++ >= _dimAmount)
            {
                dist = Vector3.Distance(dimensionsPositions[i], dimensionsPositions[0]);
            }
            else
            {
                dist = Vector3.Distance(dimensionsPositions[i], dimensionsPositions[i + 1]);
            }
        }
    }

    public void AddSatisticsUnit()
    {
        SetParentObjectAndData();
        InstantiateDimensionObjects(dUnitObj, dUnit);

        LineRenderer lRenderer = dUnitObj.GetComponent<LineRenderer>();
        lRenderer.positionCount = numberOfDimensions;

        lRenderer.startWidth = 0.1f;
        lRenderer.endWidth = 0.1f;
        lRenderer.SetPositions(dimensionsPositions);
    }
    private void SetParentObjectAndData()
    {
        dUnits++;
        dUnitObj = (GameObject)Instantiate(Resources.Load("UnitPrefabs/DataUnit"));
        dUnit = dUnitObj.GetComponent<DataUnit>();
        dUnitObj.name = "DataUnit" + dUnits.ToString();
        dUnit.ObjectReceiveData(numberOfDimensions, minScore, maxScore);
        availableDataUnits.Add(dUnit);
    }

    private void InstantiateDimensionObjects(GameObject _dObject, DataUnit _unit)
    {
        dimensionsPositions = new Vector3[numberOfDimensions];
        angleRate = GetangleRate(numberOfDimensions);
        int[] dimensionsAchievements = _unit.ReturnAchievements();

        for (int i = 0; i < numberOfDimensions; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angleRate, Vector3.up);
            Vector3 direction = rotation * Vector3.forward;

            int dimAchievement = dimensionsAchievements[i];
            float radius = GetRatiusBasedOnPercentageOfAchievement(maxRadius, dimAchievement, maxScore);
            Vector3 dimensionPosition = centralPosition + (direction * radius);


            dimensionsPositions[i] = dimensionPosition;
            GameObject testObject = (GameObject)Instantiate(Resources.Load("UnitPrefabs/TestObject"), dimensionPosition, centralRotation);
            testObject.transform.SetParent(_dObject.transform);
        }
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

            float radius = GetRatiusBasedOnPercentageOfAchievement(maxRadius, maxScore * 1.1f, maxScore);
            Vector3 lineEnd = centralPosition + (direction * radius);

            GameObject lineObject = (GameObject)Instantiate(Resources.Load("RangesPrefabs/DimLine"), rVisualizer.transform.position, centralRotation);
            lineObject.transform.SetParent(gLinesObj.transform);

            LineRenderer line = lineObject.GetComponent<LineRenderer>();

            line.startWidth = 0.07f;
            line.endWidth = 0.07f;
            line.SetPosition(0, gLinesObj.transform.position);
            line.SetPosition(1, lineEnd);
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

    public void FilterDataRange(float minVal, float maxVal)
    {
        foreach (DataUnit unit in availableDataUnits)
        {
            if (unit.gameObject.activeInHierarchy)
            {
                foreach (int unitScore in unit.ReturnAchievements())
                {
                    if (unitScore > maxVal || unitScore < minVal)
                    {
                        unit.gameObject.SetActive(false);
                    }
                }
            }
            else if (!unit.gameObject.activeInHierarchy)
            {
                foreach (int unitScore in unit.ReturnAchievements())
                {
                    if (unitScore <= maxVal && unitScore >= minVal)
                    {
                        unit.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}

