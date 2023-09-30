using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinderUI : MonoBehaviour
{
    public enum State
    {
        Garbage,
        StartingTeleporter,
        MafiaGuy,
        Exit,
    }
    private State state;
    public Image Image;
    public RectTransform Arrow;

    public GarbageCanManager GarbageCanManager;
    public Sprite GarbageSprite;
    public Transform StartingTeleporter;
    public Sprite TeleporterSprite;
    public Transform Exit;
    public Sprite ExitSprite;
    public Transform MafiaGuy;
    public Sprite MafiaGuySprite;

    private Transform tracking;

    void Start()
    {
        SetState(State.Garbage);
    }

    public void SetState(State to)
    {
        switch (to)
        {
            case State.Garbage:
                tracking = GarbageCanManager.SpecialGarbageCan.transform;
                Image.sprite = GarbageSprite;
                break;
            case State.StartingTeleporter:
                tracking = StartingTeleporter;
                Image.sprite = TeleporterSprite;
                break;
            case State.MafiaGuy:
                tracking = MafiaGuy;
                Image.sprite = MafiaGuySprite;
                break;
            case State.Exit:
                tracking = Exit;
                Image.sprite = ExitSprite;
                break;
        }
    }

    void Update()
    {
        if (tracking == null)
        {
            return;
        }
        Vector2 diff = tracking.position - ObjectRegistry.Singleton.Player.transform.position;
        Arrow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90);
    }
}
