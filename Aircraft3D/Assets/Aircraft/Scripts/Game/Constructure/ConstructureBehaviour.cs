using UnityEngine;

public class ConstructureBehaviour : MonoBehaviour
{
    public UnitHealth health;
    public int hp;
    public GameObject explodePrefab;
    public GameObject destroyPrefab;

    private bool _died;

    void Start()
    {
        health.SetValue(hp, hp);
        _died = false;
    }

    public void ReceiveDamage(float v)
    { ReceiveDamage((int)v); }

    public void ReceiveDamage(int v)
    {
        if (v > 0)
            HitFeedback();

        if (_died)
            return;

        var currentHp = health.hpCurrent;
        //Debug.Log("currentHp " + currentHp);
        currentHp -= v;
        if (currentHp <= 0)
        {
            Die();
            currentHp = 0;
        }

        health.SetValue(currentHp);
    }

    void Die()
    {
        _died = true;
        DieFeedback();
        Destroy(this.gameObject);
    }

    void DieFeedback()
    {
        var go = Instantiate(destroyPrefab, transform.position, Quaternion.identity, transform.parent);
        go.SetActive(true);
    }

    void HitFeedback()
    {
        var go = Instantiate(explodePrefab, transform.position, Quaternion.identity, transform.parent);
        go.SetActive(true);
    }
}
