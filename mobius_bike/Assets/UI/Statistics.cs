using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public static event Action OnAchievementsChanged = () => {};
    public static event Action OnCollectionChanged = () => {};
    public static event Action OnStatsChanged = () => {};

    public List<AchievementData> achievementDatas = new List<AchievementData>();
    public List<CollectionItemData> collectionDatas = new List<CollectionItemData>();

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

    public void GetCollectionItem()
    {
        var randomData = GetRandomItemType();
        collectionDatas.Add(new CollectionItemData(
            text: randomData.type.ToString(),
            iconName: randomData.type.ToString(),
            price: randomData.price
            ));
        OnCollectionChanged();

        (CollectionItemType type, int price) GetRandomItemType()
        {
            var random = new System.Random(Guid.NewGuid().GetHashCode());
            var result = random.Next(0, Enum.GetValues(typeof(CollectionItemType)).Length - 1);
            return ((CollectionItemType)result, random.Next(100, 1001));
        }
    }
}
