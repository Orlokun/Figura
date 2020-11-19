using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    protected List<GameObject> childrens;

    protected virtual void Awake()
    {
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
