using UnityEngine;

public class BombBulletBehaviour : BulletBehaviour
{
    protected override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("BombBulletBehaviour OnTriggerEnter");
        //Debug.Log(other);
        //Debug.Log("对敌人造成伤害");
        //Debug.Log("给出集中反馈，包含镜头震动");
        //Debug.Log("删除子弹，播放击中特效");
        var cb = other.GetComponent<ConstructureBehaviour>();
        if (cb != null)
        {
            //Debug.Log("对敌人造成伤害" + damgeValue);
            com.SoundService.instance.Play("exp" + Random.Range(1, 3));
            cb.ReceiveDamage(damgeValue);
            DieUnsilent();
        }
    }
}