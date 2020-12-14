using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ClosePopUpButton : ButtonScript, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private string baseColor = "#EEFFFF";
    private string mouseOvercol = "#FCFFD3";
    private static LTDescr delay;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        RotateZAxis(degrees, onMouseAnimtime);
        Inflate(new Vector3(1.2f, 1.2f, 1f), .7f);
        Colorate(mouseOvercol);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        GameObject[] activePopUps = GameObject.FindGameObjectsWithTag("PopUp");
        foreach (GameObject pUp in activePopUps)
        {
            if (pUp.activeInHierarchy)
            {
                pUp.SetActive(false);
            }
        }
    }
}
