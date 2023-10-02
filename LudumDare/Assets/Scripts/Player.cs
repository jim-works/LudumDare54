using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Inventory), typeof(KeyboardMovementInput), typeof(Control))]
[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public enum State
    {
        Default,
        DiggingThroughGarbage
    }
    public float InteractRadius = 3;
    public float PickpocketAlarmIncrement = 0.25f;
    public PickpocketUI PickpocketUI;
    public GameObject DepositUI;
    public GameObject GarbageDigUI;
    public LuggageCartUI LuggageCartUI;
    public VendingMachineUI VendingMachineUI;

    public GarbageCanManager GarbageCanManager;
    public State MyState { get; set; }

    public Color InvulnerabilityColor;
    public float InvulnerabilityFlashInverval = 0.1f;
    public string GameOverScene = "GameOverScene";
    public List<(VendingBuff, int)> Buffs = new();
    private float startTime;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value < health && Time.time - lastHitTime < InvulnerabilityDuration && Time.time - LastFartTime < InvulnerabilityDuration)
            {
                //invulnerability time
                return;
            }
            if (health <= 0)
            {
                Die();
                return;
            }
            if (value < health)
            {
                //got hit
                OnHit();
            }
            health = value;
            OnHealthChanged.Invoke(value);
        }
    }
    public UnityEvent<int> OnHealthChanged;
    [SerializeField]
    private int health = 2;
    public float InvulnerabilityDuration = 0.5f;
    public float KnockbackMult = 1;
    private float lastHitTime;
    public float LastFartTime;

    public Inventory Inventory;
    public FinderUI FinderUI;
    public Teleporter.TeleporterLocation CurrentLocation;
    private KeyboardMovementInput input;
    private Control control;
    private SpriteRenderer sr;
    private Collider2D col;
    private IEnumerator updatePhasedCollisionCoroutine;
    private Rigidbody2D rb;
    void Start()
    {
        Inventory = GetComponent<Inventory>();
        input = GetComponent<KeyboardMovementInput>();
        control = GetComponent<Control>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.mass = DataStore.GymMultipler;
        control.MoveAcceleration *= DataStore.GymMultipler;
        control.MoveSpeed *= DataStore.GymMultipler;
        startTime = Time.time;
        OnHealthChanged.Invoke(health);
    }
    // Update is called once per frame
    void Update()
    {
        if (DoItemDepositing())
        {
            VendingMachineUI.gameObject.SetActive(false);
            PickpocketUI.gameObject.SetActive(false);
            GarbageDigUI.SetActive(false);
            LuggageCartUI.gameObject.SetActive(false);
        }
        else if (DoGarbageDig())
        {
            VendingMachineUI.gameObject.SetActive(false);
            PickpocketUI.gameObject.SetActive(false);
            LuggageCartUI.gameObject.SetActive(false);
        }
        else if (DoVending())
        {
            PickpocketUI.gameObject.SetActive(false);
            LuggageCartUI.gameObject.SetActive(false);
        }
        else if (DoPickpocket())
        {
            LuggageCartUI.gameObject.SetActive(false);
        }
        else
        {
            DoLuggageCarting();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            DataStore.BankedMoney += 100;
        }
        UpdateFinderUI();
        switch (MyState)
        {
            case State.Default:
                input.enabled = true;
                break;
            case State.DiggingThroughGarbage:
                input.enabled = false;
                control.MoveDirection = Vector2.zero;
                break;
        }
    }

    public void AddBuff(VendingBuff buff)
    {
        int idx = Buffs.FindIndex(x => x.Item1 == buff);
        if (idx >= 0)
        {
            buff.OnStackIncreased(gameObject, Buffs[idx].Item2);
            Buffs[idx] = (Buffs[idx].Item1, Buffs[idx].Item2 + 1);
        }
        else
        {
            Buffs.Add((buff,1));
            buff.OnFirstApplied(gameObject);
        }
    }
    //returns if in range
    private bool DoPickpocket()
    {
        GameObject civ = ObjectRegistry.Singleton.GetClosestCivilian(transform.position, InteractRadius);
        PickpocketUI.gameObject.SetActive(civ != null);
        if (civ != null)
        {
            PickpocketUI.DisplayFor(civ.GetComponent<Inventory>(), 10);
            return true;
        }
        return false;
    }
    private bool DoGarbageDig()
    {
        if (GarbageCanManager.SpecialGarbageCan == null) return false;
        bool inRange = Vector3.Distance(GarbageCanManager.SpecialGarbageCan.transform.position, transform.position) <= InteractRadius;
        GarbageDigUI.SetActive(inRange);
        return inRange;
    }
    private bool DoItemDepositing()
    {
        if (Inventory.Item == null)
        {
            DepositUI.SetActive(false);
            return false;
        }
        ItemReceiver recv = ObjectRegistry.Singleton.GetClosestItemReciever(transform.position, InteractRadius);
        DepositUI.SetActive(recv != null);
        return recv != null;
    }
    private bool DoLuggageCarting()
    {
        LuggageCart cart = ObjectRegistry.Singleton.GetClosestLuggageCart(transform.position, InteractRadius);
        LuggageCartUI.gameObject.SetActive(cart != null);
        if (cart != null)
        {
            LuggageCartUI.DisplayFor(cart);
        }
        return cart != null;
    }
    private bool DoVending()
    {
        VendingMachine vend = ObjectRegistry.Singleton.GetClosestVendingMachine(transform.position, InteractRadius);
        VendingMachineUI.gameObject.SetActive(vend != null);
        if (vend != null)
        {
            VendingMachineUI.DisplayFor(vend);
        }
        return vend != null;
    }
    public void OnPickpocket(Inventory target, PickPocketResult result)
    {
        if (result == PickPocketResult.Success)
        {
            Inventory.SwapInventory(target);
            Alarm.IncreaseAlarm(PickpocketAlarmIncrement);
        }
    }
    private void OnHit()
    {
        lastHitTime = Time.time;
        StartCoroutine(FlashOnHit());
        //have to stop the old coroutine so it doesn't disable phased movement before the new event is finisehd
        if (updatePhasedCollisionCoroutine != null)
        {
            StopCoroutine(updatePhasedCollisionCoroutine);
        }
        updatePhasedCollisionCoroutine = UpdateInvulnerabilityCollision();
        StartCoroutine(updatePhasedCollisionCoroutine);
    }
    void UpdateFinderUI()
    {
        switch (FinderUI.GetState())
        {
            case FinderUI.State.Garbage:
                if (LevelRequirements.Singleton.RequiredDrug.Item == null || Inventory.Item == LevelRequirements.Singleton.RequiredDrug.Item)
                {
                    FinderUI.SetState(CurrentLocation == Teleporter.TeleporterLocation.Starting ? FinderUI.State.StartingTeleporter : FinderUI.State.MafiaGuy);
                }
                break;
            case FinderUI.State.StartingTeleporter:
                if (CurrentLocation == Teleporter.TeleporterLocation.Destination)
                {
                    FinderUI.SetState(FinderUI.State.MafiaGuy);
                }
                break;
            case FinderUI.State.MafiaGuy:
                if (LevelRequirements.Singleton.Satisfied())
                {
                    FinderUI.SetState(FinderUI.State.Exit);
                }
                break;
        }
        //reset if requirements are not met
        if (LevelRequirements.Singleton.RequiredDrug.Item != null && LevelRequirements.Singleton.RequiredDrug.Item != Inventory.Item)
        {
            FinderUI.SetState(FinderUI.State.Garbage);
        }
        else if (LevelRequirements.Singleton.RequiredMoney > 0)
        {
            FinderUI.SetState(CurrentLocation == Teleporter.TeleporterLocation.Starting ? FinderUI.State.StartingTeleporter : FinderUI.State.MafiaGuy);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("guard") && collision.gameObject.GetComponent<Guard>().AIState == Guard.State.Chasing)
        {
            Health -= 1;
            rb.AddForce((transform.position - collision.transform.position).normalized * KnockbackMult, ForceMode2D.Impulse);
        }
    }
    private IEnumerator FlashOnHit()
    {
        while (Time.time - lastHitTime < InvulnerabilityDuration - InvulnerabilityFlashInverval)
        {
            sr.color = InvulnerabilityColor;
            yield return new WaitForSeconds(InvulnerabilityFlashInverval);
            sr.color = Color.white;
            yield return new WaitForSeconds(InvulnerabilityFlashInverval);
        }
    }
    private IEnumerator UpdateInvulnerabilityCollision()
    {
        col.excludeLayers |= LayerMask.GetMask("guard");
        yield return new WaitForSeconds(InvulnerabilityDuration);
        col.excludeLayers ^= LayerMask.GetMask("guard");
    }
    private void Die()
    {
        DataStore.LastRunTime = Time.time - startTime;
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameOverScene);
    }
}
