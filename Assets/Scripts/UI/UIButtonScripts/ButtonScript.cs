using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    protected List<GameObject> childrens;
    [SerializeField]
    protected GameObject[] rotatingObjects;
    [SerializeField]
    protected float degrees;
    [SerializeField]
    protected GameObject[] inflatingObjects;
    [SerializeField]
    protected GameObject[] coloringObjects;
    [SerializeField]
    protected GameObject[] popUps;

    public bool popUpActive;


    [SerializeField]
    protected float onMouseAnimtime;

    protected virtual void Awake()
    {
        CheckInitialConditions();
    }

    protected virtual void CheckInitialConditions()
    {
        SetChildren();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //Each button has its implementation
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //Each button has its implementation
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //Each button has its implementation
    }

    #region Utils
    protected void SetChildren()
    {
        if (transform.childCount >= 1)
        {
            childrens = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                childrens.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    protected void RotateZAxis(float degrees, float time)
    {
        foreach (GameObject gObj in rotatingObjects)
        {
            if (!gameObject.LeanIsTweening())
            {
                LeanTween.rotateAround(gObj, Vector3.forward, degrees, time).setEase(LeanTweenType.easeInOutCubic);
            }
        }
    }

    protected void Inflate(Vector3 newSize, float time)
    {
        if (inflatingObjects.Length > 0)
        {
            foreach (GameObject gObj in inflatingObjects)
            {
                if (!gameObject.LeanIsTweening())
                {
                    LeanTween.scale(gObj, newSize, time).setEase(LeanTweenType.easeInOutBounce);
                }
            }
        }
    }

    protected void Colorate(string _color)
    {
        if (inflatingObjects.Length > 0)
        {
            foreach (GameObject gObj in coloringObjects)
            {
                Color _col;
                ColorUtility.TryParseHtmlString(_color, out _col);
                Image img = gObj.GetComponent<Image>();
                img.color = _col;
            }
        }
    }

    protected virtual void TogglePopUp(bool _val)
    {
        if (popUps.Length > 0)
        {
            foreach(GameObject gObj in popUps)
            {
                gObj.SetActive(_val);
            }
        }
    }

    #endregion
}
