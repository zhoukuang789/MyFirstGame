using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BulletBehaviour : ProjectileBehaviour
{
    [Tooltip("阵营")]
    public Allies allies;

    public int damgeValue = 40;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("BulletBehaviour OnTriggerEnter");
        //Debug.Log("对敌人造成伤害");
        //Debug.Log("给出集中反馈，包含镜头震动");
        //Debug.Log("删除子弹，播放击中特效");

        var part = PlaneHitCollider.PlaneHitPart.Other;

        var plane = other.GetComponent<PlaneBehaviour>();
        if (plane == null)
        {
            var hc = other.GetComponent<PlaneHitCollider>();
            if (hc != null)
            {
                plane = hc.plane;
                part = hc.part;
            }
        }

        if (plane != null)
            plane.health?.ReceiveDamage(damgeValue, part);

        DieUnsilent();
    }
}
