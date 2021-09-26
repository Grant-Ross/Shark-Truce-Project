using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedObject : MonoBehaviour
{
    public ColorManager.GameColor color;
    
    public bool startEnabled;
    [SerializeField] protected SpriteRenderer spriteRenderer;


    protected bool objectEnabled;
    protected void Start()
    {
        LeverManager.LeverChangeListener += LeverChangeCheck;
        spriteRenderer.color = ColorManager.GetHue(color);
        SetObjectState(startEnabled);
    }

    protected void OnDestroy()
    {
        LeverManager.LeverChangeListener -= LeverChangeCheck;
    }

    private void LeverChangeCheck(ColorManager.GameColor activeColor, bool active)
    {
        if (color != activeColor) return;
        SetObjectState(startEnabled ? !active : active);
    }
    
    protected virtual void SetObjectState(bool active){}
}
