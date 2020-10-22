using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataSetType
{
    psu,
}


public class ChartArchitect : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera[] vCameras;
    public PlayerProfileData playerData;


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
    public Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    float[] xPositions;
    float[] zPositions;
    float xAxisDistRange;
    float zAxisDistRange;

    public string[] xTags;
    public string[] zTags;

    public List<SingleGraph>[] xAxisGraphs;
    public List<SingleGraph>[] zAxisGraphs;

    PlayerProfileData pStats;

    public DataSetType dType;

    // Start is called before the first frame update
    void Awake()
    {
        //JustDoit();
    }

    public void InitializeData(PlayerProfileData pProfile, DataSetType _dType)
    {
        SetBasicVariables(pProfile, _dType);
        GenerateLists();
        CalculateChartXZPositions();              //Done. Data saved in floats. 
        InstantiateWithPositionValues();
        InstantiateTargetCameraObject();
        CalculateChartGeneralDimensions();
    }

    void SetBasicVariables(PlayerProfileData _pProfile, DataSetType _dType)
    {
        switch (_dType)
        {
            case DataSetType.psu:
                GetDesignPSUValues(_pProfile);
                break;
            default:
                break;
        }
    }

    void GenerateLists()
    {
        xAxisGraphs = new List<SingleGraph>[xValues];
        for (int i = 0; i < xAxisGraphs.Length; i++)
        {
            xAxisGraphs[i] = new List<SingleGraph>();
        }
        zAxisGraphs = new List<SingleGraph>[zValues];
        for (int i = 0; i < zAxisGraphs.Length; i++)
        {
            zAxisGraphs[i] = new List<SingleGraph>();
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
                //Debug.Log("xPosition added was: " + xPositions[xPositionToBeAdded]);
                xPositionToBeAdded++;
            }
        }

        for (int i = 0; i < zVirtualPositions; i++)
        {
            if (i % 2 != 0)
            {
                zPositions[zPositionToBeAdded] = zAxisDistRange * i;
                //Debug.Log("zPosition added was: " + zPositions[zPositionToBeAdded]);
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
                //Create GameObject
                GameObject chObject = (GameObject)Instantiate(Resources.Load("Prefab_BarObject"), hObject.transform, false);
                chObject.transform.localPosition = new Vector3(xPositions[xPos], 0.01f, zPositions[zPos]);
                chObject.transform.rotation = hObject.transform.rotation;
                chObject.transform.localScale = new Vector3(GetChartDiameter(), chObject.transform.localScale.y, GetChartDiameter());

                //Set Script Data
                SingleGraph sGraph = GetGraphFromchild(chObject);

                sGraph.testName = playerData.psuPData.GetPSUTestName(zPos);
                sGraph.testNumber = xPos;
                sGraph.maxScore = playerData.psuPData.GetMaxScore();
                foreach (KeyValuePair<int, EssayResult> kPair in playerData.psuPData.GetGeneralResults())
                {
                    if (kPair.Key == xPos)
                    {
                        EssayResult eResult = kPair.Value;
                        sGraph.actualScore = eResult.GetEssayResult(sGraph.testName);
                        sGraph.SetGraphState(SingleGraphMovState.increasing);
                        Debug.Log("Success!!!!! in Test named: " + sGraph.testName + " with score: " + sGraph.actualScore);
                    }
                }
                sGraph.UpdateLabelWithScore();
                //Save Graph Unit in List
                xAxisGraphs[xPos].Add(sGraph);
                zAxisGraphs[zPos].Add(sGraph);
            }
        }

        //Adding columns and rows by position **Must be deprecated

    }

    void GetDesignPSUValues(PlayerProfileData _pProfile)
    {
        zValues = 4;
        xValues = _pProfile.psuPData.GetGeneralResultsCount();
    }

    void InstantiateTargetCameraObject()
    {
        GameObject centralObject = (GameObject)Instantiate(Resources.Load("CentralObj"), transform, false);
        centralObject.transform.localPosition = new Vector3(maxXsize / 2, maxYsize / 2, maxZsize / 2);

        foreach(Cinemachine.CinemachineVirtualCamera vCamera in vCameras)
        {
            vCamera.m_LookAt = centralObject.transform;
            vCamera.m_Follow = centralObject.transform;
        }
    }




    private SingleGraph GetGraphFromchild(GameObject chObject)
    {
        for (int i = 0; i < chObject.transform.childCount; i++)
        {
            if (chObject.transform.GetChild(i).GetComponent<SingleGraph>())
            {
                return chObject.transform.GetChild(i).GetComponent<SingleGraph>();
            }
        }
        Debug.LogError("None of the childs Object had a 'Single graph' script. Game object name: " + chObject.name);
        return null;
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
