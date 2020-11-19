using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILeanTweenManager : MonoBehaviour
{
    public static UILeanTweenManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void 
}
