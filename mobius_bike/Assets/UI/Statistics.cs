using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public static event Action OnAchievementsChanged = () => {};
    public static event Action OnStatsChanged = () => {};

    public List<AchievementData> achievementDatas = new List<AchievementData>();

    public int Gold;

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            var laps = i + 1;
            achievementDatas.Add(new AchievementData(
                text: $"Проедь {laps} кругов!", 
                maxAmount: laps,
                reward: laps * 10));
        }
    }

    public void IncreaseLaps()
    {
        var achievementData = achievementDatas.FirstOrDefault(_ => _.CurrentAmount < _.MaxAmount);
        if (achievementData != null)
            achievementData.CurrentAmount++;
        OnAchievementsChanged();
    }

    internal void GetReward(AchievementData data)
    {
        Gold += data.Reward;
        achievementDatas.Remove(data);
        OnAchievementsChanged();
        OnStatsChanged();
    }
}
