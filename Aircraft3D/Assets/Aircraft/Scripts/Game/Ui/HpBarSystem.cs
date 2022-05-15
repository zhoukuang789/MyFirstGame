using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 血条管理系统
/// </summary>
public class HpBarSystem : MonoBehaviour
{
    public GameObject prefab;
    public RectTransform parent;

    public static HpBarSystem instance { get; private set; }

    public List<HpBar> bars;

    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Start()
    {

    }

    void Init()
    {
        bars = new List<HpBar>();
    }

    /// <summary>
    /// 接受一个可以显示血条的对象的注册
    /// </summary>
    /// <param name="health"></param>
    public void Register(UnitHealth health)
    {
        foreach (var bar in bars)
        {
            if (bar.health == health)
                return;
        }

        var newHpBarGo = Instantiate(prefab, parent);
        newHpBarGo.SetActive(true);
        var newHpBar = newHpBarGo.GetComponent<HpBar>();
        newHpBar.health = health;
        bars.Add(newHpBar);
        //Debug.Log("Register");
    }

    /// <summary>
    /// 清除一个注册
    /// </summary>
    /// <param name="health"></param>
    public void UnRegister(UnitHealth health)
    {
        foreach (var bar in bars)
        {
            if (bar.health == health)
            {
                bars.Remove(bar);
                if (bar != null && bar.gameObject != null)
                {
                    GameObject.Destroy(bar.gameObject);
                }
                //Debug.Log("UnRegister");
                break;
            }
        }
    }

    /// <summary>
    /// 管理所有已经注册的血条
    /// 管理包括了位置设置，是否显示
    /// </summary>
    private void Update()
    {
        foreach (var bar in bars)
        {
            var viewportPos = Camera.main.WorldToScreenPoint(bar.health.GetWorldPosition());
            bar.rect.anchoredPosition = new Vector2(viewportPos.x, viewportPos.y);
            if (viewportPos.z <= 0)
            {
                bar.Hide();
            }
            else
            {
                bar.Show();
                bar.SetValue();
            }
        }
    }
}