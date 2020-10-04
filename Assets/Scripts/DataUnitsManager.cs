using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUnitsManager : MonoBehaviour
{
    DimensionsArchitect dArchitect;
    float angleRate;

    int dUnits;
    int numberOfDimensions;

    Vector3[] unitPositions;

    GameObject dUnitObj;
    DataUnit dUnit;
    List<DataUnit> availableDataUnits = new List<DataUnit>();


    private void Awake()
    {
        dArchitect = FindObjectOfType<DimensionsArchitect>();
        numberOfDimensions = dArchitect.GetNumberOfDimensionsEvaluated();
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
        dUnit.ObjectReceiveData(dArchitect.GetNumberOfDimensionsEvaluated(), dArchitect.GetMinValue(), dArchitect.GetMaxScore());
        availableDataUnits.Add(dUnit);
    }

    private void InstantiateDimensionObjects()
    {
        unitPositions = new Vector3[numberOfDimensions];
        angleRate = dArchitect.GetangleRate(numberOfDimensions);
        int[] dimensionsAchievements = dUnit.ReturnAchievements();

        for (int i = 0; i < numberOfDimensions; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angleRate, Vector3.up);
            Vector3 direction = rotation * Vector3.forward;

            int dimAchievement = dimensionsAchievements[i];
            float radius = dArchitect.GetRatiusBasedOnPercentageOfAchievement(dimAchievement);
            Vector3 dimensionPosition = dArchitect.GetCentralPosition() + (direction * radius);

            unitPositions[i] = dimensionPosition;
            GameObject testObject = (GameObject)Instantiate(Resources.Load("UnitPrefabs/TestObject"), dimensionPosition, dArchitect.GetCentralRotation());
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