using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] public MushroomType Type;
    [SerializeField] public Sprite Sprite;
    [SerializeField] public bool IsEdible;

    public static string Name(MushroomType type)
    {
        switch (type)
        {
            case MushroomType.brown_cap_boletus:
                return "������������";
            case MushroomType.cep:
                return "�������";
            case MushroomType.moss_fly_mushroom:
                return "�������";
            case MushroomType.oily_mushroom:
                return "�������";
            case MushroomType.orange_cap_boletus:
                return "�����������";
            case MushroomType.saffron_milk_cap:
                return "�����";
            case MushroomType.yellow_mushroom:
                return "�������";
            case MushroomType.amanita:
                return "�������";
            case MushroomType.umbrella_mushroom:
                return "������� �������";
            default:
                return "�� ����";
        }
    }

    public static int Price(MushroomType type)
    {
        switch (type)
        {
            case MushroomType.brown_cap_boletus:
                return 1000;
            case MushroomType.cep:
                return 5000;
            case MushroomType.moss_fly_mushroom:
                return 2500;
            case MushroomType.oily_mushroom:
                return 900;
            case MushroomType.orange_cap_boletus:
                return 1500;
            case MushroomType.saffron_milk_cap:
                return 600;
            case MushroomType.yellow_mushroom:
                return 300;
            case MushroomType.amanita:
                return 10000;
            case MushroomType.umbrella_mushroom:
                return 99900;
            default:
                return 0;
        }
    }
}

public enum MushroomType
{
    brown_cap_boletus,
    cep,
    moss_fly_mushroom,
    oily_mushroom,
    orange_cap_boletus,
    saffron_milk_cap,
    yellow_mushroom,
    amanita,
    umbrella_mushroom
}
