using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public TabButton selTab;
    public List<TabButton> tButtons;

    private string onSelColor = "#CDF5EE";
    private string onEnterColor = "#ECECEC";
    private string onExitColor = "#FFFFFF";

    private void Start()
    {
        SortList();
        ResetTabs();
    }

    public void Subscribe(TabButton _button)
    {
        if (tButtons.Count == 0)
        {
            tButtons = new List<TabButton>();
        }
        if (_button.isStartingButton)
        {
            selTab = _button;
            Color _col;
            ColorUtility.TryParseHtmlString(onSelColor, out _col);
            selTab.bGround.color = _col;
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

    public void SortList()
    {
        tButtons.Reverse();
    }
}
