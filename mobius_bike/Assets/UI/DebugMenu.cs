using CodeSisters.Utils;
using InstantGamesBridge;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    public void IncreaseLaps() => GameMaster.Instance.Statistics.IncreaseLaps();
    public void GetCollectionItem() => GameMaster.Instance.Statistics.GetCollectionItem();
    public void DropItem() => GameMaster.Instance.DropGenerator.DropSomething();

    public void GetDamage() => GameMaster.Instance.Statistics.GetDamage();
    public void GetLife() => GameMaster.Instance.Statistics.GetLife(1);
    public void GetCoins() 
    {
        GameMaster.Instance.Statistics.GetMoney(10);
        UnityLogger.Log($"gold storage type: {Bridge.storage.defaultType} ");
        UnityLogger.Log($"gold runtime {GameMaster.Instance.Statistics.Gold} ");
        Bridge.storage.Set("gold", GameMaster.Instance.Statistics.Gold, OnStorageSetCompleted);
    }

    private void OnStorageSetCompleted(bool obj)
    {
        UnityLogger.Log($"gold OnStorageSetCompleted {obj} ");
        Bridge.storage.Get("gold", OnStorageGetCompleted);
    }

    private void OnStorageGetCompleted(bool arg1, string arg2)
    {
        UnityLogger.Log($"gold OnStorageGetCompleted {arg1} ");
        UnityLogger.Log($"gold callback Get {arg2} ");
    }
}
