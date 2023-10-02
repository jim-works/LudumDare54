using UnityEngine;

public static class Util
{
    public static int ChooseFromWeightedArray(float[] weights, float totalWeight)
    {
        float weight = Random.Range(0f, totalWeight);
        int idx = 0;
        for (; idx < weights.Length; idx++)
        {
            weight -= weights[idx];
            if (weight <= 0)
            {
                break;
            }
        }
        return System.Math.Min(idx, weights.Length - 1);
    }
}