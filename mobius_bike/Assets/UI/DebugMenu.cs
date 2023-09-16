using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    public void IncreaseLaps() => GameMaster.Instance.Statistics.IncreaseLaps();
    public void GetCollectionItem() => GameMaster.Instance.Statistics.GetCollectionItem();
}
