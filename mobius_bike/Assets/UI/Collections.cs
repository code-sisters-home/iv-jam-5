using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collections : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private CollectionItem _prefab;

    private List<CollectionItem> collectionItems = new List<CollectionItem>();

    private void OnEnable()
    {
        Statistics.OnCollectionChanged += UpdateScreen;
        UpdateScreen();
    }

    private void OnDisable()
    {
        Statistics.OnCollectionChanged -= UpdateScreen;
    }

    private void UpdateScreen()
    {
        for (int i = collectionItems.Count - 1; i >= 0; i--)
        {
            Destroy(collectionItems[i].gameObject);
        }

        collectionItems.Clear();

        for (int i = 0; i < GameMaster.Instance.Statistics.collectionDatas.Count; i++)
        {
            var collectionItem = Instantiate(_prefab, _parent, false);
            collectionItem.Init(GameMaster.Instance.Statistics.collectionDatas[i]);
            collectionItems.Add(collectionItem);
        }
    }
}
