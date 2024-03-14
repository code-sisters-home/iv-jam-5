﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class Statistics : MonoBehaviour
{
    public static event Action OnAchievementsChanged = () => { };
    public static event Action OnCollectionChanged = () => { };
    public static event Action OnStatsChanged = () => { };
    public static event Action OnDied = () => { };

    public List<AchievementData> achievementDatas = new List<AchievementData>();
    public List<CollectionItemData> collectionDatas = new List<CollectionItemData>();

    private int _gold = 100;
    private int _life = 3;
    public int Gold 
    {
        get => _gold;
        set 
        {
            if(value != _gold)
            {
                _gold = value;
                OnStatsChanged.Invoke();
            }
        }
    }

    public int Lifes 
    {
        get => _life;
        set
        {
            if (value != _life)
            {
                _life = value;
                OnStatsChanged.Invoke();
            }
        }
    }
    public static int MaxLifes = 3;

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
    }

    public void GetMushroom(Mushroom mushroom)
    {
        var isEdible = mushroom.IsEdible ? "Съедобный" : "Ядовитый";
        collectionDatas.Add(new CollectionItemData(
            text: Mushroom.Name(mushroom.Type)+"\n"+ isEdible,
            iconName: Mushroom.Sprite(mushroom.Type),
            price: Mushroom.Price(mushroom.Type)
            ));
        OnCollectionChanged();
    }

    public void GetMushroom()
    {
        var mushroomType = Mushroom.RandomMushroomType();
        var isEdible = Mushroom.IsEdibleType(mushroomType) ? "<color=green>Съедобный</color>" : "<color=red>Ядовитый</color>";
        collectionDatas.Add(new CollectionItemData(
            text: Mushroom.Name(mushroomType) + "\n<size=80%>" + isEdible,
            iconName: Mushroom.Sprite(mushroomType),
            price: Mushroom.Price(mushroomType)
            ));
        OnCollectionChanged();
    }

    public void SellMushroom(Guid id)
    {
        var itemToRemove = collectionDatas.FirstOrDefault(_ => _.Id == id);
        Assert.IsNotNull(itemToRemove, $"item data '{id}' not found");
        collectionDatas.Remove(itemToRemove);
        Gold += itemToRemove.Price;
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

        if (Lifes == 0)
            OnDied();
    }

    public void GetLife(int count)
    {
        Lifes += count;        
    }

    public void GetMoney(int count)
    {
        Gold += count;
    }
}
