using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlideHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Basic Data From Radar Scriptable
    public RadarGraphData rData;

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

    DataUnitsManager dUManager;


    [SerializeField]
    bool isMaxSlider;

    int showableNumber;

    private void Awake()
    {
        //Must Check Initial Parameters
        maxSlideHandler = maxSliderTransform.gameObject.GetComponent<SlideHandler>();
        minSlideHandler = minSliderTransform.gameObject.GetComponent<SlideHandler>();

        if (isMaxSlider)
        {
            rTransform = maxSliderTransform;
        }
        else
        {
            rTransform = minSliderTransform;
        }

        minPosition = minSliderTransform.anchoredPosition.x;
        maxPosition = maxSliderTransform.anchoredPosition.x;
        snippet.SetActive(false);
        snippetBox.SetActive(false);
        showableNumber =  CalculateShowableNumber();

        dUManager = FindObjectOfType<DataUnitsManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        Debug.Log(eventData.position);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 eventDeltaSinY = new Vector2(eventData.delta.x, rTransform.anchoredPosition.y);
        float anchPosX = rTransform.anchoredPosition.x;
        float newAnchPosX = anchPosX += eventData.delta.x;

        if (newAnchPosX < minPosition || newAnchPosX > maxPosition)
        {
            return;
        }

        else if (isMaxSlider && newAnchPosX < minSliderTransform.anchoredPosition.x)
        {
            minSliderTransform.anchoredPosition += eventDeltaSinY;
            minSlideHandler.CalculateShowableNumber();
            rTransform.anchoredPosition += eventDeltaSinY;
            showableNumber = CalculateShowableNumber(newAnchPosX);
            DisplaySnipetNumber();
        }
        else if (!isMaxSlider && newAnchPosX > maxSliderTransform.anchoredPosition.x)
        {
            maxSliderTransform.anchoredPosition += eventDeltaSinY;
            maxSlideHandler.CalculateShowableNumber();
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

    private void DisplaySnipetNumber()
    {
        snippet.SetActive(true);
        snippetBox.SetActive(true);

        Debug.Log("The showable number is this: " + showableNumber);
        Text sText = snippet.GetComponent<Text>();
        sText.text = showableNumber.ToString();
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        snippet.SetActive(false);
        snippetBox.SetActive(false);
        dUManager.FilterDataRange(minSlideHandler.GetShowableNumber(), maxSlideHandler.GetShowableNumber());
    }

    private int CalculateShowableNumber(float incomingBarX)
    {
        int maxParameter = rData.GetMaxScore() - rData.GetMinScore();
        showableNumber = Mathf.RoundToInt(((incomingBarX * maxParameter) / maxPosition) + rData.GetMinScore());
        return showableNumber;
    }
    private int CalculateShowableNumber()
    {
        int maxParameter = rData.GetMaxScore() - rData.GetMinScore();
        showableNumber = Mathf.RoundToInt(((rTransform.anchoredPosition.x * maxParameter) / maxPosition) + rData.GetMinScore());
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

}

