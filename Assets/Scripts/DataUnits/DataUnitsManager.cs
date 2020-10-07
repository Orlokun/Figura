using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUnitsManager : MonoBehaviour
{
    [SerializeField]
    RadarGraphData rData;
    float angleRate;

    int dUnits;
    int numberOfDimensions;

    Vector3[] unitPositions;

    GameObject dUnitObj;
    DataUnit dUnit;
    List<DataUnit> availableDataUnits = new List<DataUnit>();


    private void Awake()
    {
        numberOfDimensions = rData.GetNumberOfDimensions();
    }

    public void AddSatisticsUnit()
    {
        SetParentObjectAndData();
        InstantiateDimensionObjects();

        LineRenderer lRenderer = dUnitObj.GetComponent<LineRenderer>();
        lRenderer.positionCount = numberOfDimensions;

        lRenderer.startWidth = 0.1f;
        lRenderer.endWidth = 0.1f;
        lRenderer.SetPositions(unitPositions);
    }

    private void SetParentObjectAndData()
    {
        dUnits++;
        dUnitObj = (GameObject)Instantiate(Resources.Load("UnitPrefabs/DataUnit"));
        dUnit = dUnitObj.GetComponent<DataUnit>();
        dUnitObj.name = "DataUnit" + dUnits.ToString();
        dUnit.SetDataUnitVariables(rData.GetNumberOfDimensions(), rData.GetMinScore(), rData.GetMaxScore());
        availableDataUnits.Add(dUnit);
    }

    private void InstantiateDimensionObjects()
    {
        unitPositions = new Vector3[numberOfDimensions];
        angleRate = rData.GetangleRate();
        int[] dimensionsAchievements = dUnit.ReturnAchievements();

        for (int i = 0; i < numberOfDimensions; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angleRate, Vector3.up);
            Vector3 direction = rotation * Vector3.forward;

            int dimAchievement = dimensionsAchievements[i];
            float radius = rData.GetRatiusBasedOnPercentageOfAchievement(dimAchievement);
            Vector3 dimensionPosition = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z) + (direction * radius);

            unitPositions[i] = dimensionPosition;
            GameObject testObject = (GameObject)Instantiate(Resources.Load("UnitPrefabs/TestObject"), dimensionPosition, new Quaternion(0,0,0,0));
            testObject.transform.SetParent(dUnitObj.transform);
        }
    }

    public void FilterDataRange(float minVal, float maxVal)
    {
        foreach (DataUnit unit in availableDataUnits)
        {
            if (AllValuesPass(unit, minVal, maxVal))
            {
                unit.gameObject.SetActive(true);
            }
            else
            {
                unit.gameObject.SetActive(false);
            }
        }
    }

    private bool AllValuesPass(DataUnit _dUnit, float _minVal, float _maxVal)
    {
        int dNumber = _dUnit.ReturnAchievements().Length;
        int passedValues = 0;

        foreach (int unitScore in _dUnit.ReturnAchievements())
        {
            if (unitScore <= _maxVal && unitScore >= _minVal)
            {
                passedValues++;
            }
        }

        return passedValues >= dNumber;
    }
}