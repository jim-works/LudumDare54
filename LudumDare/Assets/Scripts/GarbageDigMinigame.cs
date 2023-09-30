using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageDigMinigame : MonoBehaviour
{
    private enum State
    {
        Digging,
        SelectingItem
    }
    public Animation LidOpenAnim;
    public Animation HandsDigAnim;
    public ParticleSystem DigParticles;
    public GameObject FoundItemSprite;
    public AnimationCurve FoundItemScaleCurve;
    public GameObject DiggingUI;
    public GameObject SelectingUI;
    public Item[] Drops;
    public float[] DropWeights;
    public float FindItemProb = 0.1f;
    public float BaseDecisionTime = 10;
    public float DecisionTime => BaseDecisionTime;

    private float timeElapsed;
    private float decisionTimeElapsed;
    private float totalDropWeights;
    private SpriteRenderer foundItem;
    private State state;
    private Item currentDrop;
    private Inventory targetInventory;

    void Start()
    {
        foundItem = FoundItemSprite.GetComponent<SpriteRenderer>();
        targetInventory = ObjectRegistry.Singleton.Player.GetComponent<Inventory>();
    }
    void OnEnable()
    {
        LidOpenAnim.Play();
        timeElapsed = 0;
        FoundItemSprite.SetActive(false);
        foreach (float w in DropWeights)
        {
            totalDropWeights += w;
        }
        DiggingUI.SetActive(true);
        SelectingUI.SetActive(false);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        switch (state)
        {
            case State.Digging:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Dig();
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    EndGame();
                }
                break;
            case State.SelectingItem:
                decisionTimeElapsed += Time.deltaTime;
                FoundItemSprite.transform.localScale = Vector3.one * FoundItemScaleCurve.Evaluate(decisionTimeElapsed);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    targetInventory.PickupItem(currentDrop);
                    EndGame();
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    FoundItemSprite.SetActive(false);
                    state = State.Digging;
                    currentDrop = null;
                    DiggingUI.SetActive(true);
                    SelectingUI.SetActive(false);
                }
                break;
        }
    }

    private void Dig()
    {
        DigParticles.Play();
        HandsDigAnim.Play();
        if (Random.value > FindItemProb)
        {
            return;
        }
        float weight = Random.Range(0f, totalDropWeights);
        int idx = 0;
        for (; idx < Drops.Length; idx++)
        {
            weight -= DropWeights[idx];
            if (weight <= 0)
            {
                break;
            }
        }
        idx = System.Math.Max(idx, Drops.Length - 1);
        //switch to picking anim
        currentDrop = Drops[idx];
        foundItem.sprite = currentDrop.Icon;
        FoundItemSprite.SetActive(true);
        decisionTimeElapsed = 0;
        FoundItemSprite.transform.localScale = Vector3.one * FoundItemScaleCurve.Evaluate(0f);
        DiggingUI.SetActive(false);
        SelectingUI.SetActive(true);
    }

    private void EndGame()
    {
        ObjectRegistry.Singleton.Player.MyState = Player.State.Default;
    }
}
