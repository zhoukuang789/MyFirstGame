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

    private void Start()
    {
        _crtGameScene = gameScenes[1];
        RestartScene();
    }

    public void OnClickSwitchScene_AiTest()
    {
        _crtGameScene = gameScenes[1];
        RestartScene();
    }

    public void OnClickSwitchScene_sample()
    {
        _crtGameScene = gameScenes[2];
        RestartScene();
    }

    public void OnClickSwitchScene_bullet()
    {
        _crtGameScene = gameScenes[3];
        RestartScene();
    }
    public void OnClickSwitchScene_mission1()
    {
        _crtGameScene = gameScenes[4];
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

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
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