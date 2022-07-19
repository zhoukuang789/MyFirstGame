using UnityEngine;

public class SinMoveMatYTiling : MonoBehaviour
{
    public float baseValue;
    public float amplitude;
    public float freq;
    public Material mat;

    void Update()
    {
        mat.mainTextureScale = new Vector2(0, baseValue + Mathf.Sin(Time.time * freq) * amplitude);
    }
}
