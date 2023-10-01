using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIEnd : MonoBehaviour
{
    [SerializeField] Button _least;
    [SerializeField] Button _middle;
    [SerializeField] Button _most;


    private int totalScore;
    private int profit;


    public TMP_Text textProfit;
    public TMP_Text textLost;

    public Image most;
    public Image middle;
    public Image least;
    public Sprite mostSprite;
    public Sprite middleSprite;
    public Sprite leastSprite;

    private IEnumerator updatingCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        _least.onClick.AddListener(leastClicked);
        _middle.onClick.AddListener(middleClicked);
        _most.onClick.AddListener(mostClicked);


        totalScore = 0;
        /*for (int i = 0; i < DataStore.itemsSold.Count; i++)
        {
            totalScore = totalScore + DataStore.itemsSold[i].CashValue;
        }*/

        profit = totalScore - DataStore.mobMoney;

        textProfit.text = "Total Profit: $" + profit.ToString();
        textLost.text = "Mob Took: $" + DataStore.mobMoney.ToString();
    }

    private void startRoutine()
    {
        updatingCoroutine = WaitSleep();
        StartCoroutine(updatingCoroutine);
    }
    private void leastClicked()
    {
        least.sprite = leastSprite;
        startRoutine();
    }

    private void middleClicked()
    {
        middle.sprite = middleSprite;
        startRoutine();
    }

    private void mostClicked()
    {
        most.sprite = mostSprite;
        startRoutine();
    }

    private IEnumerator WaitSleep() {
        yield return new WaitForSeconds(2f);
        ScenesManager.Instance.LoadNewGame();
    }

}


