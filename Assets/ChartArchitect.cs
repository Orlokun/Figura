using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataSet
{
    psu,

}



public class ChartArchitect : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera vCamera;

    float maxXsize = 10f;
    float maxYsize = 10f;
    float maxZsize = 10f;

    [SerializeField]
    public int xValues;
    [SerializeField]
    public int zValues;

    [SerializeField]
    string[] xAxisLabel;
    [SerializeField]
    string[] zAxisLabel;
    
    [SerializeField]
    private int yMaxScore;
    [SerializeField]
    private int yMinScore;

    //Color Handler
    [SerializeField]
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    float[] xPositions;
    float[] zPositions;
    float xAxisDistRange;
    float zAxisDistRange;

    public string[] xTags;
    public string[] zTags;

    public List<Vector3>[] xAxisColumns;
    public List<Vector3>[] zAxisColumns;

    // Start is called before the first frame update
    void Start()
    {

        GenerateLists();
        CalculateChartXZPositions();              //Done. Data saved in floats. 
        InstantiateWithPositionValues();
        InstantiateTargetCameraObject();
        CalculateChartGeneralDimensions();
        //JustDoit();
    }

    void InstantiateTargetCameraObject()
    {
        GameObject centralObject = (GameObject)Instantiate(Resources.Load("CentralObj"),transform, false);
        centralObject.transform.localPosition = new Vector3(maxXsize / 2, maxYsize / 2, maxZsize / 2);

        vCamera.m_LookAt = centralObject.transform;
        vCamera.m_Follow = centralObject.transform;
    }

    void GenerateLists()
    {
        xAxisColumns = new List<Vector3>[xValues];
        for (int i = 0; i < xAxisColumns.Length; i++)
        {
            xAxisColumns[i] = new List<Vector3>();
        }
        zAxisColumns = new List<Vector3>[zValues];
        for (int i = 0; i < zAxisColumns.Length; i++)
        {
            zAxisColumns[i] = new List<Vector3>();
        }
    }

    void CalculateChartXZPositions()
    {
        int xVirtualPositions = xValues * 2;
        int zVirtualPositions = zValues * 2;

        xPositions = new float[xValues];
        zPositions = new float[zValues];

        xAxisDistRange = maxXsize / xVirtualPositions;
        zAxisDistRange = maxZsize / zVirtualPositions;

        int xPositionToBeAdded = 0;
        int zPositionToBeAdded = 0;

        for (int i = 0; i < xVirtualPositions; i++)
        {
            if (i % 2 != 0)
            {
                xPositions[xPositionToBeAdded] = xAxisDistRange * i;
                Debug.Log("xPosition added was: " + xPositions[xPositionToBeAdded]);
                xPositionToBeAdded++;
            }
        }

        Debug.Log("/////////////////////////////////////");

        for (int i = 0; i < zVirtualPositions; i++)
        {
            if (i % 2 != 0)
            {
                zPositions[zPositionToBeAdded] = zAxisDistRange * i;
                Debug.Log("zPosition added was: " + zPositions[zPositionToBeAdded]);
                zPositionToBeAdded++;
            }
        }
    }

    void InstantiateWithPositionValues()
    {
        //Instantiate Parent Object
        GameObject hObject = (GameObject)Instantiate(Resources.Load("3DChart/ChartsObjectHolder"), transform, false);
        hObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //Set x Position
        for (int xPos = 0; xPos < xPositions.Length; xPos++)
        {
            for (int zPos = 0; zPos < zPositions.Length; zPos++)
            {
                GameObject chObject = (GameObject)Instantiate(Resources.Load("TestCylinder"), hObject.transform, false);
                chObject.transform.localPosition = new Vector3(xPositions[xPos], 0.01f, zPositions[zPos]);
                chObject.transform.rotation = hObject.transform.rotation;

                //Adding columns and rows by position **Must be deprecated
                xAxisColumns[xPos].Add(new Vector3(xPositions[xPos], 0.01f, zPositions[zPos]));
                chObject.transform.localScale = new Vector3(GetChartDiameter(), chObject.transform.localScale.y, GetChartDiameter());
            }
        }

        //Adding columns and rows by position **Must be deprecated
        for (int zPos = 0; zPos < zPositions.Length; zPos++)
        {
            for (int xPos = 0; xPos < xPositions.Length; xPos++)
            {
                zAxisColumns[zPos].Add(new Vector3(xPositions[xPos], transform.localPosition.y, zPositions[zPos]));
            }
        }
    }

    void CalculateChartGeneralDimensions()
    {
        float cylRadius = GetChartDiameter();
        float xOffSet = CalculateOffset(xValues);
        float zOffSet = CalculateOffset(zValues);
    }

    private float GetChartDiameter()
    {
        if (zValues >= xValues)
        {
            return CalculateChartDiameter(zValues);
        }
        else
        {
            return CalculateChartDiameter(xValues);
        }
    }

    float CalculateChartDiameter(int rows)
    {
        return (maxZsize / rows) * 0.8f;        //this are the perecentages
    }

    private float CalculateOffset(int _nValues)
    {
        return (_nValues / maxZsize) * 0.2f;    //this are the perecentages
    }

    void JustDoit()
    {
        for (int i = 0; i < xValues; i++)
        {
            for (int e = 0; e < zValues; e++)
            {
                Instantiate(Resources.Load("TestCylinder"), new Vector3(i, 0, e), transform.rotation);
            }
        }
    }
}
