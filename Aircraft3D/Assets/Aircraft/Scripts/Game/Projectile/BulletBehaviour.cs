using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BulletBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;

    //public PlaneHealth planeHealth;

    public enum MyPart
    {
        leftwing,
        rightWing
    }

    public MyPart part;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        Debug.Log("对敌人造成伤害");
        Debug.Log("给出集中反馈，包含镜头震动");
        Debug.Log("删除子弹，播放击中特效");
        Camera.main.transform.DOShakePosition(0.8f, 1, 12, 90, false, true);
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.SetActive(true);
        Destroy(gameObject);
    }


}
