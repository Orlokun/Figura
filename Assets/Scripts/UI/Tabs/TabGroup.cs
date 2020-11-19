using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public TabButton selTab;
    public List<TabButton> tButtons;

    private string onSelColor = "#88F3FA";
    private string onEnterColor = "#ECECEC";
    private string onExitColor = "#FFFFFF";

    private void Start()
    {
        ResetTabs();
    }


    public void Subscribe(TabButton _button)
    {
        if (tButtons == null)
        {
            tButtons = new List<TabButton>();
            selTab = _button;
        }
        tButtons.Add(_button);
    }

    public void OnTabEnter(TabButton _button)
    {
        ResetTabs();
        Color _col;
        ColorUtility.TryParseHtmlString(onEnterColor, out _col);
        foreach (TabButton tButton in tButtons)
        {
            if (tButton == _button && selTab != _button)
            {
                tButton.bGround.color = _col;
            }
        }
    }

    public void OnTabSelecter(TabButton _button)
    {
        selTab = _button;
        ResetTabs();
        Color _col;
        ColorUtility.TryParseHtmlString(onSelColor, out _col);
        foreach (TabButton tButton in tButtons)
        {
            if (tButton == _button)
            {
                tButton.bGround.color = _col;
            }
        }
    }

    public void OnTabExit(TabButton _button)
    {
        ResetTabs();
        Color _col;
        ColorUtility.TryParseHtmlString(onExitColor, out _col);
        foreach (TabButton tButton in tButtons)
        {
            if (tButton == _button && selTab != _button)
            {
                tButton.bGround.color = _col;
            }
        }
    }

    public void ResetTabs()
    {
        Color _col;
        ColorUtility.TryParseHtmlString(onExitColor, out _col);

        foreach (TabButton tButton in tButtons)
        {
            if (selTab != null && selTab == tButton) {continue;}
            {
                tButton.bGround.color = _col;
            }
        }
    }
}
