using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageDigUI : MonoBehaviour
{
    public GameObject DigMinigame;
    public GameObject DigText;
    private bool digging = false;
    private GameObject mainCamera;
    void Update()
    {
        Player p = ObjectRegistry.Singleton.Player;
        if (p.MyState == Player.State.DiggingThroughGarbage) {
            return;
        } else if (digging) {
            StopDigging(p);
        }
        if (!digging && Input.GetKeyDown(KeyCode.E)) {
            StartDigging(p);
        }
    }

    private void StartDigging(Player p)
    {
        mainCamera = Camera.main.gameObject;
        mainCamera.SetActive(false);
        DigText.SetActive(false);
        DigMinigame.SetActive(true);
        p.MyState = Player.State.DiggingThroughGarbage;
        digging = true;
    }
    private void StopDigging(Player p)
    {
        mainCamera.SetActive(true);
        DigText.SetActive(true);
        DigMinigame.SetActive(false);
        digging = false;
    }
}
