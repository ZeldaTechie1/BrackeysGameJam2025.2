using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class VampiricDrain:BasicAOEModifier
{
    public static Func<int,float> GetPlayerScore;
    public static Func<int> GetCurrentPlayer;
    public static Action<int, float> RemovePlayerScore;

    public override float ModifyScoreValue(float score)
    {
        float newScore = -1;
        int player = -1;
        if (GetCurrentPlayer != null)
        {
            player = GetCurrentPlayer();
            player = player == 0 ? 1 : player == 1 ? 0 : -1;
        }
        if(GetPlayerScore != null)
        {
            newScore = GetPlayerScore(player) * Random.Range(.10f,.15f);
        }

        if (newScore < 0 || player < 0)
        {
            Debug.LogError("Vampire Drain broke!");
        }

        RemovePlayerScore?.Invoke(player, newScore);

        return newScore;
    }
}

