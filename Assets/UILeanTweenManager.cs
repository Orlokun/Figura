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

    public static void ScaleAnimate(GameObject _obj)
    {
        LeanTween.scale(_obj.GetComponent<RectTransform>(), _obj.GetComponent<RectTransform>().localScale * 4f, 1f).setDelay(2f);
    }

    public static void ScaleAnimate(GameObject _obj, Vector2 finalSize, float time)
    {
        LeanTween.scale(_obj.GetComponent<RectTransform>(), _obj.GetComponent<RectTransform>().localScale * 1f, 1f).setDelay(2f);
    }

    public static void MoveAnimate(GameObject _obj, float xVal, float time)
    {
        LeanTween.moveLocalX(_obj, xVal, time);
    }
}
