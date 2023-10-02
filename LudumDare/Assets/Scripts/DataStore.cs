using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataStore
{
    public static int RunCount = 0;
    public static int MobInterest;
    public static int RemainingMobDebt = 1000;
    public static int BankedMoney = 10000;
    public static float LastRunTime;
    public static float ToalRunTime;
    public static List<Item> itemsSold = new();

    public static int GymMemberships = 0;
    public static int Midases = 0;
    public static int Socks = 0;

    public static float MoneyMultipler => Mathf.Pow(1.1f, Midases);
    public static float AlertMultipler => Mathf.Pow(0.9f, Socks);
    public static float GymMultipler => Mathf.Pow(1.1f, GymMemberships);

    public static bool WinCutscenePlayed = false;
}
