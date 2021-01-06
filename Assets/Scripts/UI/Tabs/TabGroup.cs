using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public TabButton selTab;
    public List<TabButton> tButtons;

    protected float exitDelaytime = 1.1f;
    protected static LTDescr delay;
    protected string onSelColor;
    protected string onEnterColor;
    protected string onExitColor;

    protected virtual void Start()
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

    public virtual void OnTabEnter(TabButton _button)
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

    public virtual void OnTabSelecter(TabButton _button)
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

    public virtual void OnTabExit(TabButton _button)
    {

    }

    protected virtual void ResetTabs()
    {
        //Different Method  for vertical and horizontal tabs
    }

    protected virtual void SortList()
    {
        tButtons.Reverse();
    }


    protected void RotateZAxis(TabButton _button, float degrees, float time)
    {
        if (_button.rotatingObjects.Length > 0)
        {
            foreach (GameObject gObj in _button.rotatingObjects)
            {
                if (!gameObject.LeanIsTweening())
                {
                    
                    LeanTween.rotateAround(gObj, Vector3.forward, degrees, time).setEase(LeanTweenType.easeInOutCubic);
                }
            }
        }
    }

    protected void SetOnEnterColor(TabButton _button)
    {
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

    protected void SetOnExitColor(TabButton _button)
    {
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
}
