using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    public string SceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (LevelRequirements.Singleton.Satisfied() && other.gameObject.GetComponent<Player>() != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        }
    }
}
