using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject title;
    
    private void Start()
    {
        var titlePos = (title.transform as RectTransform).anchoredPosition.y;
        (title.transform as RectTransform).anchoredPosition = new Vector2((title.transform as RectTransform).anchoredPosition.x, 600);
        (title.transform as RectTransform).DOAnchorPosY( titlePos, 3).SetEase(Ease.OutBounce);
    }

    public void StartButton()
    {
        GameManager.Instance.LoadScene("LevelSelect");
    }
}
