using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddButton : ButtonScript, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private string baseColor = "#EEFFFF";
    private string mouseOvercol = "#FCFFD3";
    private static LTDescr delay;

    protected override void Awake()
    {
        base.Awake();
        Colorate(baseColor);
        TogglePopUp(false);
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        delay = LeanTween.delayedCall(0.01f, () =>
        {
            TogglePopUp(true);
        });
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        RotateZAxis(degrees, onMouseAnimtime);
        Inflate(new Vector3(1.2f, 1.2f, 1f), .7f);
        Colorate(mouseOvercol);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        Inflate(new Vector3(1f, 1f, 1f), .7f);
        RotateZAxis(-degrees, onMouseAnimtime);
        Colorate(baseColor);
    }

    protected override void TogglePopUp(bool _val)
    {
        base.TogglePopUp(_val);
    }
}
