using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class VerticalTabGroup : TabGroup
{

    protected override void Start()
    {
        base.Start();
        foreach (TabButton tButton in tButtons)
        {
            ToggleObjectsActive(tButton.displayableObjects, false, .1f);
        }
        SetBaseColors();
    }

    protected void SetBaseColors()
    {
        onSelColor = "#CDF5EE";
        onEnterColor = "#ECECEC";
        onExitColor = "#FFFFFF";
    }

    public override void OnTabEnter(TabButton _button)
    {
        if (selTab != _button)
        {
            selTab = _button;
            ResetTabs();
            SetOnEnterColor(_button);
            ToggleObjectsActive(_button.displayableObjects, true, .5f);
            RotateZAxis(_button, 360f, .7f);
            foreach (GameObject gObj in _button.displayableObjects)
            {
                UILeanTweenManager.ScaleAnimate(gObj, new Vector2(194f, 50f), .3f);
                UILeanTweenManager.MoveAnimate(gObj, 25.3f, .3f);
            }
        }
    }

    public override void OnTabExit(TabButton _button)
    {
        Debug.Log("Im out of:  " + _button.gameObject.name);
        StartCoroutine(ButtonExitWait(exitDelaytime, _button));
    }

    protected IEnumerator ButtonExitWait(float exitDelaytime, TabButton _button)
    {
        yield return new WaitForSeconds(exitDelaytime);

        selTab = null;
        SetOnExitColor(_button);
        ResetTabs();
    }

    protected void ToggleObjectsActive(GameObject[] gObjects, bool isActive, float time)
    {
        foreach (GameObject gObject in gObjects)
        {
            delay = LeanTween.delayedCall(0.5f, () =>
            {
                gObject.SetActive(isActive);
            });
        }
    }

    protected void ToggleObjectsActive(List<GameObject> gObjects, bool isActive)
    {
        foreach (GameObject gObject in gObjects)
        {
            gObject.SetActive(isActive);
        }
    }


    protected override void ResetTabs()
    {
        base.ResetTabs();

        foreach (TabButton tab in tButtons)
        {
            ToggleObjectsActive(tab.displayableObjects, false, .03f);
        }
    }
}
