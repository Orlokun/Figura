using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionComponent : MonoBehaviour
{
    [Range(0, 100)]
    public int pObtenido;

    // Start is called before the first frame update
    void Awake()
    {

    }

    public void SetMyValue(int _pObtenido)
    {
        pObtenido = _pObtenido;
    }
}
