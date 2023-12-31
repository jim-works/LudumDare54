using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        ButtonScene,
        SampleScene,
        EndScene
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.SampleScene.ToString());
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadButtonScene()
    {
        SceneManager.LoadScene("ButtonScene");
    }

    public void LoadNewGameOrEndCutscene()
    {
        if (DataStore.RemainingMobDebt <= 0 && !DataStore.WinCutscenePlayed)
        {
            DataStore.WinCutscenePlayed = true;
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            LoadNewGame();
        }
    }
}
