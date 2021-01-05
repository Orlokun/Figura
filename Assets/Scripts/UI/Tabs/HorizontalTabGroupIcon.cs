using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTabGroupIcon : TabGroup
{
    protected override void Start()
    {
        base.Start();
        SetColors();
    }
    
    
    protected void SetColors()
    {
        onSelColor = "#CDF5EE";
        onEnterColor = "#ECECEC";
        onExitColor = "#FFFFFF";
    }

    public override void OnTabEnter(TabButton _button)
    {
        base.OnTabEnter(_button);
        SetOnEnterColor(_button);
        RotateZAxis(_button, 360f, .5f);        //TODO: Calculate angles left for 0. 
        TweenBGround();
        TweenText();
    }

    protected void TweenBGround()
    {

    }
    protected void TweenText()
    {

    }
}
