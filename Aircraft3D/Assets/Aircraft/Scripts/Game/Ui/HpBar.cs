using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider bar;
    public GameObject view;
    public UnitHealth health;
    public RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Show()
    {
        view.SetActive(true);
    }

    public void Hide()
    {
        view.SetActive(false);
    }

    public void SetValue()
    {
        bar.value = (float)health.hpCurrent / (float)health.hpMax;
    }
}
