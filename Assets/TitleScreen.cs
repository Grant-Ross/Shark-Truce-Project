
using DG.Tweening;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject title;
    
    private void Start()
    {
        var titlePos = (title.transform as RectTransform).anchoredPosition.y;
        (title.transform as RectTransform).anchoredPosition = new Vector2((title.transform as RectTransform).anchoredPosition.x, 600);
        (title.transform as RectTransform).DOAnchorPosY( titlePos, 2.5f).SetEase(Ease.OutBounce);
    }

    public void StartButton()
    {
        GameManager.Instance.LoadScene("LevelSelect");
    }
}
