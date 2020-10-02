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
    // Start is called before the first frame update

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
        DataUnit unit = new DataUnit(numberOfDimensions, minScore, maxScore);
        InstantiateDimensionObjects(unit.ReturnAchievements());
        //DimensionsLineBuilder();
        //DrawConnectingLine(_dimNumber);
    }

    private void InstantiateDimensionObjects(int[] achievements)
    {
        dUnits++;
        dimensionsPositions = new Vector3[numberOfDimensions];
        angleRate = GetangleRate(numberOfDimensions);

        GameObject dUnit = (GameObject)Instantiate(Resources.Load("UnitPrefabs/DataUnit"));
        dUnit.name = "DataUnit" + dUnits.ToString();

        LineRenderer lRenderer = dUnit.GetComponent<LineRenderer>();
        lRenderer.positionCount = numberOfDimensions;
        Vector3[] dimPositions = new Vector3[numberOfDimensions];

        for (int i = 0; i < numberOfDimensions; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angleRate, Vector3.up);
            Vector3 direction = rotation * Vector3.forward;
            int dimensionAchievement = achievements[i];

            float radius = GetRatiusBasedOnPercentageOfAchievement(maxRadius, dimensionAchievement, maxScore);
            Vector3 dimensionPosition = centralPosition + (direction * radius);
            dimPositions[i] = dimensionPosition;


            dimensionsPositions[i] = dimensionPosition;
            GameObject testObject = (GameObject)Instantiate(Resources.Load("UnitPrefabs/TestObject"), dimensionPosition, centralRotation);
            testObject.transform.SetParent(dUnit.transform);
        }

        lRenderer.startWidth = 0.1f;
        lRenderer.endWidth = 0.1f;
        lRenderer.SetPositions(dimPositions);
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


}

