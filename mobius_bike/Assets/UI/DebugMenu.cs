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
}
