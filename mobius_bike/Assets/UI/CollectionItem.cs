using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Image _icon;

    private CollectionItemData _data;

    internal void Init(CollectionItemData data)
    {
        _data = data;
        _name.SetText(data.Text);
        _price.SetText("{0}", data.Price);
        _icon.sprite = GameMaster.Instance.SpriteAtlas.GetSprite(data.IconName);
    }
}

public class CollectionItemData
{
    public Guid Id;
    public string Text;
    public string IconName;
    public int Price;

    public CollectionItemData(string text, string iconName, int price)
    {
        Id = Guid.NewGuid();
        IconName = iconName;
        Text = text;
        Price = price;
    }
}

public enum CollectionItemType
{
    UI_Icon_Clover_Leaf,
    UI_Icon_Gem,
    UI_Icon_bomb,
    UI_Icon_Gas
}