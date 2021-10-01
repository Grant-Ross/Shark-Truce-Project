using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FinishPlate : MonoBehaviour
{
    [SerializeField] private SpriteRenderer topBun;

    private bool finished = false;
    public void StartFinishSequence()
    {
        if (finished) return;
        finished = true;
        StartCoroutine(FinishSequence());
    }

    private IEnumerator FinishSequence()
    {
        var bunPos = topBun.gameObject.transform.position.y;
        
        var t = topBun.gameObject.transform.DOMoveY(bunPos + 7, .5f).SetEase(Ease.OutCubic);
        while (t.IsPlaying()) yield return null;
        topBun.sortingLayerName = "Character";
        topBun.sortingOrder = 10;
        topBun.gameObject.transform.DOMoveY(bunPos + 2.5f, .5f).SetEase(Ease.InCubic);
    }
    
    
}
