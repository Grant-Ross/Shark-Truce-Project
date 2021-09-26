using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorManager
{
    public enum GameColor
    {
        Red, Blue, Green
    }

    private static readonly Color[] colors = {new Color(248/255f,80/255f,120/255f), 
        new Color(141/255f,141/255f,248/255f), 
        new Color(100/255f,248/255f,100/255f)};
    public static Color GetHue(GameColor color){return colors[(int) color];}
}
