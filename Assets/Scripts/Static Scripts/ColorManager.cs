using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorManager
{
    public enum GameColor
    {
        Red, Blue, Green
    }

    private static readonly Color[] colors = {Color.red, Color.blue, Color.green};
    public static Color GetHue(GameColor color){return colors[(int) color];}
}
