using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataStore
{
    public static int mobMoney;
    public static int BankedMoney = 100;
    public static float LastRunTime;
    public static float ToalRunTime;
    public static List<Item> itemsSold = new();
}
