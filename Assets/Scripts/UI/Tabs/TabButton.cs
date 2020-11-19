using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour,IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tGroup;

    public Image bGround;
    void Awake()
    {
        bGround = GetComponent<Image>();
        tGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tGroup.OnTabSelecter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tGroup.OnTabExit(this);
    }
}
