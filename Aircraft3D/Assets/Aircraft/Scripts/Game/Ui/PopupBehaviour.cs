using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PopupBehaviour : MonoBehaviour
{
    public class PopupData
    {
        public bool showCloseBtn=true;
        public bool ShowBtn1=true;
        public bool ShowBtn2=false;
        public Action callback1=null;
        public Action callback2 = null;
        public string title="hi";
        public string desc;
        public string btn1Txt="Ok";
        public string btn2Txt = "Cancel";
    }

    public Text title;
    public Text desc;
    public Text btn1Txt;
    public Text btn2Txt;

    public GameObject btn1;
    public GameObject btn2;
    public GameObject closeBtn;

    public static PopupBehaviour instance;

    private PopupData _data;

    public CanvasGroup cg;
    public RectTransform rectTransform;

    public void Awake()
    {
        instance = this;
        Hide();
    }

    public void OnClickClose()
    {
        com.SoundService.instance.Play("click");
        Hide();
    }

    void Hide()
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void OnClickButton1()
    {
        Debug.Log("OnClickButton1");
        _data.callback1?.Invoke();
    }
    public void OnClickButton2()
    {
        Debug.Log("OnClickButton2");
        _data.callback2?.Invoke();
    }

    void ShowAnim()
    {
        cg.alpha = 1;
        rectTransform.localScale = Vector3.zero;
        rectTransform.DOScale(1, 0.5f).SetEase(Ease.OutCubic).OnComplete(
            () =>
            {
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
          );
    }

    public void Show(PopupData data)
    {
        _data = data;
        title.text = _data.title;
        desc.text = _data.desc;

        if (_data.showCloseBtn)
        {
            closeBtn.SetActive(true);
        }
        else
        {
            closeBtn.SetActive(false);
        }

        if (_data.ShowBtn1)
        {
            btn1.SetActive(true);
            btn1Txt.text = _data.btn1Txt;
        }
        else
        {
            btn1.SetActive(false);
        }
        if (_data.ShowBtn2)
        {
            btn2.SetActive(true);
            btn2Txt.text = _data.btn2Txt;
        }
        else
        {
            btn2.SetActive(false);
        }
        ShowAnim();
    }
}
