using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILeanTweenManager : MonoBehaviour
{
    public static UILeanTweenManager instance;

    //DeleteThisTest
    public static GameObject _animObject;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _animObject = GameObject.Find("ArchivedViewport");
        LeanTween.scale(_animObject.GetComponent<RectTransform>(), _animObject.GetComponent<RectTransform>().localScale * 0.1f, 0.01f);
        Debug.Log(_animObject.GetComponent<RectTransform>().localScale);
        ScaleAnimate();
    }

    public static void ScaleAnimate(GameObject _animObject)
    {
        LeanTween.scale(_animObject.GetComponent<RectTransform>(), _animObject.GetComponent<RectTransform>().localScale * 4f, 1f).setDelay(2f);
    }

    public static void ScaleAnimate()
    {
        LeanTween.scale(_animObject.GetComponent<RectTransform>(), _animObject.GetComponent<RectTransform>().localScale * 1f, 1f).setDelay(2f);
    }
}
