using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoValueRangeSlider : MonoBehaviour
{
    int minValue = 0;
    int maxValue = 150;

    bool minIsClicked = false;

    public RectTransform minHandleTransform;
    public RectTransform maxHandleTransform;


    private void Awake()
    {
    }
}
