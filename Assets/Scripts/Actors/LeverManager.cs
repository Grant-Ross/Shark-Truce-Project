using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    
    public static event Action<ColorManager.GameColor, bool> LeverChangeListener;
    
    public static void OnLeverChange(ColorManager.GameColor color, bool active) {LeverChangeListener?.Invoke(color, active);}

    

}
