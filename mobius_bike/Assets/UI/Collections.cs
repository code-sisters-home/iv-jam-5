using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.Assertions;

public class Collections : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private CollectionItem _prefab;
    [SerializeField] private Button _sell;
    [SerializeField] private TextMeshProUGUI _finalPrice; 
    [SerializeField] private TextMeshProUGUI _selectedItemName; 
    [SerializeField] private Emptyable _emptyable; 

    private List<CollectionItem> collectionItems = new List<CollectionItem>();

    private void OnEnable()
    {
        Statistics.OnCollectionChanged += UpdateScreen;
        _sell.onClick.AddListener(Sell);
        UpdateScreen();
    }

    private void OnDisable()
    {
        Statistics.OnCollectionChanged -= UpdateScreen;
        _sell.onClick.RemoveAllListeners();
    }

    private void UpdateScreen()
    {
        if (GameMaster.Instance == null)
            return;

        for (int i = collectionItems.Count - 1; i >= 0; i--)
        {
            collectionItems[i].OnItemClicked -= HandleItemClick;
            Destroy(collectionItems[i].gameObject);
        }

        collectionItems.Clear();

        if(GameMaster.Instance.Statistics.collectionDatas.Count == 0)
        {
            _emptyable.SetEmpty(true);
            return;
        }

        _emptyable.SetEmpty(false);

        for (int i = 0; i < GameMaster.Instance.Statistics.collectionDatas.Count; i++)
        {
            var collectionItem = Instantiate(_prefab, _parent, false);
            collectionItem.Init(GameMaster.Instance.Statistics.collectionDatas[i]);
            collectionItem.OnItemClicked += HandleItemClick;
            collectionItems.Add(collectionItem);
        }

        if (_selectedId != Guid.Empty)
            HandleItemClick(_selectedId);
        else
            HandleItemClick(collectionItems.FirstOrDefault().Id);
    }

    private Guid _selectedId;
    private void HandleItemClick(Guid id)
    {
        _selectedId = id;

        foreach (var item in collectionItems)
        {
            item.selectable.SetSelected(item.Id == id);
        }

        var selectedItem = collectionItems.FirstOrDefault(_ => _.Id == id);
        Assert.IsNotNull(selectedItem);

        _selectedItemName.SetText(selectedItem.Name);
        _finalPrice.SetText($"Финальная цена продажи:\n<size=200%> {selectedItem.Price}");
    }

    private void Sell()
    {
        //прежде чем продать, выбираем следующий айтем, потому что придел коллбек на обновление экрана
        //и _selectedId не окажется среди collectionItems
        var itemToRemove = _selectedId;
        var itemToSelect = collectionItems.FirstOrDefault(_ => _.Id != _selectedId);
        if (itemToSelect == null)
            _selectedId = Guid.Empty;
        else
            _selectedId = itemToSelect.Id;
        GameMaster.Instance.Statistics.SellMushroom(itemToRemove);
    }
}
