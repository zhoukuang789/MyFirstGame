using UnityEngine;
using DG.Tweening;

public class LoadingWindowBehaviour : MonoBehaviour
{
    public RectTransform panel;
    public RectTransform logo;

    float _logoY;

    private void Awake()
    {
        _logoY = logo.anchoredPosition.y;
    }

    void Start()
    {
        panel.localScale = Vector3.zero;
        panel.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutCirc).SetDelay(1.0f);

        Vector2 pos = logo.anchoredPosition;
        pos.y = -1300;
        logo.anchoredPosition = pos;
        logo.DOAnchorPosY(_logoY, 1).SetEase(Ease.OutElastic);
    }
}
