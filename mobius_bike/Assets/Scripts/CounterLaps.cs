using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterLaps : MonoBehaviour
{
    public Collider point;

    private void OnTriggerEnter(Collider other)
    {
        if (point == null)
            return;
        if(other == point)
            GameMaster.Instance.Statistics.IncreaseLaps();

        if (other.tag.Equals("drop"))
        {
            GameMaster.Instance.Statistics.GetCollectionItem();
            Destroy(other.gameObject);
        }
    }
}
