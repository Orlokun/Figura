using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PopUpBG : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject[] activePopUps = GameObject.FindGameObjectsWithTag("PopUp");
        foreach (GameObject pUp in activePopUps)
        {
            if (pUp.activeInHierarchy)
            {
                pUp.SetActive(false);
            }
        }
        gameObject.SetActive(false);
    }
}
