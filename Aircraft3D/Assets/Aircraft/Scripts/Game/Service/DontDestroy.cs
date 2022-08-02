using UnityEngine;
public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this.gameObject);
    }
}
