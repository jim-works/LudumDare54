using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISceneChange : MonoBehaviour
{
    [SerializeField] Button _playGame;
    // Start is called before the first frame update
    void Start()
    {
        _playGame.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame(){
        ScenesManager.Instance.LoadNewGame();
    }
}
