using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SliderMovType
{
    Horizontal,
    Vertical,
}

public enum SliderDataType
{
    Chart3D,
    RadarGraph,
}

public class SlideHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region GlobalVariables
    //the type of slider
    [SerializeField]
    SliderMovType sType;

    [SerializeField]
    SliderDataType sDType;

    //Basic Data From Radar Scriptable
    public RadarGraphData rData;
    public PsuData psuData;

    //Both of the slider handlers Transforms
    [SerializeField]
    RectTransform minSliderTransform;
    [SerializeField]
    RectTransform maxSliderTransform;

    //This handler transform
    RectTransform rTransform;

    //ScriptReferences
    SlideHandler minSlideHandler;
    SlideHandler maxSlideHandler;

    //Snippet and textbox object
    public GameObject snippet;
    public GameObject snippetBox;

    //Max positions based on rect transform position
    float maxPosition;
    float minPosition;
    float distRange;

    DataUnitsManager dUManager;

    [SerializeField]
    bool isMaxSlider;

    int showableNumber;
    #endregion

    #region InitializingData

    private void Awake()
    {
        //TODO: Must Check Initial Parameters
        SetInitialData();
    }

    void SetInitialData()
    {
        SetTransformsData();
        SetDistanceRangeData();
        CheckSliderRole();
        snippet.SetActive(false);
        snippetBox.SetActive(false);

        switch (sDType)
        {
            case SliderDataType.Chart3D:
                break;
            case SliderDataType.RadarGraph:
                dUManager = FindObjectOfType<DataUnitsManager>();
                break;
            default:
                return;
        }
    }

    private void SetDistanceRangeData()
    {
        distRange = Mathf.Abs(maxPosition) + Mathf.Abs(minPosition);
    }

    private void SetTransformsData()
    {
        maxSlideHandler = maxSliderTransform.gameObject.GetComponent<SlideHandler>();
        minSlideHandler = minSliderTransform.gameObject.GetComponent<SlideHandler>();
        switch (sType)
        {
            case SliderMovType.Horizontal:
                minPosition = minSliderTransform.anchoredPosition.x;
                maxPosition = maxSliderTransform.anchoredPosition.x;
                break;
            case SliderMovType.Vertical:
                minPosition = minSliderTransform.anchoredPosition.y;
                maxPosition = maxSliderTransform.anchoredPosition.y;
                break;
            default:
                return;
        }
    }

    private void CheckSliderRole()
    {
        if (isMaxSlider)
        {
            rTransform = maxSliderTransform;
        }
        else
        {
            rTransform = minSliderTransform;
        }
    }
    #endregion

    #region OnDragRegion
    public void OnDrag(PointerEventData eventData)
    {

        switch (sType)
        {
            case SliderMovType.Horizontal:
                HandleUIHorizontalSlide(eventData);

                break;
            case SliderMovType.Vertical:
                HandleUIVerticalSlide(eventData);
                break;
            default:
                return;
        }
    }

    #region MovementHandlers

    private void HandleUIVerticalSlide(PointerEventData _eData)                    //This code is bassilcally repeated. Can be optimized and made flexible
    {
        Vector2 eDeltaSinX = new Vector2(rTransform.anchoredPosition.x, _eData.delta.y);
        float anchPosY = rTransform.anchoredPosition.y;
        float newYPosi = anchPosY += _eData.delta.y;

        if (newYPosi < minPosition || newYPosi > maxPosition)
        {
            return;
        }
        else if (isMaxSlider && newYPosi < minSliderTransform.anchoredPosition.y)
        {
            minSliderTransform.anchoredPosition += eDeltaSinX;
            rTransform.anchoredPosition += eDeltaSinX;
            showableNumber = CalculateShowableNumber(newYPosi);
            DisplaySnipetNumber();
        }
        else if (!isMaxSlider && newYPosi > maxSliderTransform.anchoredPosition.y)
        {
            maxSliderTransform.anchoredPosition += eDeltaSinX;
            rTransform.anchoredPosition += eDeltaSinX;
            showableNumber = CalculateShowableNumber(newYPosi);
            DisplaySnipetNumber();
        }
        else
        {
            rTransform.anchoredPosition += eDeltaSinX;
            anchPosY = rTransform.anchoredPosition.y;
            showableNumber = CalculateShowableNumber(newYPosi);
            DisplaySnipetNumber();
        }
    }

    private void HandleUIHorizontalSlide(PointerEventData _eData)
    {
        Vector2 eventDeltaSinY = new Vector2(_eData.delta.x, rTransform.anchoredPosition.y);
        float anchPosX = rTransform.anchoredPosition.x;
        float newAnchPosX = anchPosX += _eData.delta.x;

        if (newAnchPosX < minPosition || newAnchPosX > maxPosition)
        {
            return;
        }

        else if (isMaxSlider && newAnchPosX < minSliderTransform.anchoredPosition.x)
        {
            minSliderTransform.anchoredPosition += eventDeltaSinY;
            rTransform.anchoredPosition += eventDeltaSinY;
            showableNumber = CalculateShowableNumber(newAnchPosX);
            DisplaySnipetNumber();
        }
        else if (!isMaxSlider && newAnchPosX > maxSliderTransform.anchoredPosition.x)
        {
            maxSliderTransform.anchoredPosition += eventDeltaSinY;
            rTransform.anchoredPosition += eventDeltaSinY;
            showableNumber = CalculateShowableNumber(newAnchPosX);
            DisplaySnipetNumber();
        }
        else
        {
            rTransform.anchoredPosition += eventDeltaSinY;
            anchPosX = rTransform.anchoredPosition.x;
            showableNumber = CalculateShowableNumber(newAnchPosX);
            DisplaySnipetNumber();
        }
    }

    #endregion

    #region CalculateUsableValue
    private void DisplaySnipetNumber()
    {
        snippet.SetActive(true);
        snippetBox.SetActive(true);

        Text sText = snippet.GetComponent<Text>();
        sText.text = showableNumber.ToString();
    }

    private int CalculateShowableNumber(float _slidePosition)
    {
        int maxParameter = 600;
        int minParameter = 250;
        int inRangeNumber = 0;

        switch (sType)
        {
            case SliderMovType.Horizontal:
                inRangeNumber = Mathf.RoundToInt(((_slidePosition * maxParameter) / maxPosition));
                return showableNumber;
            case SliderMovType.Vertical:
                inRangeNumber = GetInRangeNumber(_slidePosition);
                showableNumber = Mathf.RoundToInt(((inRangeNumber * maxParameter) / distRange) + minParameter);
                return showableNumber;
        }
        return showableNumber;
    }

    private int GetInRangeNumber(float _slidePosition)
    {
        if (_slidePosition > 0)
        {
            return Mathf.RoundToInt(_slidePosition + (distRange / 2));
        }
        else
        {
            return Mathf.RoundToInt(Mathf.Abs(minPosition) + (_slidePosition));
        }
    }

    private int CalculateShowableNumber()
    {
        int maxParameter = 850;
        if (sType == SliderMovType.Horizontal)
        {
            Debug.Log("Object: " + rTransform.gameObject.name + ".  Transform X = " + rTransform.anchoredPosition.x + ". maxParameter is: " + maxPosition + " + 250");
            showableNumber = Mathf.RoundToInt(((rTransform.anchoredPosition.x * maxParameter) / maxPosition) + 250);
        }
        else
        {
            Debug.Log("Object: " + rTransform.gameObject.name + ".  Transform Y = " + rTransform.anchoredPosition.y + ". maxParameter is: " + maxPosition + " + 250");
            showableNumber = Mathf.RoundToInt(((rTransform.anchoredPosition.y * maxParameter) / maxPosition) + 250);
        }
        return showableNumber;
    }

    public int GetShowableNumber()
    {
        return showableNumber;
    }

    public void UpdateMyShowableNumber()
    {
        showableNumber = CalculateShowableNumber();
    }
    #endregion

    #endregion

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        Debug.Log(eventData.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        snippet.SetActive(false);
        snippetBox.SetActive(false);
        //dUManager.FilterDataRange(minSlideHandler.GetShowableNumber(), maxSlideHandler.GetShowableNumber());
    }
}

