using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartArchitect : MonoBehaviour
{

    float maxXsize = 10f;
    float maxYsize = 10f;
    float maxZsize = 10f;

    [SerializeField]
    public int xValues;
    [SerializeField]
    public int zValues;

    [SerializeField]
    private int yMaxScore;
    [SerializeField]
    private int yMinScore;


    // Start is called before the first frame update
    void Start()
    {
        JustDoit();
    }


    void JustDoit()
    {
        for (int i = 0; i<xValues;i++)
        {
            for (int e = 0; e<zValues;e++)
            {
                Instantiate(Resources.Load("TestCylinder"), new Vector3(i, 0, e),transform.rotation);
            }
        }
    }
}
