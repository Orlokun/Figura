using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DimensionsArchitect : MonoBehaviour
{
    public RadarGraphData rData;

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
        maxRadius = rData.GetMaxRadius();
        centralPosition = transform.position;
        centralRotation = transform.rotation;
        DrawGuideLinesforDimension();
    }


    private void DrawGuideLinesforDimension()
    {
        angleRate = rData.GetangleRate();
        GameObject gLinesObj = (GameObject)Instantiate(Resources.Load("RangesPrefabs/GuideLinesObject"));
        gLinesObj.transform.SetParent(rVisualizer.transform);
        for (int i = 0; i < rData.GetNumberOfDimensions(); i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angleRate, Vector3.up);
            Vector3 direction = rotation * Vector3.forward;

            float radius = rData.GetRatiusBasedOnPercentageOfAchievement(rData.GetMaxScore() * 1.1f);
            Vector3 lineEnd = centralPosition + (direction * radius);

            GameObject lineObject = (GameObject)Instantiate(Resources.Load("RangesPrefabs/DimLine"), rVisualizer.transform.position, centralRotation);
            lineObject.transform.SetParent(gLinesObj.transform);

            DrawLine(lineObject, lineEnd);
        }
    }

    #region Getters

    public Vector3 GetCentralPosition()
    {
        return centralPosition;
    }

    public Quaternion GetCentralRotation()
    {
        return centralRotation;
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

