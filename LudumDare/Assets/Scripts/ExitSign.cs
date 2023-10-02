using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    public string SceneName;
    private float startTime = 0;

    void Start()
    {
        startTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (LevelRequirements.Singleton.Satisfied() && other.gameObject.GetComponent<Player>() != null)
        {
            DataStore.LastRunTime = Time.time - startTime;
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        }
    }
}
