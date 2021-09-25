using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : ActivatedObject


    protected override void LeverChangeCheck(ColorManager.GameColor activeColor, bool active)
    {
        if (color != activeColor) return;
        SetObjectState(active);
    }
}
