using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class GameScene
{
    public int index;
    public string desc;
    public bool needTransitionWhenLoaded;
}

public class SceneService : MonoBehaviour
{
    public static SceneService instance { get; private set; }

    public List<GameScene> gameScenes;

    private GameScene _crtGameScene;

    private void Awake()
    {
        instance = this;
    }

    public void SwitchScene_Menu()
    {
        SetScene(0);
    }

    public void OnClickSwitchScene_mission1()
    {
        SetScene(1);
    }

    public void SetScene(int i)
    {
        _crtGameScene = gameScenes[i];
        RestartScene();
    }

    public void RestartScene()
    {
        StartScene(_crtGameScene);
    }

    void StartScene(GameScene targetScene)
    {
        Debug.Log("StartScene " + targetScene.desc);
        StartCoroutine(LoadYourAsyncScene(targetScene.index, targetScene.needTransitionWhenLoaded));
    }

    IEnumerator LoadYourAsyncScene(int index, bool needTransition)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        int maxWait = 1000;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone && maxWait > 0)
        {
            maxWait--;
            Debug.Log(maxWait);
            yield return null;
        }
        Debug.Log("load scene done ");
        //PauseService.instance.Resume();
        if (needTransition)
        {
            //TransitionBehaviour.instance.ShowSmaller();
        }
        else
        {
            //TransitionBehaviour.instance.Hide();
        }
    }
}