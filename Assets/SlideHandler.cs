using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlideHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    RectTransform minSliderTransform;
    [SerializeField]
    RectTransform maxSliderTransform;
    RectTransform rTransform;

    SlideHandler minSlideHandler;
    SlideHandler maxSlideHandler;


    public GameObject snippet;
    public GameObject snippetBox;

    float maxPosition;
    float minPosition;

    DimensionsArchitect dArchitect;


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

        dArchitect = FindObjectOfType<DimensionsArchitect>();
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

        else if (isMaxSlider && newAnchPosX<minSliderTransform.anchoredPosition.x)
        {
            minSliderTransform.anchoredPosition += eventDeltaSinY;
            rTransform.anchoredPosition += eventDeltaSinY;
            anchPosX = rTransform.anchoredPosition.x;
            DisplaySnipetNumber(anchPosX);
        }
        else if (!isMaxSlider && newAnchPosX > maxSliderTransform.anchoredPosition.x)
        {
            maxSliderTransform.anchoredPosition += eventDeltaSinY;
            rTransform.anchoredPosition += eventDeltaSinY;
            anchPosX = rTransform.anchoredPosition.x;
            DisplaySnipetNumber(anchPosX);
        }
        else
        {
            rTransform.anchoredPosition += eventDeltaSinY;
            anchPosX = rTransform.anchoredPosition.x;
            DisplaySnipetNumber(anchPosX);
        }
    }

    private void DisplaySnipetNumber(float incomingBarX)
    {
        int maxParameter = 600;
        snippet.SetActive(true);
        snippetBox.SetActive(true);
        showableNumber = Mathf.RoundToInt(((incomingBarX * maxParameter) / maxPosition) + 250);
        Debug.Log("The showable number is this: " + showableNumber);
        Text sText = snippet.GetComponent<Text>();
        sText.text = showableNumber.ToString();
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        snippet.SetActive(false);
        snippetBox.SetActive(false);
        dArchitect.FilterDataRange(minSlideHandler.GetShowabeNumber(), maxSlideHandler.GetShowabeNumber());
    }

    public int GetShowabeNumber()
    {
        return showableNumber;
    }

}

