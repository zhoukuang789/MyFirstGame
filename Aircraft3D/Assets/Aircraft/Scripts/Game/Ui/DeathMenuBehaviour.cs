using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using com;
using UnityEngine.SceneManagement;

public class DeathMenuBehaviour : MonoBehaviour
{
    public CanvasGroup cg;
    public GameObject[] btns;
    public static DeathMenuBehaviour instance;

    void Start()
    {
        instance = this;
        Hide();
    }

    public void Hide()
    {
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        foreach (var btn in btns) { btn.SetActive(false); }
    }

    public void Show()
    {
        cg.blocksRaycasts = true;
        cg.DOFade(1, 2).SetDelay(0.25f).OnComplete(() =>
        {
            foreach (var btn in btns) { btn.SetActive(true); }
        });
    }

    public void OnClickRetry()
    {
        //com.SoundService.instance.Play("click");
        SceneService.instance.RestartScene();
    }

    public void OnClickQuit()
    {
        // com.SoundService.instance.Play("click");
        SceneService.instance.SwitchScene_Menu();
    }
}
