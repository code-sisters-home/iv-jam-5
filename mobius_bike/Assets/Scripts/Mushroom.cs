using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] public MushroomType Type;
    
	public float lifetime = 10.5f;
    public bool IsEdible => IsEdibleType(Type);

    void Start()
    {
		Destroy(gameObject, lifetime);
    }

    public static string Name(MushroomType type)
    {
        return type switch
        {
            MushroomType.brown_cap_boletus => "Подберезовик",
            MushroomType.cep => "Боровик",
            MushroomType.moss_fly_mushroom => "Моховик",
            MushroomType.oily_mushroom => "Маслёнок",
            MushroomType.orange_cap_boletus => "Подосиновик",
            MushroomType.saffron_milk_cap => "Рыжик",
            MushroomType.yellow_mushroom => "Лисичка",
            MushroomType.amanita => "Мухомор",
            MushroomType.umbrella_mushroom => "Бледная поганка",
            _ => "Не гриб",
        };
    }

    public static string Sprite(MushroomType type)
    {
        return type switch
        {
            MushroomType.brown_cap_boletus => "1_brown_cap",
            MushroomType.cep => "2_cep",
            MushroomType.moss_fly_mushroom => "3_moss",
            MushroomType.oily_mushroom => "4_oily",
            MushroomType.orange_cap_boletus => "5_orange",
            MushroomType.saffron_milk_cap => "6_saffron",
            MushroomType.yellow_mushroom => "7_yellow",
            MushroomType.amanita => "8_amanita",
            MushroomType.umbrella_mushroom => "9_umbrella",
            _ => "Не гриб",
        };
    }

    public static bool IsEdibleType(MushroomType type)
    {
        return type switch
        {
            MushroomType.brown_cap_boletus => true,
            MushroomType.cep => true,
            MushroomType.moss_fly_mushroom => true,
            MushroomType.oily_mushroom => true,
            MushroomType.orange_cap_boletus => true,
            MushroomType.saffron_milk_cap => true,
            MushroomType.yellow_mushroom => true,
            MushroomType.amanita => false,
            MushroomType.umbrella_mushroom => false,
            _ => false,
        };
    }

    public static int Price(MushroomType type)
    {
        return type switch
        {
            MushroomType.brown_cap_boletus => 1000,
            MushroomType.cep => 5000,
            MushroomType.moss_fly_mushroom => 2500,
            MushroomType.oily_mushroom => 900,
            MushroomType.orange_cap_boletus => 1500,
            MushroomType.saffron_milk_cap => 600,
            MushroomType.yellow_mushroom => 300,
            MushroomType.amanita => 10000,
            MushroomType.umbrella_mushroom => 99900,
            _ => 0,
        };
    }

    public static MushroomType RandomMushroomType()
    {
        return (MushroomType) UnityEngine.Random.Range(0, (int)MushroomType.count-1);
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
    umbrella_mushroom,
    count //всегда последний
}


