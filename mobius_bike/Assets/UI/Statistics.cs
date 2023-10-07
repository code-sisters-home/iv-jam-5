using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public static event Action OnAchievementsChanged = () => { };
    public static event Action OnCollectionChanged = () => { };
    public static event Action OnStatsChanged = () => { };
    public static event Action OnDied = () => { };

    public List<AchievementData> achievementDatas = new List<AchievementData>();
    public List<CollectionItemData> collectionDatas = new List<CollectionItemData>();

    [NonSerialized] public int Gold = 100;
    [NonSerialized] public int Gems = 10;
    public int Lifes { get; private set; }
    public static int MaxLifes = 3;

    private void Awake()
    {
        Lifes = MaxLifes;

        for (int i = 0; i < 5; i++)
        {
            var laps = i + 1;
            achievementDatas.Add(new AchievementData(
                text: $"ѕроедь {laps} кругов!", 
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

    public void GetMushroom(Mushroom mushroom)
    {
        var isEdible = mushroom.IsEdible ? "—ъедобный" : "ядовитый";
        collectionDatas.Add(new CollectionItemData(
            text: Mushroom.Name(mushroom.Type)+"\n"+ isEdible,
            iconName: mushroom.Sprite.name,
            price: Mushroom.Price(mushroom.Type)
            ));
        OnCollectionChanged();
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

    public void GetDamage()
    {
        Lifes--;
        OnStatsChanged();

        if (Lifes == 0)
            OnDied();
    }

    public void GetLife(int count)
    {
        Lifes += count;
        if (Lifes > MaxLifes)
            Lifes = MaxLifes;
        OnStatsChanged();
    }
}
