using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{

    private static TooltipSystem current;
    public Tooltip tTip;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
        Hide();
    }

    public static void Show(string content, string header = "")
    {
        current.tTip.SetText(content, header);
        current.tTip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tTip.gameObject.SetActive(false);
    }
}
