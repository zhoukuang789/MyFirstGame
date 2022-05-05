using System.Collections;
using UnityEngine;

public class PlayerPlane : PlaneBehaviour
{

    public int a = 0;
    protected override void Start()
    {
        base.Start();
        Debug.Log("此处有测试代码");
        return;

        Debug.Log("开始协程测试");

        StartCoroutine(Test1());
        StartCoroutine("Test2");
    }


    IEnumerator Test1()
    {
        yield return new WaitForSeconds(2);

        var data = new PopupBehaviour.PopupData();
        data.title = "Hello";
        data.callback1 = () => { Debug.Log("cb1"); this.health.ReceiveDamage(9999); };
        data.callback2 = () => { Debug.Log("cb2"); this.health.ReceiveDamage(-9999); };
        data.ShowBtn2 = true;
        data.btn1Txt = "自杀";
        data.btn2Txt = "heal";

        PopupBehaviour.instance.Show(data);

    }

    IEnumerator Test2()
    {
        while (a < 10)
        {
            a++;
            if (health.IsDead())
            {
                break;
            }
            yield return null;
            com.SoundService.instance.Play("click");
        }
    }
}
